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
using static Domain.Models.EnumStatus;

namespace Infrastructure.Repositories
{
     public sealed class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Attendance>> GetAttendanceByActivity(long activityId)
        {
            var list = await _context.Attendances
                .Include(x=>x.User)
                .Where(x=>x.DialyActivityId == activityId).ToListAsync();
            return list;
        }

        public async Task<List<Attendance>> GetAttendanceByTraining(long trainingId)
        {
            var list = await _context.Attendances
                 .Include(x => x.User)
                .Include(x=>x.DialyActivity).Where(x => x.DialyActivity.TrainingId == trainingId).ToListAsync();
            return list;
        }

        public async Task ValidateUserToTrainingAttendance(long trainingId)
        {
            // Retrieve the training
            var training = await _context.Trainings.FindAsync(trainingId);
            if (training == null)
            {
                throw new ArgumentException("Invalid training ID");
            }

            // Get all participants of the training
            var participants = await _context.TrainingParticipants
                .Where(tp => tp.TrainingId == trainingId)
                .Include(tp => tp.User)
                .ToListAsync();

            // Get the dialy activities associated with the training
            var dialyActivities = await _context.DialyActivities
                .Where(da => da.TrainingId == trainingId)
                .Include(da => da.Attendances)
                .ToListAsync();

            // Iterate over each participant
            foreach (var participant in participants)
            {
                // Check if the participant has already attended the activity on a previous date
                foreach (var activity in dialyActivities)
                {
                    // Skip activities whose date has already passed
                    //if (activity.Date <= DateTime.Today)
                    //{
                    //    Console.WriteLine($"Activity on {activity.Date} has already passed.");
                    //    continue;
                    //}

                    var checkexistance = activity.Attendances
                        .FirstOrDefault(a => a.UserId == participant.UserId && a.DialyActivityId == activity.Id);

                    if (checkexistance != null)
                    {
                        // Participant has attended the activity on a previous date
                        Console.WriteLine($"User {participant.User.UserName} has already attended activity on {checkexistance.DialyActivity.Date}");
                        continue; // Skip adding this participant to the attendance
                    }

                    // Participant has not attended the activity on any previous date, add to attendance
                    activity.Attendances.Add(new Attendance
                    {
                        UserId = participant.UserId,
                        DialyActivityId = activity.Id,
                        AttendanceStatus = AttendanceStatus.NILL // or any other status
                    });
                }
            }

            await _context.SaveChangesAsync(); // Save changes to the database
        }

        public async Task UpdateAttendanceStatus(List<(long attendanceId, AttendanceStatus status)> attendanceData)
        {
            foreach (var (attendanceId, status) in attendanceData)
            {
                // Retrieve the Attendance record by its ID
                var attendance = await _context.Attendances.FindAsync(attendanceId);
                if (attendance != null)
                {
                    // Update the AttendanceStatus
                    attendance.AttendanceStatus = status;
                    _context.Attach(attendance).State = EntityState.Modified;
                     
                }
                else
                {
                    // Handle the case where the provided attendanceId is not found
                    throw new ArgumentException($"Attendance record with ID {attendanceId} not found.");
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }


    }
}
