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
    public sealed class SupervisorSectionQuestionRepository : Repository<SupervisorSectionQuestion>, ISupervisorSectionQuestionRepository
    {
        private readonly AppDbContext _context;

        public SupervisorSectionQuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<SupervisorSectionQuestion>> ListQuestions()
        {
           var list = await _context.SupervisorSectionQuestions.Include(x=>x.SupervisorSection).ToListAsync();
            return list;
        }
    }
}
