using Application.Queries.SupervisorSectionQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Supervisor.ISection
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<SupervisorSection> Datas { get; private set; }

        public async Task OnGetAsync()
        {
            var query = new ListSupervisorSectionQuery();
            Datas = await _mediator.Send(query);
        }
    }
}
