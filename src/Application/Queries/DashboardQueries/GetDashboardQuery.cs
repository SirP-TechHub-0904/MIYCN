using Application.Validators;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DashboardQueries
{
    public sealed class GetDashboardQuery : IRequest<AdminDashboardDto>
    {
         
        public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, AdminDashboardDto>
        {

            private readonly IDashboardRepository _repository;

            public GetDashboardQueryHandler(IDashboardRepository repository)
            {
                _repository = repository;
            }
            public async Task<AdminDashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                AdminDashboardDto data = await _repository.AdminDashboardData();

                return data;
            }
        }
    }
}
