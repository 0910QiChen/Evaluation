using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DomainLayer.DomainModels;
using Evaluation.Models;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

namespace Evaluation.Controllers
{
    public class LibraryController : Controller
    {
        public IAuthorService authorService;
        public IBookService bookService;

        private Mapper DTOMapper;

        public LibraryController()
        {
            authorService = new AuthorService();
            bookService = new BookService();

            var DTOConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuthorDTO, AuthorVM>().
                ForMember(dest => dest.BookVMs, opt => opt.MapFrom(src => src.BookDTOs));
                cfg.CreateMap<BookDTO, BookVM>();
                cfg.CreateMap<AuthorVM, AuthorDTO>();
                cfg.CreateMap<BookVM, BookDTO>();
            });
            DTOMapper = new Mapper(DTOConfig);
        }

        public ActionResult Library(string currentFilter, string searchString)
        {
            if (searchString != null)
            {
                ViewBag.CurrentFilter = searchString;
            }
            else
            {
                searchString = currentFilter;
            }
            var authors = DTOMapper.Map<List<AuthorVM>>(authorService.getAllAuthors());
            var library = new List<AuthorBookVM>();
            foreach (var author in authors)
            {
                if (author.BookVMs.Count < 1)
                {
                    var authorBookVM = new AuthorBookVM()
                    {
                        AuthorID = author.AuthorID,
                        Age = author.Age,
                        Country = author.Country,
                    };
                    if (string.IsNullOrEmpty(searchString) || author.Country.Contains(searchString) || author.Name.Contains(searchString))
                    {
                        library.Add(authorBookVM);
                    }
                }
                else
                {
                    foreach (var book in author.BookVMs)
                    {
                        var authorBookVM = new AuthorBookVM()
                        {
                            BookID = book.BookID,
                            Title = book.Title,
                            Genre = book.Genre,
                            PublishDate = book.PublishDate,
                            AuthorID = author.AuthorID,
                            Name = author.Name,
                            Age = author.Age,
                            Country = author.Country,
                        };
                        if (string.IsNullOrEmpty(searchString) || book.Title.Contains(searchString) || book.Genre.Contains(searchString) || author.Name.Contains(searchString) || author.Country.Contains(searchString))
                        {
                            library.Add(authorBookVM);
                        }
                    }
                }
            }
            return View(library);
        }

        public ActionResult Authors(string currentFilter, string searchString)
        {
            if (searchString != null)
            {
                ViewBag.CurrentFilter = searchString;
            }
            else
            {
                searchString = currentFilter;
            }
            var authors = DTOMapper.Map<List<AuthorVM>>(authorService.getAllAuthors());
            if (!string.IsNullOrEmpty(searchString))
            {
                var authorList = authors.Where(a => a.Name.Contains(searchString) || a.Country.Contains(searchString));
                return View(authorList);
            }
            return View(authors);
        }

        public ActionResult Books(string currentFilter, string searchString)
        {
            if (searchString != null)
            {
                ViewBag.CurrentFilter = searchString;
            }
            else
            {
                searchString = currentFilter;
            }
            var books = DTOMapper.Map<List<BookVM>>(bookService.getAllBooks());
            if (!string.IsNullOrEmpty(searchString))
            {
                var bookList = books.Where(b => b.Title.Contains(searchString) || b.Genre.Contains(searchString));
                return View(bookList);
            }
            return View(books);
        }

        public ActionResult AuthorDetails(int? id)
        {
            if(id == null)
            {
                TempData["NoAuthorID"] = "No author ID was provided!";
                return RedirectToAction("Authors");
            }
            var author = DTOMapper.Map<AuthorVM>(authorService.findAuthor(id));
            if(author == null)
            {
                TempData["NoAuthor"] = $"No author found with ID {id}";
                return RedirectToAction("Authors");
            }
            return View(author);
        }

        public ActionResult BookDetails(int? id)
        {
            if (id == null)
            {
                TempData["NoBookID"] = "No book ID was provided!";
                return RedirectToAction("Books");
            }
            var book = DTOMapper.Map<BookVM>(bookService.findBook(id));
            if (book == null)
            {
                TempData["NoBook"] = $"No book found with ID {id}.";
                return RedirectToAction("Books");
            }
            return View(book);
        }

        public ActionResult AuthorCreate()
        {
            return View();
        }

        public ActionResult BookCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorCreate([Bind(Include ="Name,Age,Country")] AuthorVM authorVM)
        {
            if (ModelState.IsValid)
            {
                var authorDTO = DTOMapper.Map<AuthorDTO>(authorVM);
                authorService.createAuthor(authorDTO);
                return RedirectToAction("Library");
            }
            return View(authorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookCreate([Bind(Include = "Title,Genre,PublishDate,AuthorID")] BookVM bookVM)
        {
            if (ModelState.IsValid)
            {
                var author = authorService.findAuthor(bookVM.AuthorID);
                if (author == null)
                {
                    ModelState.AddModelError("AuthorID", "Author not found. Please enter a valid author.");
                    return View(bookVM);
                }
                var bookDTO = DTOMapper.Map<BookDTO>(bookVM);
                bookService.createBook(bookDTO);
                return RedirectToAction("Library");
            }
            return View(bookVM);
        }

        public ActionResult AuthorEdit(int? id)
        {
            if (id == null)
            {
                TempData["NoAuthorID"] = "No author ID was provided!";
                return RedirectToAction("Authors");
            }
            var author = DTOMapper.Map<AuthorVM>(authorService.findAuthor(id));
            if (author == null)
            {
                TempData["NoAuthor"] = $"No author found with ID {id}";
                return RedirectToAction("Authors");
            }
            return View(author);
        }

        public ActionResult BookEdit(int? id)
        {
            if (id == null)
            {
                TempData["NoBookID"] = "No book ID was provided!";
                return RedirectToAction("Books");
            }
            var book = DTOMapper.Map<BookVM>(bookService.findBook(id));
            if (book == null)
            {
                TempData["NoBook"] = $"No book found with ID {id}.";
                return RedirectToAction("Books");
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorEdit([Bind(Include ="AuthorID,Name,Age,Country")] AuthorVM authorVM)
        {
            var author = DTOMapper.Map<AuthorDTO>(authorVM);
            authorService.editAuthor(author);
            return RedirectToAction("Library");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookEdit([Bind(Include = "BookID,Title,Genre,PublishDate,AuthorID")] BookVM bookVM)
        {
            var author = authorService.findAuthor(bookVM.AuthorID);
            if(author == null)
            {
                ModelState.AddModelError("AuthorID", "Author not found. Please enter a valid author.");
                return View(bookVM);
            }
            var book = DTOMapper.Map<BookDTO>(bookVM);
            bookService.editBook(book);
            return RedirectToAction("Library");
        }

        public ActionResult AuthorDelete(int? id)
        {
            if (id == null)
            {
                TempData["NoAuthorID"] = "No author ID was provided!";
                return RedirectToAction("Authors");
            }
            var author = DTOMapper.Map<AuthorVM>(authorService.findAuthor(id));
            if (author == null)
            {
                TempData["NoAuthor"] = $"No author found with ID {id}";
                return RedirectToAction("Authors");
            }
            return View(author);
        }

        public ActionResult BookDelete(int? id)
        {
            if (id == null)
            {
                TempData["NoBookID"] = "No book ID was provided!";
                return RedirectToAction("Books");
            }
            var book = DTOMapper.Map<BookVM>(bookService.findBook(id));
            if (book == null)
            {
                TempData["NoBook"] = $"No book found with ID {id}.";
                return RedirectToAction("Books");
            }
            return View(book);
        }

        [HttpPost, ActionName("AuthorDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorDeleteConfirm(int id)
        {
            authorService.deleteAuthor(id);
            return RedirectToAction("Library");
        }

        [HttpPost, ActionName("BookDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult BookDeleteConfirm(int id)
        {
            bookService.deleteBook(id);
            return RedirectToAction("Library");
        }
    }
}