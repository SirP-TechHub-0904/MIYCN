using Application.Commands.AttendanceCommand;
using Application.Queries.AttendanceQueries;
using Application.Queries.DialyActivityQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using static Domain.Models.EnumStatus;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    public class AttendanceModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public AttendanceModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<Attendance> Datas { get; private set; }
        public Training Training { get; private set; }

        public DialyActivity DialyActivity { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }

        [BindProperty]
        public long TrainingId { get; set; }

        [BindProperty]
        public long DialyActivityId { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }




            GetByIdDialyActivityQuery ACommand = new GetByIdDialyActivityQuery(id);
            DialyActivity = await _mediator.Send(ACommand);

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(DialyActivity.TrainingId);
            Training = await _mediator.Send(Command);

            ValidateUserToTrainingAttendanceCommand queryAttendance = new ValidateUserToTrainingAttendanceCommand(DialyActivity.TrainingId);
            await _mediator.Send(queryAttendance);

            var query = new ListAttendanceByActivityIdQuery(id);
            Datas = await _mediator.Send(query);

            return Page();
        }
         


        public async Task<IActionResult> OnPostAsync()
        {
            StringBuilder formInfo = new StringBuilder();
            var attendanceData = new List<(long attendanceId, AttendanceStatus status)>();
            // Initialize counters for each status
            int presentCount = 0;
            int absentCount = 0;
            int excuseCount = 0;
            int nillCount = 0;
            foreach (var key in Request.Form.Keys)
            {
                string value = Request.Form[key];
                formInfo.AppendLine($"{key}: {value}");

                // Check if the key starts with "AttendanceResult_"
                if (key.StartsWith("AttendanceResult_"))
                {
                    // Extract the attendance ID from the key
                    if (long.TryParse(key.Substring("AttendanceResult_".Length), out long attendanceId))
                    {
                        // Get the enum value from the form
                        if (Enum.TryParse(value, out Domain.Models.EnumStatus.AttendanceStatus status))
                        {
                            // Add the extracted attendance ID and status to the list
                            attendanceData.Add((attendanceId, status));
                            // Increment the corresponding counter
                            switch (status)
                            {
                                case AttendanceStatus.Present:
                                    presentCount++;
                                    break;
                                case AttendanceStatus.Absent:
                                    absentCount++;
                                    break;
                                case AttendanceStatus.Excused:
                                    excuseCount++;
                                    break;
                                default:
                                    nillCount++;
                                    break;
                            }
                        }
                        else
                        {
                            // Handle invalid enum value
                            // Perhaps return an error response or log the issue
                        }
                    }
                    else
                    {
                        // Handle invalid attendance ID
                        // Perhaps return an error response or log the issue
                    }
                }
            }

            // Now you have attendanceData populated with attendance IDs and statuses
            // Pass attendanceData to the command handler
            var command = new UpdateAttendanceStatusCommand(attendanceData);
            await _mediator.Send(command);
            // Construct the TempData message with the counts
            string message = $"{presentCount} present, {absentCount} absent, {excuseCount} excuse";

            // Store the message in TempData
            TempData["response"] = message;
            return RedirectToPage("./Attendance", new {id= DialyActivityId });
            // Your existing code continues here...
        }



    }
}
