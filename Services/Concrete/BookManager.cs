using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contrats;
using Services.Contrats;

namespace Services.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public BookDto CreateOneBook(BookDtoForInsertion bookDto)
        {

            var entity = _mapper.Map<Book>(bookDto);
            if (bookDto is null)
                throw new ArgumentNullException(nameof(bookDto));

            _manager.Book.CreateOneBook(entity);
            _manager.Save();
            return _mapper.Map<BookDto>(entity);
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            //check entity 
            var entity = _manager.Book.GetOneBookById(id,trackChanges);
            if (entity is null)
               throw new BookNotFoundException(id); 

            _manager.Book.DeleteOneBook(entity);
            _manager.Save();

        }

        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
            var books= _manager.Book.GetAllBooks(trackChanges);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public (BookDtoForUpdate bookDtoForUpdate, Book book) GetBookForPatch(int id, bool trackChanges)
        {
            var book = _manager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return (bookDtoForUpdate, book);
        }

        public BookDto GetOneBookById(int id, bool trackChanges)
        {
            var book = _manager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            return _mapper.Map<BookDto>(book);
        }

        public void SaveChangesForUpdate(BookDtoForUpdate bookDto, Book book)
        {
            _mapper.Map(bookDto, book); 
            _manager.Save();
        }

        public void UpdateOneBook(BookDtoForUpdate bookDto, int id, bool trackChanges)
        {
            //check entity 
            var entity = _manager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
                throw new BookNotFoundException(id);

            //map entity to dto
            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.UpdateOneBook(entity);
            _manager.Save();
        }
    }
}
