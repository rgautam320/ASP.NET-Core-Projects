using BookListRazor.Model;
using BookListRazor.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages
{
    public class UpsertModel : PageModel
    {
        private readonly IBookListRepository<BookListModel> _books;

        public UpsertModel(IBookListRepository<BookListModel> books)
        {
            _books = books;
        }

        [BindProperty]
        public BookListModel Book { get; set; }
        public async Task OnGet(string id)
        {
            if (id == null)
            {
                Book = new BookListModel();
            }
            else
            {
                Book = await _books.GetAsync(new Guid(id));
            }
        }
        public async Task<IActionResult> OnPost(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    await _books.CreateAsync(Book);
                }
                else
                {
                    await _books.UpdateAsync(new Guid(id), Book);
                }
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
