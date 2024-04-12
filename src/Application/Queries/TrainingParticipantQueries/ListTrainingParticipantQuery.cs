using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingParticipantQueries
{
       public sealed class ListTrainingParticipantQuery : IRequest<List<TrainingParticipant>>
    {
        public class ListTrainingParticipantQueryHandler : IRequestHandler<ListTrainingParticipantQuery, List<TrainingParticipant>>
        {
            private readonly ITrainingParticipantRepository _repository;

            public ListTrainingParticipantQueryHandler(ITrainingParticipantRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TrainingParticipant>> Handle(ListTrainingParticipantQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
