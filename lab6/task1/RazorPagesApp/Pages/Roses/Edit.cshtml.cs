using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Data;
using RazorPagesApp.Models;

namespace RazorPagesApp.Pages.Roses
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesApp.Data.RazorPagesAppContext _context;

        public EditModel(RazorPagesApp.Data.RazorPagesAppContext context)
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

            var rose =  await _context.Rose.FirstOrDefaultAsync(m => m.Id == id);
            if (rose == null)
            {
                return NotFound();
            }
            Rose = rose;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Rose).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoseExists(Rose.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoseExists(int id)
        {
            return _context.Rose.Any(e => e.Id == id);
        }
    }
}
