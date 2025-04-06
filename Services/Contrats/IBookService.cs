using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.Models;

namespace Services.Contrats
{
     public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks(bool trackChanges);
        BookDto GetOneBookById(int id, bool trackChanges);
        BookDto CreateOneBook(BookDtoForInsertion book);
        void DeleteOneBook(int id, bool trackChanges);
        void UpdateOneBook(BookDtoForUpdate bookDto ,int id, bool trackChanges);
        (BookDtoForUpdate bookDtoForUpdate, Book book) GetBookForPatch(int id, bool trackChanges);

        void SaveChangesForUpdate(BookDtoForUpdate bookDto, Book book);
    }
}
