using Application.Commands.ProviderCommand;
using Application.Queries.ProviderQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.ProviderPage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    //public class  : PageModel
    public class RemoveModel : PageModel
    {
        private readonly IMediator _mediator;

        public RemoveModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Provider Provider { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdProviderQuery Command = new GetByIdProviderQuery(id);
            Provider = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                DeleteProviderCommand Command = new DeleteProviderCommand(Provider.Id);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
