using Application.Queries.TrainingFacilitatorQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    public class FacilitatorsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public FacilitatorsModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<FacilitatorInTrainingDTo> Datas { get; private set; }
        public Training Training { get; private set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var query = new ListFacilitatorByTrainingIdQuery(id);
            Datas = await _mediator.Send(query);

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            return Page();
        }
    }
}
