using Application.Validators;
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
    public sealed class GetByIdSupervisorTrainingFormQuery : IRequest<SupervisorTrainingForm>
    {
        public GetByIdSupervisorTrainingFormQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdSupervisorTrainingFormQueryHandler : IRequestHandler<GetByIdSupervisorTrainingFormQuery, SupervisorTrainingForm>
        {

            private readonly ISupervisorTrainingFormRepository _repository;

            public GetByIdSupervisorTrainingFormQueryHandler(ISupervisorTrainingFormRepository repository)
            {
                _repository = repository;
            }
            public async Task<SupervisorTrainingForm> Handle(GetByIdSupervisorTrainingFormQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                SupervisorTrainingForm data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
