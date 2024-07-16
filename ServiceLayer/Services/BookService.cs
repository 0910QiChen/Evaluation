using DomainLayer.DomainModels;
using DomainLayer.Interfaces;
using RepositoryLayer.Contexts;
using RepositoryLayer.Repositories;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using AutoMapper;
using System.Collections.Generic;

namespace ServiceLayer.Services
{
    public class BookService : IBookService
    {
        LibraryContext _context = new LibraryContext();

        private readonly IUnitOfWork _unitOfWork;

        private Mapper mapper;

        public BookService()
        {
            _unitOfWork = new UnitOfWork(_context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookDTO>();
                cfg.CreateMap<BookDTO, Book>();
            });
            mapper = new Mapper(config);
        }

        public List<BookDTO> getAllBooks()
        {
            var bookDTO = mapper.Map<List<BookDTO>>(_unitOfWork.BookRepo.getAll());
            return bookDTO;
        }

        public void createBook(BookDTO bookDTO)
        {
            var book = mapper.Map<Book>(bookDTO);
            _unitOfWork.BookRepo.createBook(book);
            _unitOfWork.commit();
        }

        public BookDTO findBook(int? id)
        {
            var book = mapper.Map<BookDTO>(_unitOfWork.BookRepo.getByID(id));
            if (book == null)
            {
                return null;
            }
            return book;
        }

        public void editBook(BookDTO bookDTO)
        {
            var book = mapper.Map<Book>(bookDTO);
            if (book != null)
            {
                _unitOfWork.BookRepo.update(book);
                _unitOfWork.commit();
            }
        }

        public void deleteBook(int id)
        {
            _unitOfWork.BookRepo.deleteBook(id);
            _unitOfWork.commit();
        }
    }
}