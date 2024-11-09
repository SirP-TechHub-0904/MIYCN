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
    public sealed class AddSupervisorTrainingFormCommand : IRequest
    {
       
        public List<SupervisorTrainingForm> TrainingForms { get; }

        public AddSupervisorTrainingFormCommand(List<SupervisorTrainingForm> trainingForms)
        {
            TrainingForms = trainingForms;
        }
    }

    public class AddSupervisorTrainingFormCommandHandler : IRequestHandler<AddSupervisorTrainingFormCommand>
    {
        private readonly ISupervisorTrainingFormRepository _moduleTopicRepository;

        public AddSupervisorTrainingFormCommandHandler(ISupervisorTrainingFormRepository moduleTopicRepository)
        {
            _moduleTopicRepository = moduleTopicRepository;
        }

        public async Task Handle(AddSupervisorTrainingFormCommand request, CancellationToken cancellationToken)
        {

            await _moduleTopicRepository.AddRangeAsync(request.TrainingForms, cancellationToken);


        }
    }
}
