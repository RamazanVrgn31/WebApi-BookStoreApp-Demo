using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
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


        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {

            var entity = _mapper.Map<Book>(bookDto);
            if (bookDto is null)
                throw new ArgumentNullException(nameof(bookDto));

            _manager.Book.CreateOneBook(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            //check entity 
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);

            _manager.Book.DeleteOneBook(book);
            await _manager.SaveAsync();

        }

        public async Task<(IEnumerable<BookDto> books, MetaData metadata)> GetAllBooksAsync(BookParameters bookParameters ,bool trackChanges)
        {
            if (!bookParameters.ValidPriceRange)
                throw new PriceOutOfRangeBadRequestException();

            var booksWithMetadata = await _manager.Book.GetAllBooksAsync(bookParameters , trackChanges);
             var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetadata);

            return(booksDto, booksWithMetadata.MetaData);
        }

        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);

            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return (bookDtoForUpdate, book);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);

            return _mapper.Map<BookDto>(book);
        }

        public async Task SaveChangesForUpdateAsync(BookDtoForUpdate bookDto, Book book)
        {
            _mapper.Map(bookDto, book);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneBookAsync(BookDtoForUpdate bookDto, int id, bool trackChanges)
        {
            //check entity 
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
            //map entity to dto
            book = _mapper.Map<Book>(bookDto);

            _manager.Book.UpdateOneBook(book);
            await _manager.SaveAsync();
        }



        private async Task<Book> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
        {
            var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);

            if (book is null)
                throw new BookNotFoundException(id);
            return book;
        }
    }
}
