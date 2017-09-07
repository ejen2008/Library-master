using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.Infrastructure;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BLL.Services
{
    public class AuthorService
    {
        private AuthorRepository _authorRepository;
        private string _exceprionDosenotFound = "Author doesn't found";
        private string _exceptionDoesnotCreated = "Author doesn't created";

        public AuthorService(AuthorRepository repository)
        {
            var rep = new AuthorRepository(new DAL.EF.LibraryContext());
            _authorRepository = repository;
        }

        public AuthorDTO GetAuthor(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Don't installed id", "");
            }
            Author author = _authorRepository.Get(id.Value);

            ValidExceptionFound(author);

            Mapper.Initialize(a => a.CreateMap<Author, AuthorDTO>());
            AuthorDTO authorDTO = Mapper.Map<Author, AuthorDTO>(author);
            return authorDTO;
        }
        public IEnumerable<AuthorDTO> GetAuthors()
        {
            Mapper.Initialize(a => a.CreateMap<Author, AuthorDTO>());
            IEnumerable<Author> author = _authorRepository.GetAll();
            List<AuthorDTO> authorsDTO = Mapper.Map<IEnumerable< Author>, List<AuthorDTO>>(author);
            return authorsDTO;
        }

        public void CreateAuthor(AuthorDTO authorDTO)
        {
            ValidExceptionCreated(authorDTO);

            Author author = MappAuthor(authorDTO);
            _authorRepository.Create(author);
        }
        public void Update(AuthorDTO authorDTO)
        {
            ValidExceptionCreated(authorDTO);

            Author author = _authorRepository.Get(authorDTO.Id);

            ValidExceptionFound(author);

            author = null;
            author = MappAuthor(authorDTO);
            _authorRepository.Update(author);
        }

        private void ValidExceptionCreated(AuthorDTO authorDTO)
        {
            if (authorDTO == null)
            {
                throw new ValidationException(_exceptionDoesnotCreated, "");
            }
        }
        private void ValidExceptionFound(Author author)
        {
            if (author == null)
            {
                throw new ValidationException(_exceprionDosenotFound, "");
            }
        }

        private Author MappAuthor(AuthorDTO authorDTO)
        {
            Mapper.Initialize(a => a.CreateMap<AuthorDTO, Author>());
            Author author = Mapper.Map<AuthorDTO, Author>(authorDTO);
            return author;
        }

        public void Delete(AuthorDTO authorDTO)
        {
            ValidExceptionCreated(authorDTO);

            Author author = _authorRepository.Get(authorDTO.Id);

            ValidExceptionFound(author);

            _authorRepository.Delete(authorDTO.Id);
        }
    }
}