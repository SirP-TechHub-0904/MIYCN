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
        public async Task<AdminDashboardDto> AdminDashboardData()
        {
            AdminDashboardDto adminDashboardDto = new AdminDashboardDto();
            adminDashboardDto.Trainings = await _context.Trainings.CountAsync();
            adminDashboardDto.Facilitator = await _context.TrainingFacilitators.CountAsync();
            adminDashboardDto.Participants = await _context.TrainingParticipants.CountAsync();
            adminDashboardDto.Sponsors = await _context.Sponsors.CountAsync();


            return adminDashboardDto;
        }
    }
}
