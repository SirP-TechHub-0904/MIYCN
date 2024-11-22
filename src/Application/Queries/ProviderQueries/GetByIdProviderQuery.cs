using Application.Validators;
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
        public sealed class GetByIdProviderQuery : IRequest<Provider>
    {
        public GetByIdProviderQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdProviderQueryHandler : IRequestHandler<GetByIdProviderQuery, Provider>
        {

            private readonly IProviderRepository _repository;

            public GetByIdProviderQueryHandler(IProviderRepository repository)
            {
                _repository = repository;
            }
            public async Task<Provider> Handle(GetByIdProviderQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Provider data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
