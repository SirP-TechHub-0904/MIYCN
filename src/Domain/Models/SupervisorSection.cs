using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class SupervisorSection
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Instruction { get; set; }
        public string? QuestionTitle { get; set; }


        public EvaluationAnswerType AnswerType { get; set; }
        public string? AnswerTypeTitle { get; set; }
        public bool EnableRemark { get; set; }
        public string? RemarkTitle { get; set; }

        public bool EnableNames { get; set; }
        public string? ColumnOne { get; set; }
        public string? ColumnTwo { get; set; }
        public string? ColumnThree { get; set; }
        public string? ColumnFour { get; set; }

        public int SortOrder { get; set; }

        public ICollection<SupervisorSectionQuestion> SupervisorSectionQuestion { get; set; }
    }


}
