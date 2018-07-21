using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment.Models;
using SSD_Assignment.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SSD_Assignment.Pages.Listings
{
    public class IndexModel : PageModel
    {
        private readonly SSD_Assignment.Data.ApplicationDbContext _context;

        public IndexModel(SSD_Assignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Listing> Listing { get;set; }
        public SelectList Categories { get; set; }
        public string ListingCategory { get; set; }

        public async Task OnGetAsync(string listingCategory, string searchString)
        {
            // Use LINQ to get list of categories.
            IQueryable<string> categoryQuery = from l in _context.Listing
                                            orderby l.Category
                                            select l.Category;

            var listings = from l in _context.Listing
                           select l;

            if(!String.IsNullOrEmpty(searchString))
    {
                listings = listings.Where(s => s.Title.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(listingCategory))
            {
                listings = listings.Where(x => x.Category == listingCategory);
            }

            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
            Listing = await listings.ToListAsync();
        }
    }
}
