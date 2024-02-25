using System.ComponentModel;
using MAUI.Reader.Model;

namespace MAUI.Reader.ViewModel
{
    class ReadBook (Book book) : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Book CurrentBook { get; init; } = book;
    }
}
