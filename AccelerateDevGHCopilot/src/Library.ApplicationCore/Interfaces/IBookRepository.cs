using Library.ApplicationCore.Entities;

namespace Library.ApplicationCore;

public interface IBookRepository
{
    Task<List<Book>> SearchBooks(string searchInput);
    Task<Book?> GetBook(int bookId);
}
