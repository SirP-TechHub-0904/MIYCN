using Application.Validators;
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
    public sealed class GetByIdSupervisorSectionQuery : IRequest<SupervisorSection>
    {
        public GetByIdSupervisorSectionQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdSupervisorSectionQueryHandler : IRequestHandler<GetByIdSupervisorSectionQuery, SupervisorSection>
        {

            private readonly ISupervisorSectionRepository _repository;

            public GetByIdSupervisorSectionQueryHandler(ISupervisorSectionRepository repository)
            {
                _repository = repository;
            }
            public async Task<SupervisorSection> Handle(GetByIdSupervisorSectionQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                SupervisorSection data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
