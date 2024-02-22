using System.Collections.ObjectModel;
using System.Text.Json;
using MAUI.Reader.Model;

namespace MAUI.Reader.Service
{
    public class LibraryService
    {
        private HttpClientHandler httpClientHandler { get; set; }= new HttpClientHandler();
        private readonly HttpClient _httpClient;
        
        public LibraryService()
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            _httpClient = new HttpClient(httpClientHandler);
        }

        public async Task<List<Book>> LoadBooks()
        {
            try
            {
                string url = "https://localhost:5001/api/book";
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    return JsonSerializer.Deserialize<List<Book>>(content, options);
                }
                else
                {
                    Console.WriteLine("Error while fetching books");
                    return new List<Book>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<Book> LoadBook(int id)
        {
            try
            {
                string url = "https://localhost:5001/api/book/" + id;
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    return JsonSerializer.Deserialize<Book>(content, options);
                }
                else
                {
                    Console.WriteLine("Error while fetching book");
                    return new Book();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}