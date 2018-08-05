using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SSD_Assignment.Pages
{
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }
        public int iStatusCode { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            // Get the details of the exception that occurred
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();

            iStatusCode = HttpContext.Response.StatusCode;
        }
    }
}
