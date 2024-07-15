using DomainLayer.DomainModels;
using System.Collections.Generic;

namespace DomainLayer.Interfaces
{
    public interface IAuthorRepo : IGenericRepo<Author>
    {
        IEnumerable<Author> getAllAuthors();
        void createAuthor(Author author);
        Author findAuthor(int? id);
        void editAuthor(Author author);
        void deleteAuthor(int id);
    }
}