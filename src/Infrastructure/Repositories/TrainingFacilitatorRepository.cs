using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class TrainingFacilitatorRepository : Repository<TrainingFacilitator>, ITrainingFacilitatorRepository
    {
        private readonly AppDbContext _context;

        public TrainingFacilitatorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddFacilitator(TrainingFacilitator model)
        {
            var check = await _context.TrainingFacilitators.FirstOrDefaultAsync(x => x.TrainingId == model.TrainingId && x.UserId == model.UserId);
            if (check == null)
            {
                await AddAsync(model);
                return true;
            }
            return false;
        }

        public async Task<FacilitatorInTrainingDTo> FacilitatorInTraining(long trainingId, string userId)
        {
            var participantDto = await _context.TrainingFacilitators
                .Where(tp => tp.TrainingId == trainingId && tp.UserId == userId)
                .Select(tp => new FacilitatorInTrainingDTo
                {
                    Id = tp.User.Id,
                    UniqueId = tp.User.UniqueId,
                    AccountToken = tp.User.AccountToken,
                    FirstName = tp.User.FirstName,
                    MiddleName = tp.User.MiddleName,
                    LastName = tp.User.LastName,
                    DateOfBirth = tp.User.DateOfBirth,
                    Date = tp.User.Date,
                    Religion = tp.User.Religion,
                    PhoneNumber = tp.User.PhoneNumber,
                    Email = tp.User.Email,
                    UserStatus = tp.User.UserStatus,
                    Gender = tp.User.Gender,
                    Role = tp.User.Role,
                    CurrentState = tp.User.CurrentState,
                    CurrentLga = tp.User.CurrentLga,
                    Address = tp.User.Address,
                    PlaceOfWork = tp.User.PlaceOfWork,
                    StateOrigin = tp.User.StateOrigin,
                    LgaOrigin = tp.User.LgaOrigin,
                    Country = tp.User.Country,
                    PassportFilePathUrl = tp.User.PassportFilePathUrl,
                    PassportFilePathKey = tp.User.PassportFilePathKey,
                    IdCardUrl = tp.User.IdCardUrl,
                    IdCardKey = tp.User.IdCardKey,
                    BankName = tp.User.BankName,
                    BankAccount = tp.User.BankAccount,
                    AccountNumber = tp.User.AccountNumber,
                    Logs = tp.User.Logs,
                    TrainingId = tp.TrainingId,
                    Title = tp.Training.Title,
                    State = tp.Training.State,
                    LGA = tp.Training.LGA,
                    Position = tp.Position
                })
                .FirstOrDefaultAsync();

            return participantDto;
        }

        public async Task<List<FacilitatorInTrainingDTo>> FacilitatorInTraining(long trainingId)
        {
            var participants = await _context.TrainingFacilitators
                   .Where(tp => tp.TrainingId == trainingId)
                   .Select(tp => new FacilitatorInTrainingDTo
                   {
                       Id = tp.User.Id,
                       UniqueId = tp.User.UniqueId,
                       AccountToken = tp.User.AccountToken,
                       FirstName = tp.User.FirstName,
                       MiddleName = tp.User.MiddleName,
                       LastName = tp.User.LastName,
                       DateOfBirth = tp.User.DateOfBirth,
                       Date = tp.User.Date,
                       Religion = tp.User.Religion,
                       PhoneNumber = tp.User.PhoneNumber,
                       Email = tp.User.Email,
                       UserStatus = tp.User.UserStatus,
                       Gender = tp.User.Gender,
                       Role = tp.User.Role,
                       CurrentState = tp.User.CurrentState,
                       CurrentLga = tp.User.CurrentLga,
                       Address = tp.User.Address,
                       PlaceOfWork = tp.User.PlaceOfWork,
                       StateOrigin = tp.User.StateOrigin,
                       LgaOrigin = tp.User.LgaOrigin,
                       Country = tp.User.Country,
                       PassportFilePathUrl = tp.User.PassportFilePathUrl,
                       PassportFilePathKey = tp.User.PassportFilePathKey,
                       IdCardUrl = tp.User.IdCardUrl,
                       IdCardKey = tp.User.IdCardKey,
                       BankName = tp.User.BankName,
                       BankAccount = tp.User.BankAccount,
                       AccountNumber = tp.User.AccountNumber,
                       Logs = tp.User.Logs,
                       TrainingId = tp.TrainingId,
                       Title = tp.Training.Title,
                       State = tp.Training.State,
                       LGA = tp.Training.LGA,
                       Position = tp.Position
                   })
                   .ToListAsync();

            return participants;
        }

    }
}
