using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.DTOs
{
    public class TrainingByUserDto
    {
        public string UserId { get; set; }
        public long TrainingId { get; set; }
        public string Type { get; set; } // Participant or Facilitator
        public string? FacilitatorPosition { get; set; }
        public string? TrainingTitle { get; set; }
        public DateTime TrainingDate { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserState { get; set; }
        public string? UserLGA { get; set; }
        public string? PlaceOfWork { get; set; }
        public string? TrainingState { get; set; }
        public string? TrainingLGA { get; set; }
        public TrainingStatus TrainingStatus { get; set; }
    }
}
