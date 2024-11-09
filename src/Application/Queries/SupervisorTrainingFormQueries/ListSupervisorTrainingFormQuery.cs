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
    public sealed class ListSupervisorTrainingFormQuery : IRequest<List<SupervisorTrainingForm>>
    {
        public class ListSupervisorTrainingFormQueryHandler : IRequestHandler<ListSupervisorTrainingFormQuery, List<SupervisorTrainingForm>>
        {
            private readonly ISupervisorTrainingFormRepository _repository;

            public ListSupervisorTrainingFormQueryHandler(ISupervisorTrainingFormRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<SupervisorTrainingForm>> Handle(ListSupervisorTrainingFormQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
