using System.Collections.ObjectModel;
using MAUI.Reader.Model;

namespace MAUI.Reader.Service
{
    public class LibraryService
    {
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouter et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>() {
            new Book(){Id = 1, Name= "Book1", Authors = new List<Author>{ new Author(){Id = 1, Name = "Author1"}, new Author(){Id = 2, Name = "Author2"} }, Price = 10.99, Content = "aaaa", Genres = new List<Genre>{ new Genre() { Id=1, Name="Genre1" }, new Genre() { Id=2, Name="Genre2" } } },
            new Book(){Id = 2, Name= "Book2", Authors = new List<Author>{ new Author(){Id = 3, Name = "Author3"}, new Author(){Id = 4, Name = "Author4"} }, Price = 10.99, Content = "ffff", Genres = new List<Genre>{ new Genre() { Id=3, Name="Genre3" }, new Genre() { Id=4, Name="Genre4" } } },
            new Book(){Id = 3, Name= "Book3", Authors = new List<Author>{ new Author(){Id = 5, Name = "Author5"}, new Author(){Id = 6, Name = "Author6"} }, Price = 10.99, Content = "tttt", Genres = new List<Genre>{ new Genre() { Id=5, Name="Genre5" }, new Genre() { Id=6, Name="Genre6" } } }
        };

        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 
        public LibraryService()
        {
        }
    }
}
