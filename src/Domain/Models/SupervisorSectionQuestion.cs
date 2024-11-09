using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class SupervisorSectionQuestion
    {
        public long Id { get; set; }
        public string? Question  { get; set; } 

        public int SortOrder { get; set; }


        public long SupervisorSectionId { get; set; }
        public SupervisorSection SupervisorSection { get; set; }
    }
}
