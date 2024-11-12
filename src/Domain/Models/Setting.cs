using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Setting
    {
        public long Id { get; set; }
 
        //
        [Display(Name = "Certificate Use Right Side Physical Signature")]
        public bool CertificateUseRightSidePhysicalSignature { get; set; }
        [Display(Name = "Certificate Right Side Signature Url")]

        public string? CertificateRightSideSignatureUrl { get; set; }
        [Display(Name = "Certificate Right Side Signature Key")]

        public string? CertificateRightSideSignatureKey { get; set; }
        [Display(Name = "Certificate Right Side Name")]

        public string? CertificateRightSideName { get; set; }
        [Display(Name = "Certificate Right Side Office Position")]

        public string? CertificateRightSideOfficePosition { get; set; }
        [Display(Name = "Certificate Right Side Office Title")]

        public string? CertificateRightSideOfficeTitle { get; set; }

        //
        [Display(Name = "Certificate Use Left Side Physical Signature")]
        public bool CertificateUseLeftSidePhysicalSignature { get; set; }
        [Display(Name = "Certificate Left Side Signature Url")]

        public string? CertificateLeftSideSignatureUrl { get; set; }
        [Display(Name = "Certificate Left Side Signature Key")]

        public string? CertificateLeftSideSignatureKey { get; set; }
        [Display(Name = "Certificate Left Side Name")]

        public string? CertificateLeftSideName { get; set; }
        [Display(Name = "Certificate Left Side Office Position")]

        public string? CertificateLeftSideOfficePosition { get; set; }

        [Display(Name = "Certificate Left Side Office Title")]
        public string? CertificateLeftSideOfficeTitle { get; set; }
        //
        [Display(Name = "Certificate Title")]
        public string? CertificateTitle { get; set; }
    }
}
