using Application.Commands.SupervisorTrainingFormCommand;
using Application.Queries.IdentityQueries;
using Application.Queries.SupervisorSectionQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Collections.Specialized.BitVector32;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateSupervisorReportModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateSupervisorReportModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public List<SupervisorSection> Section {  get; set; }
        public TrainingDto Training { get; set; }
        public FullProfileDto FullProfileDto { get; set; }

        [BindProperty]
        public long TrainingId { get;set;}
        public async Task OnGetAsync(long id)
        {
            GetByIdAndCountTrainingQuery Command = new GetByIdAndCountTrainingQuery(id);
            Training = await _mediator.Send(Command);

            var command = new ListSupervisorSectionQuery();
            Section = await _mediator.Send(command);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            GetUserProfileByIdQuery zCommand = new GetUserProfileByIdQuery(userId);
            FullProfileDto = await _mediator.Send(zCommand);
        }

        // Razor page post method
        public async Task<IActionResult> OnPostAsync(
            Dictionary<long, string> Answers,
            Dictionary<long, string> Remarks,
            Dictionary<long, string> ColumnOne,
            Dictionary<long, string> ColumnTwo,
            Dictionary<long, string> ColumnThree,
            Dictionary<long, string> ColumnFour)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var trainingForms = new List<SupervisorTrainingForm>();

                // Add regular question answers
                foreach (var answer in Answers)
                {
                    trainingForms.Add(new SupervisorTrainingForm
                    {
                        UserId = userId,
                        SupervisorSectionQuestionId = answer.Key,
                        Answer = answer.Value,
                        Remark = Remarks.ContainsKey(answer.Key) ? Remarks[answer.Key] : null,
                        Date = DateTime.UtcNow,
                        LastUpdateDate = DateTime.UtcNow,
                        TrainingId = TrainingId
                    });
                }

                // Add column-based responses (assuming these map to the same SupervisorSectionQuestionId)
                foreach (var columnEntry in ColumnOne.Keys)
                {
                    trainingForms.Add(new SupervisorTrainingForm
                    {
                        UserId = userId,
                        SupervisorSectionQuestionId = columnEntry,
                        ColumnOne = ColumnOne[columnEntry],
                        ColumnTwo = ColumnTwo.ContainsKey(columnEntry) ? ColumnTwo[columnEntry] : null,
                        ColumnThree = ColumnThree.ContainsKey(columnEntry) ? ColumnThree[columnEntry] : null,
                        ColumnFour = ColumnFour.ContainsKey(columnEntry) ? ColumnFour[columnEntry] : null,
                        Date = DateTime.UtcNow,
                        LastUpdateDate = DateTime.UtcNow,
                        TrainingId = TrainingId
                    });
                }

                var command = new AddSupervisorTrainingFormCommand(trainingForms);
                await _mediator.Send(command);

                TempData["success"] = "Success";
                return RedirectToPage("./SupervisorReport", new {id = TrainingId});
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                TempData["error"] = "Unale to submit form";
                GetByIdAndCountTrainingQuery Command = new GetByIdAndCountTrainingQuery(TrainingId);
                Training = await _mediator.Send(Command);

                var command = new ListSupervisorSectionQuery();
                Section = await _mediator.Send(command);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                GetUserProfileByIdQuery zCommand = new GetUserProfileByIdQuery(userId);
                FullProfileDto = await _mediator.Send(zCommand);
                return Page();
            }
        }
    }
}
