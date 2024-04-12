using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TrainingFacilitator
    {
        public long Id { get; set; }
        public string? UserId {  get; set; }
        public AppUser User { get; set; }

        public long TrainingId { get; set; }
        public Training Training { get; set; }

        public string Position {  get; set; } 
    }
}
