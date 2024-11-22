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
     public sealed class AddBatchCommand : IRequest
    {
        public AddBatchCommand(Batch batch)
        {
            Batch = batch;
        }

        public Batch Batch { get; set; }


    }

    public class AddBatchCommandHandler : IRequestHandler<AddBatchCommand>
    {
        private readonly IBatchRepository _batchRepository;

        public AddBatchCommandHandler(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public async Task Handle(AddBatchCommand request, CancellationToken cancellationToken)
        {

            await _batchRepository.AddAsync(request.Batch);


        }
    }
}
