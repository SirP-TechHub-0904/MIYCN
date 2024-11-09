using Domain.DTOs;
using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISupervisorTrainingFormRepository : IRepository<SupervisorTrainingForm>
    {
        Task AddRangeAsync(IEnumerable<SupervisorTrainingForm> entities, CancellationToken cancellationToken);
         Task<Dictionary<long, SupervisorTrainingForm>> GetUserResponsesAsync(string userId, long trainingId);
        Task<List<UserTrainingReportDto>> GetUserWithReportReportsAsync(long trainingId);
    }
}
