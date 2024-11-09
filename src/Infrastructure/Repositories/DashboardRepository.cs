using Domain.DTOs;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;
        private readonly ITrainingRepository _repository;
        public DashboardRepository(AppDbContext context, ITrainingRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        public async Task<AdminDashboardDto> AdminDashboardData(string? state)
        {

            AdminDashboardDto adminDashboardDto = new AdminDashboardDto();
           

            if (state == "all")
            {
                adminDashboardDto.Trainings = await _context.Trainings.CountAsync();
                adminDashboardDto.Facilitator = await _context.TrainingFacilitators.CountAsync();
                adminDashboardDto.Participants = await _context.TrainingParticipants.Where(x => x.ParticipantTrainingStatus == ParticipantTrainingStatus.Active).CountAsync();
                adminDashboardDto.Sponsors = await _context.Sponsors.CountAsync();
            }
            else
            {
                if (state != null)
                {
                    adminDashboardDto.Trainings = await _context.Trainings.Where(x => x.State.ToLower() == state.ToLower()).CountAsync();
                    adminDashboardDto.Facilitator = await _context.TrainingFacilitators.Include(x=>x.Training).Where(x => x.Training.State.ToLower() == state.ToLower()).CountAsync();
                    adminDashboardDto.Participants = await _context.TrainingParticipants.Include(x => x.Training).Where(x => x.Training.State.ToLower() == state.ToLower() && x.ParticipantTrainingStatus == ParticipantTrainingStatus.Active).CountAsync();
                    adminDashboardDto.Sponsors = await _context.Sponsors.Include(x => x.Training).Where(x => x.Training.State.ToLower() == state.ToLower()).CountAsync();
                     
                }
                else
                {
                    adminDashboardDto.Trainings = 0;
                    adminDashboardDto.Facilitator = 0;
                    adminDashboardDto.Participants = 0;
                    adminDashboardDto.Sponsors = 0;
                }
            }

            
           


            return adminDashboardDto;
        }

        public async Task<UserDashboardDto> UserDashboardData(string userId)
        {
            UserDashboardDto UserDashboardDto = new UserDashboardDto();

           List<TrainingByUserDto> ptraining = await _repository.GetAllTrainingsByUserId(userId);


 
            UserDashboardDto.Trainings = ptraining.Count();
            UserDashboardDto.ActiveTraining = ptraining.Where(x=>x.TrainingStatus == TrainingStatus.Active).Count();
            UserDashboardDto.CompletedTraining = ptraining.Where(x=>x.TrainingStatus == TrainingStatus.Completed).Count();
            UserDashboardDto.CancelledTraining = ptraining.Where(x=>x.TrainingStatus == TrainingStatus.Cancelled).Count();
            UserDashboardDto.UpcomingTraining = ptraining.Where(x=>x.TrainingStatus == TrainingStatus.Upcoming).Count();


            return UserDashboardDto;
        }
    }
}
