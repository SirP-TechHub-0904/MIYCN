using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DialyActivityCommand
{
    public sealed class AddDialyActivityCommand : IRequest
    {
        public AddDialyActivityCommand(DialyActivity dialyActivity)
        {
            DialyActivity = dialyActivity;
        }

        public DialyActivity DialyActivity { get; set; }


    }

    public class AddDialyActivityCommandHandler : IRequestHandler<AddDialyActivityCommand>
    {
        private readonly IDialyActivityRepository _dialyActivityRepository;

        public AddDialyActivityCommandHandler(IDialyActivityRepository dialyActivityRepository)
        {
            _dialyActivityRepository = dialyActivityRepository;
        }

        public async Task Handle(AddDialyActivityCommand request, CancellationToken cancellationToken)
        {
            var check = await _dialyActivityRepository.GetActivityByTrainingIdAndDate(request.DialyActivity.TrainingId, request.DialyActivity.Date);
            if (check == null)
            {
                await _dialyActivityRepository.AddAsync(request.DialyActivity);
            }

        }
    }
}
