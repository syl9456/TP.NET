using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASP.Server.Models;

namespace ASP.Server.ViewModels
{
    public class UpdateBookViewModel
    {
        [Required]
        public String Name { get; set; }
        
        [Required (ErrorMessage = "At least one author is required")]
        public IEnumerable<int> Authors { get; set; }
        public IEnumerable<Author> AllAuthors { get; set; }
        
        [Required]
        public double Price { get; set; }
        
        [Required]
        public String Content { get; set; }
        
        [Required (ErrorMessage = "At least one genre is required")]
        public IEnumerable<int> Genres { get; set; }
        
        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; set; }
    }
}