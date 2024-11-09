using Application.Commands.SupervisorSectionCommand;
using Application.Queries.SupervisorSectionQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Supervisor.ISection
{
    [Microsoft.AspNetCore.Authorization.Authorize]
 
        public class DeleteModel : PageModel
        {
            private readonly IMediator _mediator;

            public DeleteModel(IMediator mediator)
            {
                _mediator = mediator;
            }

            [BindProperty]
            public SupervisorSection SupervisorSection { get; set; }

            public async Task<IActionResult> OnGetAsync(long id)
            {
                if (id < 0)
                {
                    return NotFound();
                }
                GetByIdSupervisorSectionQuery Command = new GetByIdSupervisorSectionQuery(id);
                SupervisorSection = await _mediator.Send(Command);
                return Page();
            }
            public async Task<IActionResult> OnPostAsync()
            {
                try
                {
                    DeleteSupervisorSectionCommand Command = new DeleteSupervisorSectionCommand(SupervisorSection.Id);
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
 