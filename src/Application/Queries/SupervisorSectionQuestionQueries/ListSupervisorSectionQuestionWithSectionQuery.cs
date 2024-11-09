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
    public sealed class ListSupervisorSectionQuestionWithSectionQuery : IRequest<List<SupervisorSectionQuestion>>
    {
        public class ListSupervisorSectionQuestionWithSectionQueryHandler : IRequestHandler<ListSupervisorSectionQuestionWithSectionQuery, List<SupervisorSectionQuestion>>
        {
            private readonly ISupervisorSectionQuestionRepository _repository;

            public ListSupervisorSectionQuestionWithSectionQueryHandler(ISupervisorSectionQuestionRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<SupervisorSectionQuestion>> Handle(ListSupervisorSectionQuestionWithSectionQuery request, CancellationToken cancellationToken)
            {
                return await _repository.ListQuestions();

            }
        }
    }

}
