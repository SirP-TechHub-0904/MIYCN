using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Encodings.Web;
using MediatR;
using Application.Commands.IdentityCommand;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public RegisterDto RegisterDto { get; set; }

        [BindProperty]
        public string Role { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddUserCommand regCommand = new AddUserCommand(RegisterDto, Role);
                var response = await _mediator.Send(regCommand);

                return RedirectToPage("./RegistrationStatus", new { id = response.UserId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }

}
