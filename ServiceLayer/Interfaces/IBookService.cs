using ServiceLayer.DTOs;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IBookService
    {
        List<BookDTO> getAllBooks();
        void createBook(BookDTO book);
        BookDTO findBook(int? id);
        void editBook(BookDTO book);
        void deleteBook(int id);
    }
}