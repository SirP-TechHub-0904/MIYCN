using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingFacilitatorQueries
{
    public sealed class GetByIdAndUserIdTrainingFacilitatorQuery : IRequest<TrainingFacilitator>
    {
        public GetByIdAndUserIdTrainingFacilitatorQuery(long id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public long Id { get; }
        public string UserId { get; }

        // Handler
        public class GetByIdAndUserIdTrainingFacilitatorQueryHandler : IRequestHandler<GetByIdAndUserIdTrainingFacilitatorQuery, TrainingFacilitator>
        {

            private readonly ITrainingFacilitatorRepository _repository;

            public GetByIdAndUserIdTrainingFacilitatorQueryHandler(ITrainingFacilitatorRepository repository)
            {
                _repository = repository;
            }
            public async Task<TrainingFacilitator> Handle(GetByIdAndUserIdTrainingFacilitatorQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TrainingFacilitator data = await _repository.GetFacilitatorByIdAndUserId(request.Id, request.UserId);

                return data;
            }
        }
    }

}
