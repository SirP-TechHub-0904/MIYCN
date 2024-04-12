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
      public sealed class AddSponsorCommand : IRequest
    {
        public AddSponsorCommand(Sponsor sponsor)
        {
            Sponsor = sponsor;
        }

        public Sponsor Sponsor { get; set; }


    }

    public class AddSponsorCommandHandler : IRequestHandler<AddSponsorCommand>
    {
        private readonly ISponsorRepository _sponsorRepository;

        public AddSponsorCommandHandler(ISponsorRepository sponsorRepository)
        {
            _sponsorRepository = sponsorRepository;
        }

        public async Task Handle(AddSponsorCommand request, CancellationToken cancellationToken)
        {

            await _sponsorRepository.AddAsync(request.Sponsor);


        }
    }
}
