using ServiceLayer.DTOs;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IAuthorService
    {
        List<AuthorDTO> getAllAuthors();
        void createAuthor(AuthorDTO authorDTO);
        AuthorDTO findAuthor(int? id);
        void editAuthor(AuthorDTO authorDTO);
        void deleteAuthor(int id);
    }
}