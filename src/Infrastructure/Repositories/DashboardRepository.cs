using Domain.DTOs;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AdminDashboardDto> AdminDashboardData(string? state)
        {

            AdminDashboardDto adminDashboardDto = new AdminDashboardDto();
           

            if (state == "all")
            {
                adminDashboardDto.Trainings = await _context.Trainings.CountAsync();
                adminDashboardDto.Facilitator = await _context.TrainingFacilitators.CountAsync();
                adminDashboardDto.Participants = await _context.TrainingParticipants.CountAsync();
                adminDashboardDto.Sponsors = await _context.Sponsors.CountAsync();
            }
            else
            {
                if (state != null)
                {
                    adminDashboardDto.Trainings = await _context.Trainings.Where(x => x.State.ToLower() == state.ToLower()).CountAsync();
                    adminDashboardDto.Facilitator = await _context.TrainingFacilitators.Include(x=>x.Training).Where(x => x.Training.State.ToLower() == state.ToLower()).CountAsync();
                    adminDashboardDto.Participants = await _context.TrainingParticipants.Include(x => x.Training).Where(x => x.Training.State.ToLower() == state.ToLower()).CountAsync();
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
    }
}
