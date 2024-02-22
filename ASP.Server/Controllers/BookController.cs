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

        /*public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            IEnumerable<Book> ListBooks = libraryDbContext.Books.
                Include(b => b.Authors).
                Include(b => b.Genres);
            
            return View(ListBooks);
        }*/
        
        public ActionResult<IEnumerable<Book>> List(int[] selectedAuthors, int[] selectedGenres)
        {
            var query = libraryDbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres);

            if (selectedAuthors.Length > 0)
            {
                query = query.Where(b => b.Authors
                    .Any(a => selectedAuthors.Contains(a.Id))).
                    Include(b => b.Authors).
                    Include(b => b.Genres);
            }

            if (selectedGenres.Length > 0)
            {
                query = query.Where(b => b.Genres.Any(g => selectedGenres.Contains(g.Id))).
                    Include(b => b.Authors).
                    Include(b => b.Genres);
            }

            return View(new BooksListViewModel()
            {
                Books = query.ToList(),
                AllAuthors = libraryDbContext.Authors.ToList(),
                AllGenres = libraryDbContext.Genres.ToList(),
                SelectedAuthors = selectedAuthors,
                SelectedGenres = selectedGenres
            });
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

                return RedirectToAction(nameof(Show), new { id = newBook.Id });
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookViewModel()
            {
                AllAuthors = libraryDbContext.Authors,
                AllGenres = libraryDbContext.Genres
            });
        }

        public ActionResult Show([FromRoute] int id)
        {
            // Retourne la vue avec le modèle de livre
            return View(libraryDbContext.Books.Include(b => b.Authors).Include(b => b.Genres).Single(b => b.Id == id));
        }

        public ActionResult<UpdateBookViewModel> Update([FromRoute] int id, UpdateBookViewModel newBook)
        {
            var existingBook = libraryDbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefault(b => b.Id == id);

            if (existingBook == null)
            {
                // Gérer le cas où le livre n'est pas trouvé
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                if (newBook.Name == existingBook.Name && newBook.Price == existingBook.Price &&
                    newBook.Content == existingBook.Content &&
                    newBook.Authors.SequenceEqual(existingBook.Authors.Select(a => a.Id)) &&
                    newBook.Genres.SequenceEqual(existingBook.Genres.Select(g => g.Id)))
                {
                    return RedirectToAction(nameof(Show), new { id = existingBook.Id });
                }


                if (newBook.Name != null)
                {
                    existingBook.Name = newBook.Name;
                }

                if (newBook.Price != 0)
                {
                    existingBook.Price = newBook.Price;
                }

                if (newBook.Content != null)
                {
                    existingBook.Content = newBook.Content;
                }

                if (newBook.Authors != null)
                {
                    existingBook.Authors = newBook.Authors.Select(id => libraryDbContext.Authors.Find(id)).ToList();
                }

                if (newBook.Genres != null)
                {
                    existingBook.Genres = newBook.Genres.Select(id => libraryDbContext.Genres.Find(id)).ToList();
                }

                libraryDbContext.Update(existingBook);

                // Sauvegarder les modifications
                libraryDbContext.SaveChanges();

                return RedirectToAction(nameof(Show), new { id = existingBook.Id });
            }

            return View(new UpdateBookViewModel()
            {
                Name = existingBook.Name,
                Price = existingBook.Price,
                Content = existingBook.Content,
                AllAuthors = libraryDbContext.Authors,
                AllGenres = libraryDbContext.Genres
            });
        }

        public ActionResult Delete([FromRoute] int id)
        {
            var existingBook = libraryDbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefault(b => b.Id == id);

            if (existingBook == null)
            {
                // Gérer le cas où le livre n'est pas trouvé
                return NotFound();
            }

            libraryDbContext.Remove(existingBook);
            libraryDbContext.SaveChanges();

            return RedirectToAction(nameof(List));
        }
    }
}