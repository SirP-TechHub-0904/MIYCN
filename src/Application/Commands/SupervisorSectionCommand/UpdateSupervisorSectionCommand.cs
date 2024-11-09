using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SupervisorSectionCommand
{
     public sealed class UpdateSupervisorSectionCommand : IRequest
    {
        public UpdateSupervisorSectionCommand(SupervisorSection movie)
        {
            SupervisorSection = movie;
        }

        public SupervisorSection SupervisorSection { get; set; }


    }

    public class UpdateSupervisorSectionCommandHandler : IRequestHandler<UpdateSupervisorSectionCommand>
    {
        private readonly ISupervisorSectionRepository _repository;

        public UpdateSupervisorSectionCommandHandler(ISupervisorSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateSupervisorSectionCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.SupervisorSection);
        }
    }
}
