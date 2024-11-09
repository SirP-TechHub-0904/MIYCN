using Application.Queries.IdentityQueries;
using Application.Queries.SupervisorSectionQueries;
using Application.Queries.SupervisorTrainingFormQueries;
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

    public class SupervisorReportModel : PageModel
    {
        private readonly IMediator _mediator;

        public SupervisorReportModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<SupervisorSection> Section { get; set; }
        public Dictionary<long, SupervisorTrainingForm> UserResponses { get; set; }
        public TrainingDto Training { get; set; }
        public FullProfileDto FullProfileDto { get; set; }

        public async Task OnGetAsync(long id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new ListSupervisorSectionQuery();
            Section = await _mediator.Send(command);
            UserResponses = await _mediator.Send(new GetUserResponsesQuery(userId, id));


            GetByIdAndCountTrainingQuery yCommand = new GetByIdAndCountTrainingQuery(id);
            Training = await _mediator.Send(yCommand);


            GetUserProfileByIdQuery zCommand = new GetUserProfileByIdQuery(userId);
            FullProfileDto = await _mediator.Send(zCommand);
        }
    }
}
