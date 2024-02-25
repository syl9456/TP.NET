using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUI.Reader.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public double Price { get; set; }
        public string Content { get; set; }
        public List<Genre> Genres { get; set; }
    }
    
    public class Author
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Book> Books { get; set; } 
    }
    
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}