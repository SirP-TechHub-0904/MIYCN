using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Supervisor.ISection
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class InfoModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
