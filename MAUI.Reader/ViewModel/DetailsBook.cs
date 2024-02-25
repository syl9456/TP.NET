using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using MAUI.Reader.Model;
using CommunityToolkit.Mvvm.DependencyInjection;
using MAUI.Reader.Service;

namespace MAUI.Reader.ViewModel
{
    public partial class DetailsBook(Book book) : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ReadBookCommand { get; init; } = new RelayCommand<Book>(
            x => {
                Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(book);
            }
        );
        public Book CurrentBook { get; init; } = book;

        private async void GetBook(int id)
        {
            var book = await Ioc.Default.GetRequiredService<LibraryService>().LoadBook(id);
            Book.Add(book);
        }
    }
}
