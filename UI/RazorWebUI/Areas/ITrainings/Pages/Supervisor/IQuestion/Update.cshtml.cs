using Application.Commands.TrainingCommand;
using Application.Commands.SupervisorSectionQuestionCommand;
using Application.Queries.TrainingQueries;
using Application.Queries.SupervisorSectionQuestionQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Queries.IdentityQueries;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Queries.SupervisorSectionQueries;

namespace RazorWebUI.Areas.ITrainings.Pages.Supervisor.IQuestion
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public SupervisorSectionQuestion SupervisorSectionQuestion { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdSupervisorSectionQuestionQuery Command = new GetByIdSupervisorSectionQuestionQuery(id);
            SupervisorSectionQuestion = await _mediator.Send(Command);


            ListSupervisorSectionQuery xCommand = new ListSupervisorSectionQuery();
            var supervisorsection = await _mediator.Send(xCommand);
             

            ViewData["SupervisorSection"] = new SelectList(supervisorsection, "Id", "Title");

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateSupervisorSectionQuestionCommand Command = new UpdateSupervisorSectionQuestionCommand(SupervisorSectionQuestion);
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
