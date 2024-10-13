using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webcrp10._2.Pages
{
    public class UsloviaModel : PageModel
    {
        private readonly ILogger<UsloviaModel> _logger;

        public UsloviaModel(ILogger<UsloviaModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
