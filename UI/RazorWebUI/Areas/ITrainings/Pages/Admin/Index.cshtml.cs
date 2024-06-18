using Application.Queries.IdentityQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
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

        public IEnumerable<Training> Datas { get; private set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string state = "";
            var usercommand = new GetUserByIdQuery(userId);
            var userinfo = await _mediator.Send(usercommand);
            if(userinfo != null)
            {
                state = userinfo.State;
            }
            if(User.IsInRole("Admin") || User.IsInRole("mSuperAdmin"))
            {
                state = "all";
            }
            var query = new ListTrainingQuery(state);
            Datas = await _mediator.Send(query);
        }

    }
}
