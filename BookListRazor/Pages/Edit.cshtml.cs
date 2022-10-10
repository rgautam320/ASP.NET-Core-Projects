using BookListRazor.Model;
using BookListRazor.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages
{
    public class EditModel : PageModel
    {
        private readonly IBookListRepository<BookListModel> _books;

        public EditModel(IBookListRepository<BookListModel> books)
        {
            _books = books;
        }

        [BindProperty]
        public BookListModel Book { get; set; }
        public async Task OnGet(Guid id)
        {
            Book = await _books.GetAsync(id);
        }
        public async Task<IActionResult> OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                await _books.UpdateAsync(id, Book);

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
