using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SupervisorSectionQuestionQueries
{
    public sealed class GetByIdSupervisorSectionQuestionQuery : IRequest<SupervisorSectionQuestion>
    {
        public GetByIdSupervisorSectionQuestionQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdSupervisorSectionQuestionQueryHandler : IRequestHandler<GetByIdSupervisorSectionQuestionQuery, SupervisorSectionQuestion>
        {

            private readonly ISupervisorSectionQuestionRepository _repository;

            public GetByIdSupervisorSectionQuestionQueryHandler(ISupervisorSectionQuestionRepository repository)
            {
                _repository = repository;
            }
            public async Task<SupervisorSectionQuestion> Handle(GetByIdSupervisorSectionQuestionQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                SupervisorSectionQuestion data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
