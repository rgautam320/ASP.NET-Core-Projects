using BookListRazor.Model;
using BookListRazor.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookListRazor.Controllers
{
    [Route("/api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        IBookListRepository<BookListModel> _books;
        public BookController(IBookListRepository<BookListModel> books)
        {
            _books = books;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _books.GetAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _books.DeleteAsync(id);
                return Json(new { success = true, message = "Delete successful" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
