using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SupervisorSectionQuestionCommand
{
    public sealed class DeleteSupervisorSectionQuestionCommand : IRequest
    {
        public DeleteSupervisorSectionQuestionCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteSupervisorSectionQuestionCommandHandler : IRequestHandler<DeleteSupervisorSectionQuestionCommand>
    {
        private readonly ISupervisorSectionQuestionRepository _repository;

        public DeleteSupervisorSectionQuestionCommandHandler(ISupervisorSectionQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSupervisorSectionQuestionCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
