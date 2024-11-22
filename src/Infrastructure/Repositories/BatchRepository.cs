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
    public sealed class BatchRepository : Repository<Batch>, IBatchRepository
    {
        private readonly AppDbContext _context;

        public BatchRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Batch>> GetAll()
        {
            return await _context.Batches
                .Include(x=>x.Training).ToListAsync();
        }
    }
}
