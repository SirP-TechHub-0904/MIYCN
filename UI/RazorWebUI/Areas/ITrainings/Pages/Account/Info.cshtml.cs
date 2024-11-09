using Application.Queries.TrainingFacilitatorQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class InfoModel : PageModel
    {
        private readonly IMediator _mediator;

        public InfoModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingDto Training { get; set; }

        public TrainingFacilitator TrainingFacilitator { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdAndCountTrainingQuery Command = new GetByIdAndCountTrainingQuery(id);
            Training = await _mediator.Send(Command);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GetByIdAndUserIdTrainingFacilitatorQuery xCommand = new GetByIdAndUserIdTrainingFacilitatorQuery(id, userId);
            TrainingFacilitator = await _mediator.Send(xCommand);

            

            return Page();
        }
    }
}
