using DomainLayer.Interfaces;
using DomainLayer.DomainModels;
using RepositoryLayer.Contexts;
using System.Collections.Generic;

namespace RepositoryLayer.Repositories
{
    public class BookRepo : GenericRepo<Book>, IBookRepo 
    {
        private readonly GenericRepo<Book> _geneRepo;

        public BookRepo(LibraryContext context) : base(context)
        {
            _geneRepo = new GenericRepo<Book>(context);
        }

        public IEnumerable<Book> getAllBooks()
        {
            return _geneRepo.getAll();
        }

        public void createBook(Book book)
        {
            _geneRepo.add(book);
        }

        public Book findBook(int? id)
        {
            return _geneRepo.getByID(id);
        }

        public void editBook(Book book)
        {
            _geneRepo.update(book);
        }
        public void deleteBook(int id)
        {
            _geneRepo.delete(findBook(id));
        }
    }
}