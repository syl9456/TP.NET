using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASP.Server.Models;

namespace ASP.Server.ViewModels
{
    public class UpdateBookViewModel
    {
        [Required]
        public int Id { get; set; }
        
        public String Name { get; set; }
        
        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre
        [Required(ErrorMessage = "You need at least 1 author"), MinLength(1)]
        public IEnumerable<int> Authors { get; set; }
        public IEnumerable<Author> AllAuthors { get; set; }
        
        public double Price { get; set; }
        public String Content { get; set; }

        // Liste des genres séléctionné par l'utilisateur
        [Required(ErrorMessage = "You need at least 1 genre"), MinLength(1)]
        public IEnumerable<int> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; set; }
    }
}