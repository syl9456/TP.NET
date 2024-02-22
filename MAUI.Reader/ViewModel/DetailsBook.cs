using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using MAUI.Reader.Model;
using MAUI.Reader.Service;

namespace MAUI.Reader.ViewModel
{
    public partial class DetailsBook(Book book) : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Une commande permet de recevoir des évènement de l'IHM
        public ICommand ReadBook2Command { get; init; } = new RelayCommand<Book>(x => { /* A vous de définir la commande */ });
        public ObservableCollection<Book> Book { get; set; } = new ObservableCollection<Book>();
        [RelayCommand]
        public void ReadBook(Book book)
        {
            /* A vous de définir la commande */
        }
        
        public Book CurrentBook { get; init; } = book;

        private async void GetBook(int id)
        {
            var book = await Ioc.Default.GetRequiredService<LibraryService>().LoadBook(id);
            Book.Add(book);
        }
    }
    
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new Book() /*{ Title = "Test Book" }*/) { }
    }
}
