using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.AttendanceCommand
{
    public sealed class UpdateAttendanceStatusCommand : IRequest
    {
        public UpdateAttendanceStatusCommand(List<(long attendanceId, AttendanceStatus status)> attendanceData)
        {
            AttendanceData = attendanceData;
        }

        public List<(long attendanceId, AttendanceStatus status)> AttendanceData { get; set; }
    }

    public class UpdateAttendanceStatusCommandHandler : IRequestHandler<UpdateAttendanceStatusCommand>
    {
        private readonly IAttendanceRepository _repository;

        public UpdateAttendanceStatusCommandHandler(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateAttendanceStatusCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAttendanceStatus(request.AttendanceData);
        }
    }

}
