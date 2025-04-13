using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contrats
{
     public interface IBookService
    {
        Task<(IEnumerable<ExpandoObject> books,MetaData metadata)> GetAllBooksAsync(BookParameters bookParameter, bool trackChanges);
        Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges);
        Task<BookDto> CreateOneBookAsync(BookDtoForInsertion book);
        Task DeleteOneBookAsync(int id, bool trackChanges);
        Task UpdateOneBookAsync(BookDtoForUpdate bookDto ,int id, bool trackChanges);
        Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetBookForPatchAsync(int id, bool trackChanges);

        Task SaveChangesForUpdateAsync(BookDtoForUpdate bookDto, Book book);
    }
}
