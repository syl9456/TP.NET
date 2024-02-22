using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MAUI.Reader.Model;
using MAUI.Reader.Service;
using System.Windows.Input;

using CommunityToolkit.Maui.Alerts;

namespace MAUI.Reader.ViewModel
{
    public partial class ListBooks : INotifyPropertyChanged
    {
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>(); 
        public ICommand NavigateToDetailsCommand { get; }
        public ListBooks()
        {
            NavigateToDetailsCommand = new Command<Book>(OnBookSelected);
            GetBooks();
            ItemSelectedCommand = new Command(OnItemSelectedCommand);
        }
        public ICommand ItemSelectedCommand { get; private set; }
        public void OnItemSelectedCommand(object book)
        {
        }


        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        //public ObservableCollection<Book> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;
        
        private async void GetBooks()
        {
            var books = await Ioc.Default.GetRequiredService<LibraryService>().LoadBooks();
            foreach (var book in books)
            {
                MainThread.BeginInvokeOnMainThread(() => {
                    Books.Add(book);
                });
            }
        }
        private void OnBookSelected(Book book)
        {
            //redirect to details page
            Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(book);
        }



        public int Count { get; set; }

        [RelayCommand]
        public void CounterClicked()
        {
            Count++;

            Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(new Book());
        }
    }
}
