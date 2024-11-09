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
using static System.Collections.Specialized.BitVector32;

namespace Infrastructure.Repositories
{
      public sealed class SupervisorSectionRepository : Repository<SupervisorSection>, ISupervisorSectionRepository
    {
        private readonly AppDbContext _context;

        public SupervisorSectionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<SupervisorSection>> GetAll()
        {
           var sections = await _context.SupervisorSections
                .Include(s => s.SupervisorSectionQuestion)
                .OrderBy(s => s.SortOrder)
                .ToListAsync();
            return sections;
        }
    }
}
