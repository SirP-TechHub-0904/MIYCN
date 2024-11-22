using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProviderCommand
{
    public sealed class DeleteProviderCommand : IRequest
    {
        public DeleteProviderCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand>
    {
        private readonly IProviderRepository _repository;

        public DeleteProviderCommandHandler(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
