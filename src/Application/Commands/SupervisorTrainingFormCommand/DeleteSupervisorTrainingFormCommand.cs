using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SupervisorTrainingFormCommand
{
    public sealed class DeleteSupervisorTrainingFormCommand : IRequest
    {
        public DeleteSupervisorTrainingFormCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteSupervisorTrainingFormCommandHandler : IRequestHandler<DeleteSupervisorTrainingFormCommand>
    {
        private readonly ISupervisorTrainingFormRepository _repository;

        public DeleteSupervisorTrainingFormCommandHandler(ISupervisorTrainingFormRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSupervisorTrainingFormCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
