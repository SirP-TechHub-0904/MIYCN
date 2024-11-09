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
     public sealed class GetUserResponsesQuery : IRequest<Dictionary<long, SupervisorTrainingForm>>
    {
        public string UserId { get; set; }
        public long TrainingId { get; set; }

        public GetUserResponsesQuery(string userId, long trainingId)
        {
            UserId = userId;
            TrainingId = trainingId;
        }

        public class GetUserResponsesQueryHandler : IRequestHandler<GetUserResponsesQuery, Dictionary<long, SupervisorTrainingForm>>
        {
            private readonly ISupervisorTrainingFormRepository _repository;

            public GetUserResponsesQueryHandler(ISupervisorTrainingFormRepository repository)
            {
                _repository = repository;
            }

            public async Task<Dictionary<long, SupervisorTrainingForm>> Handle(GetUserResponsesQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetUserResponsesAsync(request.UserId, request.TrainingId);
            }
        }
    }

}
