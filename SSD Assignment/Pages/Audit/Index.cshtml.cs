using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment.Models;

namespace SSDAssignment.Pages.Audit
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly SSD_Assignment.Models.ApplicationDbContext _context;

        public IndexModel(SSD_Assignment.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AuditRecord> AuditRecord { get;set; }

        public async Task OnGetAsync()
        {
            AuditRecord = await _context.AuditRecords.ToListAsync();
        }
    }
}
