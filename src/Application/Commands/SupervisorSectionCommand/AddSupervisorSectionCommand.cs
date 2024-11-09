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
    public sealed class AddSupervisorSectionCommand : IRequest
    {
        public AddSupervisorSectionCommand(SupervisorSection moduleTopic)
        {
            SupervisorSection = moduleTopic;
        }

        public SupervisorSection SupervisorSection { get; set; }


    }

    public class AddSupervisorSectionCommandHandler : IRequestHandler<AddSupervisorSectionCommand>
    {
        private readonly ISupervisorSectionRepository _moduleTopicRepository;

        public AddSupervisorSectionCommandHandler(ISupervisorSectionRepository moduleTopicRepository)
        {
            _moduleTopicRepository = moduleTopicRepository;
        }

        public async Task Handle(AddSupervisorSectionCommand request, CancellationToken cancellationToken)
        {

            await _moduleTopicRepository.AddAsync(request.SupervisorSection);


        }
    }
}
