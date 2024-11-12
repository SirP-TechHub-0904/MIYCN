using Application.Queries.CertificationQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AllCertificateModel : PageModel
    {
        private readonly IMediator _mediator;

        public AllCertificateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public List<Domain.Models.Certificate> Certificate { get; set; }
        public Training Training { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {

            ListCertificateQuery Command = new ListCertificateQuery();
            Certificate = await _mediator.Send(Command);
             
            return Page();
        }
    }
}
