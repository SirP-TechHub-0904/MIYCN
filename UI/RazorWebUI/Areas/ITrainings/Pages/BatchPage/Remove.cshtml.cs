using Application.Commands.BatchCommand;
using Application.Queries.BatchQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.BatchPage
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
        public Batch Batch { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdBatchQuery Command = new GetByIdBatchQuery(id);
            Batch = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                DeleteBatchCommand Command = new DeleteBatchCommand(Batch.Id);
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
