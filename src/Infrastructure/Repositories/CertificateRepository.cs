using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Infrastructure.Repositories
{
    public sealed class CertificateRepository : Repository<Certificate>, ICertificateRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserTestRepository _userTestRepository;
        private readonly UserManager<AppUser> _userManager;

        public CertificateRepository(AppDbContext context, IUserTestRepository userTestRepository, UserManager<AppUser> userManager) : base(context)
        {
            _context = context;
            _userTestRepository = userTestRepository;
            _userManager = userManager;
        }

        public async Task AddParticipantForCertificate(List<(long trainingId, CertificateType certificateType, string userId)> certificateData)
        {
            long GenTrainingId = 0;
            List<Certificate> certificatesToAdd = new List<Certificate>();

            // Get the last GeneralNumber from the Certificates table
            int lastGeneralNumber = 0;
            if (_context.Certificates.Any())
            {
                lastGeneralNumber = await _context.Certificates.MaxAsync(c => c.GeneralNumber);
            }

            // A dictionary to keep track of last SectionNumbers per CategoryNumberOnCertificate
            Dictionary<string, int> lastSectionNumbers = new Dictionary<string, int>();
            foreach (var (trainingId, certificateType, userId) in certificateData)
            {
                //
                string CategoryNumberOnCertificate = "";
                string Year = "";
                var getTrainingCategory = await _context.TrainingParticipants.Include(x => x.Training).FirstOrDefaultAsync(x => x.TrainingId == trainingId && x.UserId == userId);
                if (getTrainingCategory != null)
                {

                    Year = getTrainingCategory.Training.StartDate.ToString("yy");
                    var categoryNumber = await _context.TrainingCategories.FirstOrDefaultAsync(x => x.Id == getTrainingCategory.Training.TrainingCategoryId);
                    if (categoryNumber != null)
                    {
                        CategoryNumberOnCertificate = categoryNumber.CertificateInitial;
                    }
                }


                GenTrainingId = trainingId;
                int CategoryNumber = 0;
                int GeneralNumber = 0;
                // Retrieve the Certificates record by its ID
                var usercert = await _context.Certificates.FirstOrDefaultAsync(x => x.TrainingId == trainingId && x.UserId == userId && x.CertificateType == certificateType);
                if (usercert == null)
                {
                    //get user
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        // Update GeneralNumber
                        lastGeneralNumber++;
                        GeneralNumber = lastGeneralNumber;

                        // Update SectionNumber
                        int lastSectionNumber = 0;
                        if (!string.IsNullOrEmpty(CategoryNumberOnCertificate))
                        {
                            if (!lastSectionNumbers.ContainsKey(CategoryNumberOnCertificate))
                            {
                                // Get the last SectionNumber for this CategoryNumberOnCertificate
                                var lastCertInCategory = await _context.Certificates
                                    .Where(c => c.CategoryNumberOnCertificate == CategoryNumberOnCertificate)
                                    .OrderByDescending(c => c.SectionNumber)
                                    .FirstOrDefaultAsync();
                                if (lastCertInCategory != null)
                                {
                                    lastSectionNumber = lastCertInCategory.SectionNumber;
                                }
                                lastSectionNumbers[CategoryNumberOnCertificate] = lastSectionNumber;
                            }

                            // Increment SectionNumber for this category
                            lastSectionNumbers[CategoryNumberOnCertificate]++;
                            CategoryNumber = lastSectionNumbers[CategoryNumberOnCertificate];


                            // add the Certificates
                            Certificate cert = new Certificate();
                            cert.TrainingId = trainingId;
                            cert.UserId = userId;
                            cert.FullName = user.FullnameX;
                            cert.PassportUrl = user.PassportFilePathUrl;
                            cert.IssuerDate = DateTime.UtcNow.AddHours(1);
                            cert.CertificateStatus = CertificateStatus.Preview;
                            cert.CertificateType = certificateType;
                            cert.SectionNumber = CategoryNumber;
                            cert.GeneralNumber = GeneralNumber;
                            cert.CategoryNumberOnCertificate = CategoryNumberOnCertificate;
                            cert.CerificateNumber = $"{CategoryNumberOnCertificate}P{CategoryNumber.ToString("D4")}{GeneralNumber.ToString("D6")}/MIYCN{Year}";
                            _context.Certificates.Add(cert);
                        }
                    }
                }
                else
                {
                    // Handle the case where the provided attendanceId is not found
                    throw new ArgumentException($"Certificates record with  not found.");
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
            //
            var getallcert = await _context.Certificates.Where(x => x.TrainingId == GenTrainingId && x.CerificateNumber == null).ToListAsync();
            foreach (var cert in getallcert)
            {
                cert.CerificateNumber = $"MIY" + cert.Id.ToString("00000") + "NUT";
                _context.Attach(cert).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Certificate> GetCertificateByNumber(string number)
        {
            var getallcert = await _context.Certificates
                .Include(x => x.Training)
                .FirstOrDefaultAsync(x => x.CerificateNumber == number);
            return getallcert;
        }

        public async Task<List<Certificate>> GetCertificateByTrainingId(long trainingId)
        {
            var list = await _context.Certificates
                .Include(x => x.Training)
                .Include(x => x.User)
                .Where(x => x.TrainingId == trainingId).ToListAsync();
            return list;
        }

        public async Task<List<ParticipantCertificateDto>> ParticipantCertificateList(long trainingId)
        {
            var participants = await _context.TrainingParticipants
                .Include(p => p.User)
                .Where(p => p.TrainingId == trainingId).Where(tp => tp.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active)
                .ToListAsync();

            var participantCertificates = new List<ParticipantCertificateDto>();

            foreach (var participant in participants)
            {
                var pretestResult = await _userTestRepository.UserTestResult(trainingId, participant.UserId, (int)TrainingTestType.PreTest);
                var posttestResult = await _userTestRepository.UserTestResult(trainingId, participant.UserId, (int)TrainingTestType.PostTest);

                var attendance = await _context.Attendances
                    .Include(x => x.DialyActivity)
                    .Where(a => a.UserId == participant.UserId && a.DialyActivity.TrainingId == trainingId)
                    .ToListAsync();
                bool AddedForCert = false;
                var usercert = await _context.Certificates.FirstOrDefaultAsync(x => x.TrainingId == trainingId && x.UserId == participant.UserId && x.CertificateType == CertificateType.Participant);
                if (usercert != null)
                {
                    AddedForCert = true;
                }
                var signInPresent = attendance.Where(x => x.AttendanceSignInStatus == AttendanceSignInStatus.Present).Count();
                var signInAbsent = attendance.Where(x => x.AttendanceSignInStatus == AttendanceSignInStatus.Absent).Count();
                var signOutPresent = attendance.Where(x => x.AttendanceSignOutStatus == AttendanceSignOutStatus.Present).Count();
                var signOutAbsent = attendance.Where(x => x.AttendanceSignOutStatus == AttendanceSignOutStatus.Present).Count();

                var certificateDto = new ParticipantCertificateDto
                {
                    UserId = participant.UserId,
                    ParticipantId = participant.Id,
                    Email = participant.User.Email,
                    Fullname = participant.User.FullnameX,
                    Phone = participant.User.PhoneNumber,
                    PretestScore = pretestResult != null ? pretestResult.PercentageResult : "0%",
                    PostestScore = posttestResult != null ? posttestResult.PercentageResult : "0%",
                    SignInAttendancePresent = signInPresent.ToString(),
                    SignInAttendanceAbsent = signInAbsent.ToString(),
                    SignOutAttendancePresent = signOutPresent.ToString(),
                    SignOutAttendanceAbsent = signOutAbsent.ToString(),
                    AddedToCertificate = AddedForCert
                };

                participantCertificates.Add(certificateDto);
            }

            return participantCertificates;
        }

    }
}
