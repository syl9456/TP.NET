using System.Collections.Generic;
using ASP.Server.Models;

namespace ASP.Server.ViewModels
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Author> AllAuthors { get; set; }
        public IEnumerable<Genre> AllGenres { get; set; }
        public int[] SelectedAuthors { get; set; }
        public int[] SelectedGenres { get; set; }
    }
}
