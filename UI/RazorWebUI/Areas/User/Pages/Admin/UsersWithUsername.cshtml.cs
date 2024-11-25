using Application.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class UsersWithUsernameModel : PageModel
    {
        private readonly ILogger<UsersWithUsernameModel> _logger;
        private readonly IMediator _mediator;

        public UsersWithUsernameModel(ILogger<UsersWithUsernameModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<ProfileDto> Datas { get; private set; }

        public async Task OnGetAsync()
        {
            var query = new GetUserListByDto();
            Datas = await _mediator.Send(query);
        }
    }
}
