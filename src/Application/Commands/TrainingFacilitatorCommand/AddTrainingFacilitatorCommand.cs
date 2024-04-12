using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingFacilitatorCommand
{
    public sealed class AddTrainingFacilitatorCommand : IRequest
    {
        public AddTrainingFacilitatorCommand(TrainingFacilitator trainingFacilitator)
        {
            TrainingFacilitator = trainingFacilitator;
        }

        public TrainingFacilitator TrainingFacilitator { get; set; }


    }

    public class AddTrainingFacilitatorCommandHandler : IRequestHandler<AddTrainingFacilitatorCommand>
    {
        private readonly ITrainingFacilitatorRepository _trainingFacilitatorRepository;

        public AddTrainingFacilitatorCommandHandler(ITrainingFacilitatorRepository trainingFacilitatorRepository)
        {
            _trainingFacilitatorRepository = trainingFacilitatorRepository;
        }

        public async Task Handle(AddTrainingFacilitatorCommand request, CancellationToken cancellationToken)
        {
            var check = await _trainingFacilitatorRepository.FacilitatorInTraining(request.TrainingFacilitator.TrainingId, request.TrainingFacilitator.UserId);
            if (check == null)
            {
                await _trainingFacilitatorRepository.AddAsync(request.TrainingFacilitator);
            }


        }
    }
}
