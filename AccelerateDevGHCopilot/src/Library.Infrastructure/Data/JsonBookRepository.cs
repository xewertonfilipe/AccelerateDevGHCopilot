using Library.ApplicationCore;
using Library.ApplicationCore.Entities;

namespace Library.Infrastructure.Data;

public class JsonBookRepository : IBookRepository
{
    private readonly JsonData _jsonData;

    public JsonBookRepository(JsonData jsonData)
    {
        _jsonData = jsonData;
    }

    public async Task<List<Book>> SearchBooks(string searchInput)
    {
        await _jsonData.EnsureDataLoaded();

        List<Book> searchResults = new List<Book>();
        foreach (Book book in _jsonData.Books!)
        {
            if (book.Title.Contains(searchInput, StringComparison.OrdinalIgnoreCase) ||
                book.ISBN.Contains(searchInput, StringComparison.OrdinalIgnoreCase))
            {
                searchResults.Add(book);
            }
        }
        searchResults.Sort((b1, b2) => String.Compare(b1.Title, b2.Title));

        searchResults = _jsonData.GetPopulatedBooks(searchResults);

        return searchResults;
    }

    public async Task<Book?> GetBook(int id)
    {
        await _jsonData.EnsureDataLoaded();

        foreach (Book book in _jsonData.Books!)
        {
            if (book.Id == id)
            {
                Book populated = _jsonData.GetPopulatedBook(book);
                return populated;
            }
        }
        return null;
    }
}
