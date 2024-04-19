using Application.Commands.EmailCommand;
using Application.Queries.IdentityQueries;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;
using PostmarkEmailService;
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
        private readonly IMediator _mediator;
        private readonly ITrainingRepository _trainingRepository;
        public AddTrainingFacilitatorCommandHandler(ITrainingFacilitatorRepository trainingFacilitatorRepository, IMediator mediator, ITrainingRepository trainingRepository)
        {
            _trainingFacilitatorRepository = trainingFacilitatorRepository;
            _mediator = mediator;
            _trainingRepository = trainingRepository;
        }

        public async Task Handle(AddTrainingFacilitatorCommand request, CancellationToken cancellationToken)
        {
            var check = await _trainingFacilitatorRepository.FacilitatorInTraining(request.TrainingFacilitator.TrainingId, request.TrainingFacilitator.UserId);
            if (check == null)
            {
                try
                {
                    var result = await _trainingFacilitatorRepository.AddFacilitator(request.TrainingFacilitator);
                    if (result == true)
                    {
                        var getTraining = await _trainingRepository.GetByIdAsync(request.TrainingFacilitator.TrainingId);

                        string messagedetails = $"Your have been added to a <b>{getTraining.Title}</b> as a facilitator<br><br>" +
                                   $"Kindly check your dashboard for more details.<br><br>";

                        GetUserByIdQuery getUser = new GetUserByIdQuery(request.TrainingFacilitator.UserId);
                        var userdata = await _mediator.Send(getUser);
                        //send email
                        MessageDto msn = new MessageDto();
                        msn.Email = userdata.Email;
                        msn.Message = messagedetails;
                        msn.Subject = "MIYCN TRAINING";
                        msn.Name = userdata.FullnameX;
                        SendMessageCommand emailcommand = new SendMessageCommand(msn);
                        PostmarkResponse responseemail = await _mediator.Send(emailcommand);
                    }
                }
                catch (Exception ex) { }
            }



        }
    }
}
