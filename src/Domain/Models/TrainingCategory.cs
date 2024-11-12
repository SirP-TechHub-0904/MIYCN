using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TrainingCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }


        [Display(Name = "Certificate Initial")]
        public string? CertificateInitial { get; set; }


        public ICollection<Training> Training { get; set; }
    }
}
