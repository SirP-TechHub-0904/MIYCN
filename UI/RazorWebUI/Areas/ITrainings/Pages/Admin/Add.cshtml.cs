using Application.Commands.IdentityCommand;
using Application.Commands.TrainingCommand;
using Application.Queries.BatchQueries;
using Application.Queries.ProviderQueries;
using Application.Queries.TrainingCategoryQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
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
        public Training Training { get; set; }
        [BindProperty]
        public long CategoryId { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            
            var getTrainingCategory = new GetByIdTrainingCategoryQuery(id);
            var result = await _mediator.Send(getTrainingCategory);
            if(result == null)
            {
                return RedirectToPage("./Index");
            }
           CategoryId = result.Id;

            ListBatchQuery ListBatchCommand = new ListBatchQuery();
            var Batches = await _mediator.Send(ListBatchCommand);

            ViewData["BatchId"] = new SelectList(Batches, "Id", "Title");



            ListProviderQuery ListProviderCommand = new ListProviderQuery();
            var Providers = await _mediator.Send(ListProviderCommand);

            ViewData["ProviderId"] = new SelectList(Providers, "Id", "Title");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddTrainingCommand Command = new AddTrainingCommand(Training);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index", new { id = Training.TrainingCategoryId});
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }

}
