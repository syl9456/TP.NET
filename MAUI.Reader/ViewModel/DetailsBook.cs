using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using MAUI.Reader.Model;
using MAUI.Reader.Service;

namespace MAUI.Reader.ViewModel
{
    public partial class DetailsBook : INotifyPropertyChanged
    {
        public DetailsBook(int Id)
        {
            GetBook(Id);
            ReadBookCommand = new Command<Book>(ReadBook);
        }
        public ObservableCollection<Book> Book { get; set; } = new ObservableCollection<Book>();
        public ICommand ReadBookCommand { get; private set; }
        public void ReadBook(Book book)
        {
            Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(book);
        }

        public Book CurrentBook { get; private set; }

        private async void GetBook(int id)
        {
            CurrentBook = await Ioc.Default.GetRequiredService<LibraryService>().LoadBook(id);
        }
    }
}
