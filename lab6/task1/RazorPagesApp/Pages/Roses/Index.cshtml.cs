using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Models;

namespace RazorPagesApp.Pages.Roses
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesApp.Data.RazorPagesAppContext _context;

        public IndexModel(RazorPagesApp.Data.RazorPagesAppContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public double StemLength { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public string? Color { get; set; } = null;

        public IList<Rose> TeaRoses { get; set; }
        public IList<Rose> AllRoses { get; set; }

        public async Task OnGetAsync()
        {
            TeaRoses = await _context.Rose
                .Where(r => r.Species == "Tea" && r.StemLength > StemLength)
                .Where(r => Color == null || r.Color == Color)
                .ToListAsync();
            
            AllRoses = await _context.Rose
                .OrderBy(r => r.Title)
                .ToListAsync();
        }
    }
}
