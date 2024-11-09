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
     public sealed class UpdateSupervisorSectionQuestionCommand : IRequest
    {
        public UpdateSupervisorSectionQuestionCommand(SupervisorSectionQuestion movie)
        {
            SupervisorSectionQuestion = movie;
        }

        public SupervisorSectionQuestion SupervisorSectionQuestion { get; set; }


    }

    public class UpdateSupervisorSectionQuestionCommandHandler : IRequestHandler<UpdateSupervisorSectionQuestionCommand>
    {
        private readonly ISupervisorSectionQuestionRepository _repository;

        public UpdateSupervisorSectionQuestionCommandHandler(ISupervisorSectionQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateSupervisorSectionQuestionCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.SupervisorSectionQuestion);
        }
    }
}
