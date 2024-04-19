using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Models
{
    public class Certificate
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public AppUser User { get; set; }

        public string? FullName { get; set; }
        public string? PassportUrl { get; set; }
        public string? PassportKey { get; set; }

        public string? CerificateId { get; set; }
        public CertificateType CertificateType { get; set; }

        public DateTime IssuerDate { get; set; }

        public long? TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
