using Domain.DTOs;
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
     
    public sealed class SupervisorTrainingFormRepository : Repository<SupervisorTrainingForm>, ISupervisorTrainingFormRepository
    {
        private readonly AppDbContext _context;

        public SupervisorTrainingFormRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task AddRangeAsync(IEnumerable<SupervisorTrainingForm> entities, CancellationToken cancellationToken)
        {
            // Load all existing records that match the UserId, TrainingId, and SupervisorSectionQuestionId in the entities collection
            var existingRecords = await _context.SupervisorTrainingForms
                .Where(e => entities.Select(newEntity => newEntity.UserId).Contains(e.UserId) &&
                            entities.Select(newEntity => newEntity.TrainingId).Contains(e.TrainingId) &&
                            entities.Select(newEntity => newEntity.SupervisorSectionQuestionId).Contains(e.SupervisorSectionQuestionId))
                .ToListAsync(cancellationToken);

            var processedEntities = new List<SupervisorTrainingForm>();

            // Process each entity
            foreach (var entity in entities)
            {
                var existingRecord = existingRecords.FirstOrDefault(existing =>
                    existing.UserId == entity.UserId &&
                    existing.TrainingId == entity.TrainingId &&
                    existing.SupervisorSectionQuestionId == entity.SupervisorSectionQuestionId);

                if (existingRecord != null)
                {
                    // Update existing record
                    existingRecord.Answer = entity.Answer ?? existingRecord.Answer;
                    existingRecord.Remark = entity.Remark ?? existingRecord.Remark;
                    existingRecord.ColumnOne = entity.ColumnOne ?? existingRecord.ColumnOne;
                    existingRecord.ColumnTwo = entity.ColumnTwo ?? existingRecord.ColumnTwo;
                    existingRecord.ColumnThree = entity.ColumnThree ?? existingRecord.ColumnThree;
                    existingRecord.ColumnFour = entity.ColumnFour ?? existingRecord.ColumnFour;
                    existingRecord.LastUpdateDate = DateTime.UtcNow;

                    processedEntities.Add(existingRecord);
                }
                else
                {
                    // Add new record if it doesn't exist
                    processedEntities.Add(entity);
                }
            }

            // Update the context with changes
            _context.SupervisorTrainingForms.UpdateRange(processedEntities);

            // Save changes to the database
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<UserTrainingReportDto>> GetUserWithReportReportsAsync(long trainingId)
        {
            var userReports = await _context.SupervisorTrainingForms
                .Where(r => r.TrainingId == trainingId)
                .Select(r => new UserTrainingReportDto
                {
                    UserId = r.UserId,
                    FullName = r.User.FullnameX, // Ensure FullName is a property in your AppUser model
                    Email = r.User.Email,
                    TrainingId = r.TrainingId ?? 0
                })
                .Distinct()
                .ToListAsync();

            return userReports;
        }

        public async Task<Dictionary<long, SupervisorTrainingForm>> GetUserResponsesAsync(string userId, long trainingId)
        {
            return await _context.SupervisorTrainingForms
                .Where(r => r.UserId == userId && r.TrainingId == trainingId)
                .ToDictionaryAsync(r => r.SupervisorSectionQuestionId);
        }
    }
}
