using Application.Commands.TrainingFacilitatorCommand;
using Application.Queries.TrainingFacilitatorQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateFacilitatorAccountModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateFacilitatorAccountModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingFacilitator TrainingFacilitator { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdTrainingFacilitatorQuery Command = new GetByIdTrainingFacilitatorQuery(id);
            TrainingFacilitator = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //get
                GetByIdTrainingFacilitatorQuery xCommand = new GetByIdTrainingFacilitatorQuery(TrainingFacilitator.Id);
                var xtrainingFacilitator = await _mediator.Send(xCommand);


                //update
                xtrainingFacilitator.Position = TrainingFacilitator.Position;
                UpdateTrainingFacilitatorCommand Command = new UpdateTrainingFacilitatorCommand(xtrainingFacilitator);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Facilitators", new { id = TrainingFacilitator.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }
}
