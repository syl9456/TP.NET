﻿using CommunityToolkit.Mvvm.DependencyInjection;
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
        public ListBooks()
        {
            GetBooks();
            ItemSelectedCommand = new Command<int>(OnItemSelectedCommand);
        }
        public ICommand ItemSelectedCommand { get; private set; }
        public void OnItemSelectedCommand(int Id)
        {
            Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(Id);
        }
        
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
    }
}
