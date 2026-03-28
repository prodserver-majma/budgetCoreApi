using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mahadalzahrawebapi.Pages
{
    public class emailTemplatesModel : PageModel
    {
        public void OnGet()
        {
        }
        public string TemplateName { get; set; }
        public string Module { get; set; }
        public string Content { get; set; }
        public string Reference { get; set; }
        public string EmailSubject { get; set; }
    }
}
