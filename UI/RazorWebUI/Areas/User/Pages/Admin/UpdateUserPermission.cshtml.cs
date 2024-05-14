using Amazon;
using Application.Commands.IdentityCommand;
using Application.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Admin
{
     
    public class UpdateUserPermissionModel : PageModel
    {


        private readonly ILogger<InfoModel> _logger;
        private readonly IMediator _mediator;

        public UpdateUserPermissionModel(ILogger<InfoModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public UserRolesDto UserDatas { get; private set; }

        public async Task<ActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetUserPermissionListQuery(id);
            UserDatas = await _mediator.Send(query);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, string userId, string fullname)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID and Role ID must be provided");
            } 
            var command = new UpdateUserRoleCommand(userId, id, fullname);
            string userid = await _mediator.Send(command);
            return RedirectToPage("./UpdateUserPermission", new { id = userid });
        }

    }
}
