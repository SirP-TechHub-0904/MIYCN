using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Infrastructure.Repositories
{
    public sealed class UserTestRepository : Repository<UserTest>, IUserTestRepository
    {
        private readonly AppDbContext _context;

        public UserTestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfUserTookTest(string userid, long trainingId, int testType)
        {
            TrainingTestType status = (TrainingTestType)Enum.ToObject(typeof(TrainingTestType), testType);
            var data = await _context.UserTests.Where(x => x.TrainingId == trainingId && x.UserId == userid && x.Submitted == true 
            && x.TrainingTest.TrainingTestType == status).FirstOrDefaultAsync();
            if (data != null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserTestResultDto> UserTestResult(long trainingId, string userId, int testType)
        {

            TrainingTestType status = (TrainingTestType)Enum.ToObject(typeof(TrainingTestType), testType);
            UserTestResultDto result = new UserTestResultDto();
             var userresult = await _context.UserTests
                .Include(x=>x.TrainingTest)
                .Where(x=>x.TrainingId == trainingId&& x.UserId == userId && x.TrainingTest.TrainingTestType == status).ToListAsync();

            result.UserTest = userresult;

            if (userresult.Count > 0)
            {
                int totalQuestions = userresult.Count;
                int correctAnswers = userresult.Count(x => x.Answer == x.TrainingTest.CorrectAnswer);

                double percentage = (double)correctAnswers / totalQuestions * 100;
                result.TotalQuestions = totalQuestions;
                result.PercentageResult = percentage;
            }

            

            return result;
        }

        public async Task UserTestSubmit(List<(long questionId, int answer, string userId, long trainingId)> assessmentData)
        {
            foreach (var (questionId, answer, userId, trainingId) in assessmentData)
            {
                UserTest nxtest = new UserTest();
                nxtest.UserId = userId;
                nxtest.TrainingId = trainingId;
                nxtest.Answer = answer;
                nxtest.TrainingTestId = questionId;
                nxtest.Submitted = true;
                await _context.UserTests.AddAsync(nxtest);
            }


            await _context.SaveChangesAsync();

        }
    }
}
