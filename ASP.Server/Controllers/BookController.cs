using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using ASP.Server.Database;
using ASP.Server.Models;
using ASP.Server.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.Server.Controllers
{
    public class BookController(LibraryDbContext libraryDbContext, IMapper mapper) : Controller
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;
        private readonly IMapper mapper = mapper;

        // A vous de faire comme BookController.List mais pour les genres !

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            IEnumerable<Book> ListBooks = libraryDbContext.Books.
                Include(b => b.Authors).
                Include(b => b.Genres);
            return View(ListBooks);
        }

        public ActionResult<CreateBookViewModel> Create(CreateBookViewModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateGenreModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                // Completer la création du genre avec toute les information nécéssaire que vous aurez ajoutez, et mettez la liste des gener récupéré de la base aussi
                var newBook = new Book
                        {
                            Name = book.Name,
                            Authors = book.Authors.Select(id => libraryDbContext.Authors.Find(id)).ToList(),
                            Price = book.Price,
                            Content = book.Content,
                            Genres = book.Genres.Select(id => libraryDbContext.Genres.Find(id)).ToList()
                        };
                libraryDbContext.Add(newBook);
                libraryDbContext.SaveChanges();

                return RedirectToAction("Show", new { id = newBook.Id });
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookViewModel()
            {
                AllAuthors = libraryDbContext.Authors, 
                AllGenres = libraryDbContext.Genres
            });
        }
        
        public ActionResult Show([FromRoute]int id)
        {
            // Retourne la vue avec le modèle de livre
            return View(libraryDbContext.Books.
                Include(b => b.Authors).
                Include(b => b.Genres).
                Single(b => b.Id == id));
        }

        public ActionResult<UpdateBookViewModel> Update(UpdateBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                // Récupérer le livre existant
                var existingBook = libraryDbContext.Books
                    .Include(b => b.Authors)
                    .Include(b => b.Genres)
                    .FirstOrDefault(b => b.Id == book.Id);

                if (existingBook == null)
                {
                    // Gérer le cas où le livre n'est pas trouvé
                    return NotFound();
                }
                
                // Mettre à jour les propriétés de l'entité existante
                existingBook.Name = book.Name;
                existingBook.Price = book.Price;
                existingBook.Content = book.Content;
                existingBook.Authors = book.Authors.Select(id => libraryDbContext.Authors.Find(id)).ToList();
                existingBook.Genres = book.Genres.Select(id => libraryDbContext.Genres.Find(id)).ToList();

                // Sauvegarder les modifications
                libraryDbContext.SaveChanges();

                return RedirectToAction("Show", new { id = book.Id });
            }

            return View(new UpdateBookViewModel()
            {
                AllAuthors = libraryDbContext.Authors, 
                AllGenres = libraryDbContext.Genres
            });
        }
    }
}