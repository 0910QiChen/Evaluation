using DomainLayer.Interfaces;
using DomainLayer.DomainModels;
using RepositoryLayer.Contexts;
using RepositoryLayer.Repositories;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace ServiceLayer.Services
{
    public class AuthorService : IAuthorService
    {
        LibraryContext _context = new LibraryContext();

        private readonly IUnitOfWork _unitOfWork;

        private Mapper mapper;

        public AuthorService()
        {
            _unitOfWork = new UnitOfWork(_context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookDTO>();
                cfg.CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.BookDTOs, opt => opt.MapFrom(src => src.Books));
                cfg.CreateMap<AuthorDTO, Author>();
            });
            mapper = new Mapper(config);
        }

        public List<AuthorDTO> getAllAuthors()
        {
            var authorDTO = mapper.Map<List<AuthorDTO>>(_unitOfWork.AuthorRepo.getAll());
            return authorDTO;
        }

        public void createAuthor(AuthorDTO authorDTO)
        {
            /*var author = new author
            {
                name = authordto.name,
                age = authordto.age,
                country = authordto.country,
            };*/
            var author = mapper.Map<Author>(authorDTO);
            _unitOfWork.AuthorRepo.createAuthor(author);
            _unitOfWork.commit();
        }

        public AuthorDTO findAuthor(int? id)
        {
            var author = mapper.Map<AuthorDTO>(_unitOfWork.AuthorRepo.findAuthor(id));
            if(author == null)
            {
                return null;
            }
            return author;
        }

        public void editAuthor(AuthorDTO authorDTO)
        {
            var author = mapper.Map<Author>(authorDTO);
            if(author != null)
            {
                _unitOfWork.AuthorRepo.editAuthor(author);
                _unitOfWork.commit();
            }
        }

        public void deleteAuthor(int id)
        {
            _unitOfWork.AuthorRepo.deleteAuthor(id);
            _unitOfWork.commit();
        }
    }
}