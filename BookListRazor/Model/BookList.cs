using System.ComponentModel.DataAnnotations;

namespace BookListRazor.Model
{
    public class BookListModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
    }
}
