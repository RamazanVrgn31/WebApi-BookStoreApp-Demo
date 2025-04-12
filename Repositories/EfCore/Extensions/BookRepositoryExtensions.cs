using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using System.Linq.Dynamic.Core;

namespace Repositories.EfCore.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) =>
            books.Where(book => book.Price >= minPrice && book.Price <= maxPrice);

        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            if (String.IsNullOrWhiteSpace(searchTerm))
                return books;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return books.Where(book => book.Title.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(b => b.Id);

            var orderQuery = OrderQueryBuilder.CreateQueryBuilder<Book>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return books.OrderBy(b => b.Id);
            return books.OrderBy(orderQuery);
        }
    }
}
