using DomainLayer.Interfaces;
using DomainLayer.DomainModels;
using RepositoryLayer.Contexts;
using System;

namespace RepositoryLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly LibraryContext _context;

        public IGenericRepo<Book> BookRepos { get; set; }
        public IGenericRepo<Author> AuthorRepos { get; set; }
        public IAuthorRepo AuthorRepo { get; }
        public IBookRepo BookRepo { get; }

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
            AuthorRepo = new AuthorRepo(_context);
            BookRepo = new BookRepo(_context);
            BookRepos = new GenericRepo<Book>(_context);
            AuthorRepos = new GenericRepo<Author>(_context);
        }

        public void commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}