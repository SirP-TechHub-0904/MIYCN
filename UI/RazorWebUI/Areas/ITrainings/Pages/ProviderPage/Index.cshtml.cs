using Application.Queries.IdentityQueries;
using Application.Queries.ProviderQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.ITrainings.Pages.ProviderPage
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

        public IEnumerable<Provider> Datas { get; private set; }

        public async Task OnGetAsync()
        {

            var query = new ListProviderQuery();
            Datas = await _mediator.Send(query);
        }

    }
}
