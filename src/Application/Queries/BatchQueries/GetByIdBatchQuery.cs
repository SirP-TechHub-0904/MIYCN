using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.BatchQueries
{
        public sealed class GetByIdBatchQuery : IRequest<Batch>
    {
        public GetByIdBatchQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdBatchQueryHandler : IRequestHandler<GetByIdBatchQuery, Batch>
        {

            private readonly IBatchRepository _repository;

            public GetByIdBatchQueryHandler(IBatchRepository repository)
            {
                _repository = repository;
            }
            public async Task<Batch> Handle(GetByIdBatchQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Batch data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
