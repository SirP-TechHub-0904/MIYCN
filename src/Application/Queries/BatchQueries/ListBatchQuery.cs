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
        public sealed class ListBatchQuery : IRequest<List<Batch>>
    {
        public class ListBatchQueryHandler : IRequestHandler<ListBatchQuery, List<Batch>>
        {
            private readonly IBatchRepository _repository;

            public ListBatchQueryHandler(IBatchRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Batch>> Handle(ListBatchQuery request, CancellationToken cancellationToken)
            {
                var list = await _repository.GetAll();
                return list.ToList();
            }
        }
    }

}
