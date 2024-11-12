using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain
{
    internal class Class1
    {
    }

    //public async Task AddParticipantForCertificate(List<(long trainingId, CertificateType certificateType, string userId)> certificateData)
    //{
    //    long GenTrainingId = 0;
    //    List<Certificate> certificatesToAdd = new List<Certificate>();

    //    // Get the last GeneralNumber from the Certificates table
    //    int lastGeneralNumber = 0;
    //    if (_context.Certificates.Any())
    //    {
    //        lastGeneralNumber = await _context.Certificates.MaxAsync(c => c.GeneralNumber);
    //    }

    //    // A dictionary to keep track of last SectionNumbers per CategoryNumberOnCertificate
    //    Dictionary<string, int> lastSectionNumbers = new Dictionary<string, int>();

    //    foreach (var (trainingId, certificateType, userId) in certificateData)
    //    {
    //        string CategoryNumberOnCertificate = "";
    //        string Year = "";
    //        var getTrainingCategory = await _context.TrainingParticipants
    //            .Include(x => x.Training)
    //            .FirstOrDefaultAsync(x => x.TrainingId == trainingId && x.UserId == userId);

    //        if (getTrainingCategory != null)
    //        {
    //            Year = getTrainingCategory.Training.StartDate.ToString("yy");
    //            var categoryNumber = await _context.TrainingCategories.FirstOrDefaultAsync(x => x.Id == getTrainingCategory.Training.TrainingCategoryId);
    //            if (categoryNumber != null)
    //            {
    //                CategoryNumberOnCertificate = categoryNumber.CertificateInitial;
    //            }
    //        }

    //        GenTrainingId = trainingId;

    //        // Check if the certificate already exists
    //        var usercert = await _context.Certificates.FirstOrDefaultAsync(x => x.TrainingId == trainingId && x.UserId == userId && x.CertificateType == certificateType);
    //        if (usercert == null)
    //        {
    //            // Get user
    //            var user = await _userManager.FindByIdAsync(userId);
    //            if (user != null)
    //            {
    //                // Update GeneralNumber
    //                lastGeneralNumber++;
    //                int GeneralNumber = lastGeneralNumber;

    //                // Update SectionNumber
    //                int lastSectionNumber = 0;
    //                if (!string.IsNullOrEmpty(CategoryNumberOnCertificate))
    //                {
    //                    if (!lastSectionNumbers.ContainsKey(CategoryNumberOnCertificate))
    //                    {
    //                        // Get the last SectionNumber for this CategoryNumberOnCertificate
    //                        var lastCertInCategory = await _context.Certificates
    //                            .Where(c => c.CategoryNumberOnCertificate == CategoryNumberOnCertificate)
    //                            .OrderByDescending(c => c.SectionNumber)
    //                            .FirstOrDefaultAsync();
    //                        if (lastCertInCategory != null)
    //                        {
    //                            lastSectionNumber = lastCertInCategory.SectionNumber;
    //                        }
    //                        lastSectionNumbers[CategoryNumberOnCertificate] = lastSectionNumber;
    //                    }

    //                    // Increment SectionNumber for this category
    //                    lastSectionNumbers[CategoryNumberOnCertificate]++;
    //                    int SectionNumber = lastSectionNumbers[CategoryNumberOnCertificate];

    //                    // Create the Certificate object
    //                    Certificate cert = new Certificate
    //                    {
    //                        TrainingId = trainingId,
    //                        UserId = userId,
    //                        FullName = user.FullnameX,
    //                        PassportUrl = user.PassportFilePathUrl,
    //                        IssuerDate = DateTime.UtcNow.AddHours(1),
    //                        CertificateStatus = CertificateStatus.Preview,
    //                        CertificateType = certificateType,
    //                        SectionNumber = SectionNumber,
    //                        GeneralNumber = GeneralNumber,
    //                        CategoryNumberOnCertificate = CategoryNumberOnCertificate,
    //                        ye = Year
    //                        // CerificateNumber will be assigned later
    //                    };
    //                    certificatesToAdd.Add(cert);
    //                }
    //                else
    //                {
    //                    throw new ArgumentException("CategoryNumberOnCertificate cannot be empty.");
    //                }
    //            }
    //        }
    //        else
    //        {
    //            throw new ArgumentException($"Certificate already exists for user {userId} and training {trainingId}.");
    //        }
    //    }

    //    // Add all certificates to the context
    //    _context.Certificates.AddRange(certificatesToAdd);

    //    // Save changes to get IDs assigned
    //    await _context.SaveChangesAsync();

    //    // Now, update CerificateNumber using IDs
    //    foreach (var cert in certificatesToAdd)
    //    {
    //        // Generate CertificateNumber, e.g., "MIY00001NUT"
    //        cert.CerificateNumber = $"MIY{cert.Id.ToString("D5")}NUT";

    //        // Alternatively, if you want to use SectionNumber and GeneralNumber in the CertificateNumber
    //        // cert.CerificateNumber = $"{cert.CategoryNumberOnCertificate}P{cert.SectionNumber.ToString("D4")}{cert.GeneralNumber.ToString("D6")}/MIYCN/{cert.Year}";

    //        // Mark certificate as modified
    //        _context.Entry(cert).State = EntityState.Modified;
    //    }

    //    // Save changes to the database
    //    await _context.SaveChangesAsync();
    //}

}
