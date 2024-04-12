using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingCommand
{
     public sealed class UpdateTrainingCommand : IRequest
    {
        public UpdateTrainingCommand(Training movie)
        {
            Training = movie;
        }

        public Training Training { get; set; }


    }

    public class UpdateTrainingCommandHandler : IRequestHandler<UpdateTrainingCommand>
    {
        private readonly ITrainingRepository _repository;

        public UpdateTrainingCommandHandler(ITrainingRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTrainingCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.Training);
        }
    }
}
