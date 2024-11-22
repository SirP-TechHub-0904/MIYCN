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
     public sealed class UpdateProviderCommand : IRequest
    {
        public UpdateProviderCommand(Provider provider)
        {
            Provider = provider;
        }

        public Provider Provider { get; set; }


    }

    public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand>
    {
        private readonly IProviderRepository _repository;

        public UpdateProviderCommandHandler(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.Provider);
        }
    }
}
