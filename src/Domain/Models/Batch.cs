using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Batch
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public ICollection<Training> Training { get; set; }
    }
}
