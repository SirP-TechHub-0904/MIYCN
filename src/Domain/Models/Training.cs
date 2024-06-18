using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class Training
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

        public ICollection<Sponsor> Sponsors { get; set; }
        public ICollection<TrainingFacilitator> TrainingFacilitators { get; set; }
        public ICollection<TrainingParticipant> TrainingParticipants { get; set; }
        public ICollection<DialyActivity> DialyActivities {  get; set; }
        public ICollection<TestCategory> TestCategory { get; set; }
        [Display(Name = "Enable Post Test")]
        public bool EnablePostTest { get; set; }
        [Display(Name = "Post Test Instruction")]
        public string? PostTestInstruction { get; set; }
        [Display(Name = "Enable Pre Test")]
        public bool EnablePreTest { get; set; }
        [Display(Name = "Pre Test Instruction")]
        public string? PreTestInstruction { get; set; }


        [Display(Name = "Evaluation Instruction")]
        public string? EvaluationInstruction { get; set; }


        [Display(Name = "SignInStop Start Time")]
        public TimeSpan SignInStartTime { get; set; }
        [Display(Name = "SignIn Stop Time")]
        public TimeSpan SignInStopTime { get; set; }




        [Display(Name = "SignOutStop Start Time")]
        public TimeSpan SignOutStartTime { get; set; }
        [Display(Name = "SignOut Stop Time")]
        public TimeSpan SignOutStopTime { get; set; }
    }
}
