using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Data;
using RazorPagesApp.Models;

namespace RazorPagesApp.Pages.Roses
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesApp.Data.RazorPagesAppContext _context;

        public DetailsModel(RazorPagesApp.Data.RazorPagesAppContext context)
        {
            _context = context;
        }

        public Rose Rose { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rose = await _context.Rose.FirstOrDefaultAsync(m => m.Id == id);
            if (rose == null)
            {
                return NotFound();
            }
            else
            {
                Rose = rose;
            }
            return Page();
        }
    }
}
