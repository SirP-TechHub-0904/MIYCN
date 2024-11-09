using Application.Commands.SupervisorSectionCommand;
using Application.Commands.SupervisorSectionQuestionCommand;
using Application.Queries.SupervisorSectionQueries;
using Application.Queries.SupervisorSectionQuestionQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Supervisor.IQuestion
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    [Microsoft.AspNetCore.Authorization.Authorize]

    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
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
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                DeleteSupervisorSectionQuestionCommand Command = new DeleteSupervisorSectionQuestionCommand(SupervisorSectionQuestion.Id);
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
