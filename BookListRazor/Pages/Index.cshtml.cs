using BookListRazor.Model;
using BookListRazor.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBookListRepository<BookListModel> _books;

        public IndexModel(IBookListRepository<BookListModel> books)
        {
            _books = books;
        }

        public List<BookListModel> Books { get; set; }
        public async Task OnGet()
        {
            Books = await _books.GetAsync();
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            await _books.DeleteAsync(id);

            return RedirectToPage("Index");
        }
    }
}