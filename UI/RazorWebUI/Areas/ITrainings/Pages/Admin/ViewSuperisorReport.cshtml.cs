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

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ViewSuperisorReportModel : PageModel
    {
        private readonly IMediator _mediator;

        public ViewSuperisorReportModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<SupervisorSection> Section { get; set; }
        public Dictionary<long, SupervisorTrainingForm> UserResponses { get; set; }
        public TrainingDto Training { get; set; }
        public FullProfileDto FullProfileDto { get; set; }
        public async Task OnGetAsync(long id, string userId)
        {
            var command = new ListSupervisorSectionQuery();
            Section = await _mediator.Send(command);
            UserResponses = await _mediator.Send(new GetUserResponsesQuery(userId, id));



            GetUserProfileByIdQuery zCommand = new GetUserProfileByIdQuery(userId);
            FullProfileDto = await _mediator.Send(zCommand);

            GetByIdAndCountTrainingQuery yCommand = new GetByIdAndCountTrainingQuery(id);
            Training = await _mediator.Send(yCommand);
        }
    }
}
