using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProviderCommand
{
     public sealed class AddProviderCommand : IRequest
    {
        public AddProviderCommand(Provider provider)
        {
            Provider = provider;
        }

        public Provider Provider { get; set; }


    }

    public class AddProviderCommandHandler : IRequestHandler<AddProviderCommand>
    {
        private readonly IProviderRepository _providerRepository;

        public AddProviderCommandHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task Handle(AddProviderCommand request, CancellationToken cancellationToken)
        {

            await _providerRepository.AddAsync(request.Provider);


        }
    }
}
