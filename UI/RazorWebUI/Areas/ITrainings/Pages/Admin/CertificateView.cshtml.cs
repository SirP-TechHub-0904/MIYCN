using Application.Queries.CertificationQueries;
using Application.Queries.SettingQueries;
using Application.Queries.TrainingQueries;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    public class CertificateViewModel : PageModel
    {
        private readonly IMediator _mediator;

        public CertificateViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Domain.Models.Certificate Certificate { get; set; }
        public Training Training { get; private set; }
        public Setting Setting { get; private set; }
        public byte[] BarcodeImage { get; set; }
        public string PassportBase { get; set; }
        public string LeftSignature { get; set; }
        public string RightSignature { get; set; }
        public string BlankUrl { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdCertificateQuery Command = new GetByIdCertificateQuery(id);
            Certificate = await _mediator.Send(Command);
            if(Certificate == null)
            {
                return NotFound();
            }
            GetSettingQuery yxCommand = new GetSettingQuery();
            Setting = await _mediator.Send(yxCommand);
            //
            try
            {
                // Convert Passport URL to Base64
                using (var httpClient = new HttpClient())
                {
                    var passportUrl = Certificate.PassportUrl; // Ensure PassportUrl is a valid URL
                    var imageBytes = await httpClient.GetByteArrayAsync(passportUrl);
                    PassportBase = Convert.ToBase64String(imageBytes); // Save as Base64 string
                }
            }
            catch(Exception c)
            {

            }
          
            long tid = Certificate.TrainingId ?? 0;
            GetByIdTrainingQuery xCommand = new GetByIdTrainingQuery(tid);
            Training = await _mediator.Send(xCommand);


            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            string userinfo = "";
            try
            {
               var CertificateLink = "https://miycnportal.com/certificate/" + Certificate.CerificateNumber;
                System.Drawing.Image img = barcode.Draw(CertificateLink, 100);

                BarcodeImage = AppServices.TurnImageToByteArray(img);
            }
            catch (Exception c)
            {

            }
            //
            try
            {
                // Convert Passport URL to Base64
                using (var httpClient = new HttpClient())
                {
                    var leftUrl = Setting.CertificateLeftSideSignatureUrl; // Ensure PassportUrl is a valid URL
                    var imageBytes = await httpClient.GetByteArrayAsync(leftUrl);
                    LeftSignature = Convert.ToBase64String(imageBytes); // Save as Base64 string
                }
            }
            catch (Exception c)
            {

            }
            //
            try
            {
                // Convert Passport URL to Base64
                using (var httpClient = new HttpClient())
                {
                    var rightUrl = Setting.CertificateRightSideSignatureUrl; // Ensure PassportUrl is a valid URL
                    var imageBytes = await httpClient.GetByteArrayAsync(rightUrl);
                    RightSignature = Convert.ToBase64String(imageBytes); // Save as Base64 string
                }
            }
            catch (Exception c)
            {

            }

            //
            try
            {
                // Convert Passport URL to Base64
                using (var httpClient = new HttpClient())
                {
                    var rightUrl = "https://miycnportal.com/img/blanksignature.png"; // Ensure PassportUrl is a valid URL
                    var imageBytes = await httpClient.GetByteArrayAsync(rightUrl);
                    BlankUrl = Convert.ToBase64String(imageBytes); // Save as Base64 string
                }
            }
            catch (Exception c)
            {

            }
            return Page();
        }
    }
}
