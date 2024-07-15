using DomainLayer.Interfaces;
using DomainLayer.DomainModels;
using RepositoryLayer.Contexts;
using System.Collections.Generic;

namespace RepositoryLayer.Repositories
{
    public class AuthorRepo : GenericRepo<Author>, IAuthorRepo
    {
        private readonly GenericRepo<Author> _geneRepo;

        public AuthorRepo(LibraryContext context) : base(context)
        {
            _geneRepo = new GenericRepo<Author>(context);
        }

        public IEnumerable<Author> getAllAuthors()
        {
            return _geneRepo.getAll();
        }

        public void createAuthor(Author author)
        {
            _geneRepo.add(author);
        }

        public Author findAuthor(int? id)
        {
            return _geneRepo.getByID(id);
        }

        public void editAuthor(Author author)
        {
            _geneRepo.update(author);
        }

        public void deleteAuthor(int id)
        {
            _geneRepo.delete(findAuthor(id));
        }
    }
}