using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AttendanceCommand
{
    public sealed class DeleteAttendanceCommand : IRequest
    {
        public DeleteAttendanceCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteAttendanceCommandHandler : IRequestHandler<DeleteAttendanceCommand>
    {
        private readonly IAttendanceRepository _repository;

        public DeleteAttendanceCommandHandler(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteAttendanceCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
