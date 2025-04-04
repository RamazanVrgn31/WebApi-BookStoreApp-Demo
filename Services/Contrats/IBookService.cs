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
        Book GetOneBookById(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void DeleteOneBook(int id, bool trackChanges);
        void UpdateOneBook(BookDtoForUpdate bookDto ,int id, bool trackChanges);
    }
}
