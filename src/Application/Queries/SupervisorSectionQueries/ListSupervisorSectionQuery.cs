using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SupervisorSectionQueries
{
    public sealed class ListSupervisorSectionQuery : IRequest<List<SupervisorSection>>
    {
        public class ListSupervisorSectionQueryHandler : IRequestHandler<ListSupervisorSectionQuery, List<SupervisorSection>>
        {
            private readonly ISupervisorSectionRepository _repository;

            public ListSupervisorSectionQueryHandler(ISupervisorSectionRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<SupervisorSection>> Handle(ListSupervisorSectionQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll();

            }
        }
    }

}
