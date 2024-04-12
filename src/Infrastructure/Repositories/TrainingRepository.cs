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
    public sealed class TrainingRepository : Repository<Training>, ITrainingRepository
    {
        private readonly AppDbContext _context;

        public TrainingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TrainingByUserDto>> GetAllTrainingsByUserId(string userId)
        {
            var trainings = await _context.Trainings
                .Include(t => t.TrainingFacilitators)
                    .ThenInclude(tf => tf.User)
                .Include(t => t.TrainingParticipants)
                    .ThenInclude(tp => tp.User)
                .Where(t => t.TrainingFacilitators.Any(tf => tf.UserId == userId) ||
                            t.TrainingParticipants.Any(tp => tp.UserId == userId))
                .ToListAsync();

            var trainingDtos = new List<TrainingByUserDto>();

            foreach (var training in trainings)
            {
                foreach (var facilitator in training.TrainingFacilitators.Where(tf => tf.UserId == userId))
                {
                    trainingDtos.Add(new TrainingByUserDto
                    {
                        UserId = userId,
                        TrainingId = training.Id,
                        Type = "Facilitator",
                        FacilitatorPosition = facilitator.Position,
                        TrainingTitle = training.Title,
                        TrainingDate = training.StartDate, // or any date property you prefer
                        UserEmail = facilitator.User.Email,
                        UserPhone = facilitator.User.PhoneNumber,
                        UserState = facilitator.User.CurrentState,
                        UserLGA = facilitator.User.CurrentLga,
                        PlaceOfWork = facilitator.User.PlaceOfWork,
                        TrainingLGA = training.LGA,
                        TrainingState = training.State,
                        TrainingStatus = training.TrainingStatus
                    });
                }

                foreach (var participant in training.TrainingParticipants.Where(tp => tp.UserId == userId))
                {
                    trainingDtos.Add(new TrainingByUserDto
                    {
                        UserId = userId,
                        TrainingId = training.Id,
                        Type = "Participant",
                        TrainingTitle = training.Title,
                        TrainingDate = training.StartDate, // or any date property you prefer
                        UserEmail = participant.User.Email,
                        UserPhone = participant.User.PhoneNumber,
                        UserState = participant.User.CurrentState,
                        UserLGA = participant.User.CurrentLga,
                        PlaceOfWork = participant.User.PlaceOfWork,
                        TrainingLGA = training.LGA,
                        TrainingState = training.State,
                        TrainingStatus = training.TrainingStatus
                    });
                }
            }

            return trainingDtos;
        }


        public async Task<TrainingDto> GetTrainingByIdAndCounts(long id)
        {
            var trainingDto = await _context.Trainings
        .Where(x => x.Id == id)
        .Select(x => new TrainingDto
        {
            Id = x.Id,
            Title = x.Title,
            State = x.State,
            LGA = x.LGA,
            Ward = x.Ward,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            TrainingStatus = x.TrainingStatus,
            DialyStartTime = x.DialyStartTime,
            DialyEndTime = x.DialyEndTime,
            Sponsors = x.Sponsors.Count(),
            TrainingFacilitators = x.TrainingFacilitators.Count(),
            TrainingParticipants = x.TrainingParticipants.Count(),
            DialyActivities = x.DialyActivities.Count(),
            TestCategory = x.TestCategory.Count()
        })
        .FirstOrDefaultAsync();
            if (trainingDto != null)
            {
                return trainingDto;
            }
            else
            {
                return null;
            }
        }
    }
}
