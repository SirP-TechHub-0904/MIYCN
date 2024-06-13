using Application.Queries.DashboardQueries;
using Application.Queries.IdentityQueries;
using Domain.DTOs;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.Dashboard.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public AdminDashboardDto Datas { get; private set; }

        public async Task OnGetAsync()
        {
            var query = new GetDashboardQuery();
            Datas = await _mediator.Send(query);
        }

    }
}
