using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SponsorCommand
{
     public sealed class UpdateSponsorCommand : IRequest
    {
        public UpdateSponsorCommand(Sponsor movie)
        {
            Sponsor = movie;
        }

        public Sponsor Sponsor { get; set; }


    }

    public class UpdateSponsorCommandHandler : IRequestHandler<UpdateSponsorCommand>
    {
        private readonly ISponsorRepository _repository;

        public UpdateSponsorCommandHandler(ISponsorRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateSponsorCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.Sponsor);
        }
    }
}
