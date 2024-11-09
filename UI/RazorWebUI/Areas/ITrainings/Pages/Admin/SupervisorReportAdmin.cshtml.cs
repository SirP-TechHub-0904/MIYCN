using Application.Queries.SupervisorTrainingFormQueries;
using Application.Queries.TrainingCategoryQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class SupervisorReportAdminModel : PageModel
    { 
        private readonly ILogger<SupervisorReportAdminModel> _logger;
        private readonly IMediator _mediator;

        public SupervisorReportAdminModel(ILogger<SupervisorReportAdminModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public List<UserTrainingReportDto> Datas { get; private set; }
        public TrainingDto Training { get; set; }

        public async Task OnGetAsync(long id)
        {
            GetByIdAndCountTrainingQuery Command = new GetByIdAndCountTrainingQuery(id);
            Training = await _mediator.Send(Command);
            var query = new ListSupervisorWithReportQuery(id);
            Datas = await _mediator.Send(query);
        }

    }
}
