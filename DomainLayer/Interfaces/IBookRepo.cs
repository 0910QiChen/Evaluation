using DomainLayer.DomainModels;
using System.Collections.Generic;

namespace DomainLayer.Interfaces
{
    public interface IBookRepo : IGenericRepo<Book>
    {
        IEnumerable<Book> getAllBooks();
        void createBook(Book book);
        Book findBook(int? id);
        void editBook(Book book);
        void deleteBook(int id);
    }
}