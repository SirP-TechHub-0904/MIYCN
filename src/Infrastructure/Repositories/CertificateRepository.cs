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
            Dictionary<string, int> lastRegNumbers = new Dictionary<string, int>();
            foreach (var (trainingId, certificateType, userId) in certificateData)
            {
                //
                //string CategoryNumberOnCertificate = "";
                string Year = "";
                var getTraining = await _context.TrainingParticipants
                    .Include(x => x.Training).ThenInclude(x => x.Provider)
                    .Include(x => x.Training).ThenInclude(x => x.Batch)


                    .FirstOrDefaultAsync(x => x.TrainingId == trainingId && x.UserId == userId);
                if (getTraining != null)
                {

                    Year = getTraining.Training.StartDate.ToString("yy");
                  
                }
                

                string FederalorState = getTraining.Training.IsFederal ? "F" : getTraining.Training.StateCode;
                string MasterOrProvider = getTraining.Training.Provider?.Abbreviation ?? "";
                string BatchNumber = getTraining.Training.Batch?.Title ?? "00";


                GenTrainingId = trainingId;
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
                        //int CategoryNumberOnCertificate = 0;
                        // Update SectionNumber

                        int regNumber = 0;
                        if (!lastRegNumbers.ContainsKey(FederalorState))
                        {
                            // Get the last RegNumber for this FederalorState category
                            var lastCertInCategory = await _context.Certificates
                                .Where(c => c.FederalorState == FederalorState)
                                .OrderByDescending(c => c.SectionNumber)
                                .FirstOrDefaultAsync();

                            regNumber = lastCertInCategory?.SectionNumber ?? 0;
                            lastRegNumbers[FederalorState] = regNumber;
                        }
                        
                        // Increment RegNumber for this FederalorState
                        lastRegNumbers[FederalorState]++;
                        regNumber = lastRegNumbers[FederalorState];



                        // add the Certificates
                        Certificate cert = new Certificate();
                        cert.TrainingId = trainingId;
                        cert.UserId = userId;
                        cert.FullName = user.FullnameX;
                        cert.PassportUrl = user.PassportFilePathUrl;
                        cert.IssuerDate = DateTime.UtcNow.AddHours(1);
                        cert.CertificateStatus = CertificateStatus.Preview;
                        cert.CertificateType = certificateType;
                        cert.SectionNumber = regNumber;
                        cert.FederalorState = FederalorState;
                        cert.GeneralNumber = GeneralNumber;
                        cert.CerificateNumber = $"{FederalorState}MIYCN{MasterOrProvider}{BatchNumber}{regNumber.ToString("D4")}{GeneralNumber.ToString("D7")}{Year}";
                        _context.Certificates.Add(cert);

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

        public async Task<List<Certificate>> GetAllCert()
        {
            var list = await _context.Certificates
                .Include(x=>x.User)
                .Include(x=>x.Training)
                .ToListAsync();
            return list;
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
                .Where(p => p.TrainingId == trainingId)
                .Where(tp => tp.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active)
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

                bool addedForCert = await _context.Certificates
                    .AnyAsync(x => x.TrainingId == trainingId && x.UserId == participant.UserId && x.CertificateType == CertificateType.Participant);

                var signInPresent = attendance.Count(x => x.AttendanceSignInStatus == AttendanceSignInStatus.Present);
                var signInAbsent = attendance.Count(x => x.AttendanceSignInStatus == AttendanceSignInStatus.Absent);
                var signOutPresent = attendance.Count(x => x.AttendanceSignOutStatus == AttendanceSignOutStatus.Present);
                var signOutAbsent = attendance.Count(x => x.AttendanceSignOutStatus == AttendanceSignOutStatus.Absent);

                // Determine if Pre-Test and Post-Test scores are a pass
                bool pretestPass = pretestResult != null &&
                                   !string.IsNullOrEmpty(pretestResult.PercentageResult) &&
                                   int.TryParse(pretestResult.PercentageResult.Replace("%", ""), out var preScore) &&
                                   preScore >= 70;

                bool posttestPass = posttestResult != null &&
                                    !string.IsNullOrEmpty(posttestResult.PercentageResult) &&
                                    int.TryParse(posttestResult.PercentageResult.Replace("%", ""), out var postScore) &&
                                    postScore >= 70;

                var certificateDto = new ParticipantCertificateDto
                {
                    UserId = participant.UserId,
                    ParticipantId = participant.Id,
                    Email = participant.User.Email,
                    Fullname = participant.User.FullnameX,
                    Phone = participant.User.PhoneNumber,
                    PretestScore = pretestResult?.PercentageResult ?? "0%",
                    PostestScore = posttestResult?.PercentageResult ?? "0%",
                    SignInAttendancePresent = signInPresent.ToString(),
                    SignInAttendanceAbsent = signInAbsent.ToString(),
                    SignOutAttendancePresent = signOutPresent.ToString(),
                    SignOutAttendanceAbsent = signOutAbsent.ToString(),
                    AddedToCertificate = addedForCert,
                    PretestPass = pretestPass,
                    PosttestPass = posttestPass
                };

                participantCertificates.Add(certificateDto);
            }

            // Sort participants by PosttestScore in descending order, handling numeric scores properly
            var sortedCertificates = participantCertificates
                .OrderByDescending(c => int.TryParse(c.PostestScore.Replace("%", ""), out var score) ? score : 0)
                .ToList();

            return sortedCertificates;
        }

    }
}
