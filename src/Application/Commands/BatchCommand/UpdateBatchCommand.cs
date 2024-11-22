using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BatchCommand
{
     public sealed class UpdateBatchCommand : IRequest
    {
        public UpdateBatchCommand(Batch batch)
        {
            Batch = batch;
        }

        public Batch Batch { get; set; }


    }

    public class UpdateBatchCommandHandler : IRequestHandler<UpdateBatchCommand>
    {
        private readonly IBatchRepository _repository;

        public UpdateBatchCommandHandler(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateBatchCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.Batch);
        }
    }
}
