using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SupervisorTrainingForm
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public AppUser User { get; set; }
        public long? TrainingId { get; set; }
        public Training Training { get; set; }

        public long SupervisorSectionQuestionId { get; set; }
        public SupervisorSectionQuestion SupervisorSectionQuestion { get; set; }
        public string? Answer { get; set; }
        public string? Remark { get; set; }
        public string? ColumnOne { get; set; }
        public string? ColumnTwo { get; set; }
        public string? ColumnThree { get; set; }
        public string? ColumnFour { get; set; }

        public DateTime Date { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
