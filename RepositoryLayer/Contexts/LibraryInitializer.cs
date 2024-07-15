using DomainLayer.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace RepositoryLayer.Contexts
{
    public class LibraryInitializer : DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        protected override void Seed(LibraryContext _context)
        {
            var authors = new List<Author> 
            { 
                new Author{ Name = "J.K. Rowling", Age = 55, Country = "United Kingdom"},
                new Author{ Name = "George R.R. Martin", Age = 72, Country = "United States" },
                new Author{ Name = "J.R.R Tolkien", Age = 81, Country = "United Kingdom" },
                new Author{ Name = "Agatha Christie", Age = 85, Country = "United Kingdom" },
                new Author{ Name = "Stephen King", Age = 74, Country = "United States" },
                new Author{ Name = "Mark Twain", Age = 74, Country = "United States" },
                new Author{ Name = "Jane Austen", Age = 41, Country = "United Kingdom" },
            };
            authors.ForEach(a => _context.Authors.Add(a));
            _context.SaveChanges();

            var books = new List<Book> 
            {
                new Book{ Title = "Harry Potter and the Philosopher's Stone", Genre = "Fantasy", PublishDate = DateTime.Parse("1997-06-26"), AuthorID = 1 },
                new Book{ Title = "Harry Potter and the Chamber of Secrets", Genre = "Fantasy", PublishDate = DateTime.Parse("1998-07-02"), AuthorID = 1 },
                new Book{ Title = "A Game of Thrones", Genre = "Fantasy", PublishDate = DateTime.Parse("1996-08-06"), AuthorID = 2 },
                new Book{ Title = "A Clash of Kings", Genre = "Fantasy", PublishDate = DateTime.Parse("1998-11-16"), AuthorID = 2 },
                new Book{ Title = "The Hobbit", Genre = "Fantasy", PublishDate = DateTime.Parse("1937-09-21"), AuthorID = 3 },
                new Book{ Title = "The Fellowship of the Ring", Genre = "Fantasy", PublishDate = DateTime.Parse("1954-07-29"), AuthorID = 3 },
                new Book{ Title = "Murder on the Orient Express", Genre = "Mystery", PublishDate = DateTime.Parse("1934-01-01"), AuthorID = 4 },
                new Book{ Title = "And Then There Were None", Genre = "Mystery", PublishDate = DateTime.Parse("1939-11-06"), AuthorID = 4 },
                new Book{ Title = "The Shining", Genre = "Horror", PublishDate = DateTime.Parse("1977-01-28"), AuthorID = 5 },
                new Book{ Title = "It", Genre = "Horror", PublishDate = DateTime.Parse("1986-09-15"), AuthorID = 5 },
                new Book{ Title = "The Adventures of Tom Sawyer", Genre = "Adventure", PublishDate = DateTime.Parse("1876-04-30"), AuthorID = 6 },
                new Book{ Title = "Adventures of Huckleberry Finn", Genre = "Adventure", PublishDate = DateTime.Parse("1884-12-10"), AuthorID = 6 },
                new Book{ Title = "Pride and Prejudice", Genre = "Romance", PublishDate = DateTime.Parse("1813-01-28"), AuthorID = 7 },
                new Book{ Title = "Sense and Sensibility", Genre = "Romance", PublishDate = DateTime.Parse("1811-10-30"), AuthorID = 7 },
            };
            books.ForEach(b => _context.Books.Add(b));
            _context.SaveChanges();
        }
    }
}