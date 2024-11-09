using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SupervisorSectionQuestionCommand
{
    public sealed class AddSupervisorSectionQuestionCommand : IRequest
    {
        public AddSupervisorSectionQuestionCommand(SupervisorSectionQuestion moduleTopic)
        {
            SupervisorSectionQuestion = moduleTopic;
        }

        public SupervisorSectionQuestion SupervisorSectionQuestion { get; set; }


    }

    public class AddSupervisorSectionQuestionCommandHandler : IRequestHandler<AddSupervisorSectionQuestionCommand>
    {
        private readonly ISupervisorSectionQuestionRepository _moduleTopicRepository;

        public AddSupervisorSectionQuestionCommandHandler(ISupervisorSectionQuestionRepository moduleTopicRepository)
        {
            _moduleTopicRepository = moduleTopicRepository;
        }

        public async Task Handle(AddSupervisorSectionQuestionCommand request, CancellationToken cancellationToken)
        {

            await _moduleTopicRepository.AddAsync(request.SupervisorSectionQuestion);


        }
    }
}
