using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProfileCategory> ProfileCategories { get; set; }
        public DbSet<ProfileCategoryList> ProfileCategoryLists { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<DialyActivity> DialyActivities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleTopic> ModuleTopics { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<TestCategory> TestCategories { get; set; }
        public DbSet<TestSheet> TestSheets { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingFacilitator> TrainingFacilitators { get; set; }
        public DbSet<TrainingParticipant> TrainingParticipants { get; set; }
    }
}
