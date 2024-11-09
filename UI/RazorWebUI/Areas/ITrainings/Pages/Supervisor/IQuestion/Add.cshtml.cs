using Application.Commands.TrainingCommand;
using Application.Commands.SupervisorSectionQuestionCommand;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Queries.SupervisorSectionQueries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorWebUI.Areas.ITrainings.Pages.Supervisor.IQuestion
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
        public SupervisorSectionQuestion SupervisorSectionQuestion { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            ListSupervisorSectionQuery xCommand = new ListSupervisorSectionQuery();
            var supervisorsection = await _mediator.Send(xCommand);


            ViewData["SupervisorSection"] = new SelectList(supervisorsection, "Id", "Title");

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddSupervisorSectionQuestionCommand Command = new AddSupervisorSectionQuestionCommand(SupervisorSectionQuestion);
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
