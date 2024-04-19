using Application.Commands.EmailCommand;
using Application.Queries.IdentityQueries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using PostmarkEmailService;
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
        private readonly IMediator _mediator;
        private readonly ITrainingRepository _trainingRepository;
        public AddTrainingParticipantCommandHandler(ITrainingParticipantRepository trainingParticipantRepository, IMediator mediator, ITrainingRepository trainingRepository)
        {
            _trainingParticipantRepository = trainingParticipantRepository;
            _mediator = mediator;
            _trainingRepository = trainingRepository;
        }

        public async Task Handle(AddTrainingParticipantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _trainingParticipantRepository.AddParticipant(request.TrainingParticipant);
                if (result == true)
                {
                    var getTraining = await _trainingRepository.GetByIdAsync(request.TrainingParticipant.TrainingId);

                    string messagedetails = $"Your have been added to a <b>{getTraining.Title}</b> as a Perticipant<br><br>" +
                               $"Kindly check your dashboard for more details.<br><br>";

                    GetUserByIdQuery getUser = new GetUserByIdQuery(request.TrainingParticipant.UserId);
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
            catch (Exception ex)
            {

            }

        }
    }
}
