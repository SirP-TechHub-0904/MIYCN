using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.DTOs
{
    public class TrainingDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? LGA { get; set; }
        public string? Ward { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Training Status")]
        public TrainingStatus TrainingStatus { get; set; }
        [Display(Name = "Dialy Start Time")]
        public string DialyStartTime { get; set; }
        [Display(Name = "Dialy End Time")]
        public string DialyEndTime { get; set; }

        public int Sponsors { get; set; }
        public int TrainingFacilitators { get; set; }
        public int TrainingParticipants { get; set; }
        public int DialyActivities { get; set; }
        public int TestCategory { get; set; }
    }
}
