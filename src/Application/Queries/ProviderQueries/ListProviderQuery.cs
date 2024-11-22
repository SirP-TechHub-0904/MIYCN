using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ProviderQueries
{
        public sealed class ListProviderQuery : IRequest<List<Provider>>
    {
        public class ListProviderQueryHandler : IRequestHandler<ListProviderQuery, List<Provider>>
        {
            private readonly IProviderRepository _repository;

            public ListProviderQueryHandler(IProviderRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Provider>> Handle(ListProviderQuery request, CancellationToken cancellationToken)
            {
                var list = await _repository.GetAll();
                return list.ToList();
            }
        }
    }

}
