using BookListRazor.Model;
using BookListRazor.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IBookListRepository<BookListModel> _books;

        public CreateModel(IBookListRepository<BookListModel> books)
        {
            _books = books;
        }

        [BindProperty]
        public BookListModel Book { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _books.CreateAsync(Book);

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
