using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BatchCommand
{
    public sealed class DeleteBatchCommand : IRequest
    {
        public DeleteBatchCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteBatchCommandHandler : IRequestHandler<DeleteBatchCommand>
    {
        private readonly IBatchRepository _repository;

        public DeleteBatchCommandHandler(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteBatchCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
