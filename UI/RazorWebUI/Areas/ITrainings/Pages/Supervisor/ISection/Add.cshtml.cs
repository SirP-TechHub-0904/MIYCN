using Application.Commands.TrainingCommand;
using Application.Commands.SupervisorSectionCommand;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Supervisor.ISection
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public SupervisorSection SupervisorSection { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddSupervisorSectionCommand Command = new AddSupervisorSectionCommand(SupervisorSection);
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
