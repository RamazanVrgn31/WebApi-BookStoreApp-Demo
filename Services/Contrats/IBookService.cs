using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Services.Contrats
{
     public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool trackChanges);
        Book GetOneBookById(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void DeleteOneBook(int id, bool trackChanges);
        void UpdateOneBook(Book book ,int id, bool trackChanges);
    }
}
