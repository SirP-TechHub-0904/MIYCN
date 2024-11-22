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
    public sealed class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        private readonly AppDbContext _context;

        public ProviderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Provider>> GetAll()
        {
            return await _context.Providers
                .Include(x=>x.Training).ToListAsync();
        }
    }
}
