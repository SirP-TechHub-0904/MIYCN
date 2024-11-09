using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SupervisorTrainingFormCommand
{
     public sealed class UpdateSupervisorTrainingFormCommand : IRequest
    {
        public UpdateSupervisorTrainingFormCommand(SupervisorTrainingForm movie)
        {
            SupervisorTrainingForm = movie;
        }

        public SupervisorTrainingForm SupervisorTrainingForm { get; set; }


    }

    public class UpdateSupervisorTrainingFormCommandHandler : IRequestHandler<UpdateSupervisorTrainingFormCommand>
    {
        private readonly ISupervisorTrainingFormRepository _repository;

        public UpdateSupervisorTrainingFormCommandHandler(ISupervisorTrainingFormRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateSupervisorTrainingFormCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.SupervisorTrainingForm);
        }
    }
}
