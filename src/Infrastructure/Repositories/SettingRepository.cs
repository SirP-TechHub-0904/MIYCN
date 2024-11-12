using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class SettingRepository : Repository<Setting>, ISettingRepository
    {
        private readonly AppDbContext _context;
          public SettingRepository(AppDbContext context) : base(context)
        {
            _context = context;
           
        }

        public async Task<Setting> GetSetting()
        {
           var getSetting = await _context.Settings.FirstOrDefaultAsync();
            if(getSetting == null)
            {
                Setting setting = new Setting();  
                await _context.Settings.AddAsync(setting);
                await _context.SaveChangesAsync();
               return await _context.Settings.FirstOrDefaultAsync();
            }
            else
            {
                return getSetting;
            }

           
        }

       
    }
}
