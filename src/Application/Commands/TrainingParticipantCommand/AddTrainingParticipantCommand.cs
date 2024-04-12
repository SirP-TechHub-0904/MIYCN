using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingParticipantCommand
{
      public sealed class AddTrainingParticipantCommand : IRequest
    {
        public AddTrainingParticipantCommand(TrainingParticipant trainingParticipant)
        {
            TrainingParticipant = trainingParticipant;
        }

        public TrainingParticipant TrainingParticipant { get; set; }


    }

    public class AddTrainingParticipantCommandHandler : IRequestHandler<AddTrainingParticipantCommand>
    {
        private readonly ITrainingParticipantRepository _trainingParticipantRepository;

        public AddTrainingParticipantCommandHandler(ITrainingParticipantRepository trainingParticipantRepository)
        {
            _trainingParticipantRepository = trainingParticipantRepository;
        }

        public async Task Handle(AddTrainingParticipantCommand request, CancellationToken cancellationToken)
        {

            await _trainingParticipantRepository.AddAsync(request.TrainingParticipant);


        }
    }
}
