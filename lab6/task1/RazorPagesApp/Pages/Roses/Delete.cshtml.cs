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
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesApp.Data.RazorPagesAppContext _context;

        public DeleteModel(RazorPagesApp.Data.RazorPagesAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rose = await _context.Rose.FindAsync(id);
            if (rose != null)
            {
                Rose = rose;
                _context.Rose.Remove(Rose);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
