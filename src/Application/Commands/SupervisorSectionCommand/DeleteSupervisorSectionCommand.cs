using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SupervisorSectionCommand
{
    public sealed class DeleteSupervisorSectionCommand : IRequest
    {
        public DeleteSupervisorSectionCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteSupervisorSectionCommandHandler : IRequestHandler<DeleteSupervisorSectionCommand>
    {
        private readonly ISupervisorSectionRepository _repository;

        public DeleteSupervisorSectionCommandHandler(ISupervisorSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSupervisorSectionCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
