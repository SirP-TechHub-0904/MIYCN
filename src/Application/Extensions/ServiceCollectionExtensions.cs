﻿using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using XYZSMS.Services;
using PostmarkEmailService;
using AWS.Services;


namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppConnection") ?? throw new InvalidOperationException("Connection string 'MovieConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString));
            //
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));

            services.AddTransient<PostmarkClient>(_ => new PostmarkClient(configuration.GetSection("PostmarkSettings")["ServerToken"]));
            // Register StorageService
            services.AddTransient<IStorageService, StorageService>();

            services.AddTransient<IAttendanceRepository, AttendanceRepository>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<IAttendanceRepository, AttendanceRepository>();
            services.AddTransient<IDialyActivityRepository, DialyActivityRepository>();
            services.AddTransient<IEducationRepository, EducationRepository>();
            services.AddTransient<IExperienceRepository, ExperienceRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IModuleTopicRepository, ModuleTopicRepository>();
            services.AddTransient<ISponsorRepository, SponsorRepository>();
            services.AddTransient<ITestCategoryRepository, TestCategoryRepository>();
            services.AddTransient<ITestSheetRepository, TestSheetRepository>();
            services.AddTransient<ITrainingRepository, TrainingRepository>();
            services.AddTransient<ITrainingFacilitatorRepository, TrainingFacilitatorRepository>();
            services.AddTransient<ITrainingParticipantRepository, TrainingParticipantRepository>();
            services.AddTransient<IProfileCategoryListRepository, ProfileCategoryListRepository>();
            services.AddTransient<IProfileCategoryRepository, ProfileCategoryRepository>();
            services.AddTransient<ICertificateRepository, CertificateRepository>();
            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<ITrainingTestRepository, TrainingTestRepository>();
            services.AddTransient<IUserTestRepository, UserTestRepository>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;


            });
            return services;
        }
    }
}
