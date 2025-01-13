using Application.Queries.CertificationQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AllCertificateLogModel : PageModel
    {
        private readonly IMediator _mediator;

        public AllCertificateLogModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public List<Domain.Models.Certificate> Certificate { get; set; }
        public Training Training { get; private set; }
        public string StatsName {get;set;}
        public async Task<IActionResult> OnGetAsync(string state = null)
        {

            ListCertificateQuery Command = new ListCertificateQuery();
            Certificate = await _mediator.Send(Command);
            if(state != null) {
                Certificate = Certificate.Where(x=>x.Training.State.ToUpper() ==  state.ToUpper()).ToList();
                StatsName = state.ToUpper();
            }
            return Page();
        }
    }
}
