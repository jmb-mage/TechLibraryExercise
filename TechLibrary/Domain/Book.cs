using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechLibrary.Domain
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string PublishedDate { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ShortDescr { get; set; }
        public string LongDescr { get; set; }
    }

    public class BooksPage
    {
        public List<Book> books { get; set; }
        public int count { get; set; }
    }
}