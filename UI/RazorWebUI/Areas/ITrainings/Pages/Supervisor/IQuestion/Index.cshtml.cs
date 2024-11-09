using Application.Queries.SupervisorSectionQuestionQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Supervisor.IQuestion
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

        public IEnumerable<SupervisorSectionQuestion> Datas { get; private set; }

        public async Task OnGetAsync()
        {
            var query = new ListSupervisorSectionQuestionWithSectionQuery();
            Datas = await _mediator.Send(query);
        }
    }
}
