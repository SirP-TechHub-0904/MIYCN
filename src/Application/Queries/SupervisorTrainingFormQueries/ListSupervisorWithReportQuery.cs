using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SupervisorTrainingFormQueries
{
    public sealed class ListSupervisorWithReportQuery : IRequest<List<UserTrainingReportDto>>
    {

        public long TrainingId { get; set; }

        public ListSupervisorWithReportQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        public class ListSupervisorWithReportQueryHandler : IRequestHandler<ListSupervisorWithReportQuery, List<UserTrainingReportDto>>
        {
            private readonly ISupervisorTrainingFormRepository _repository;

            public ListSupervisorWithReportQueryHandler(ISupervisorTrainingFormRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<UserTrainingReportDto>> Handle(ListSupervisorWithReportQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetUserWithReportReportsAsync(request.TrainingId);

            }
        }
    }

}
