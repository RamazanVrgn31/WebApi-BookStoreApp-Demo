using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.LinkModels;
using Services.Contrats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Entities.Models;
using Microsoft.Net.Http.Headers;


namespace Services.Concrete
{
    public class BookLinks : IBookLinks
    {
        private readonly IDataShaper<BookDto> _dataShaper;

        public BookLinks(IDataShaper<BookDto> dataShaper)
        {
            _dataShaper = dataShaper;
        }



        public LinkResponse TryGenerateLinks(IEnumerable<BookDto> booksDto, string fields, HttpContext httpContext)
        {
            var shapedBooks = ShapeData(booksDto, fields);
            if (ShouldGenerateLinks(httpContext))
            {
                return ReturnLinkedBooks(booksDto, fields, httpContext, shapedBooks);
            }

            return ReturnShapedBooks(shapedBooks);


        }



        private LinkResponse ReturnLinkedBooks(IEnumerable<BookDto> booksDto, string fields, HttpContext httpContext, List<Entity> shapedBooks)
        {
            var bookDtoList = booksDto.ToList();
            for (int i = 0; i < bookDtoList.Count; i++)
            {
                var bookLinks = CreateForBook(httpContext, bookDtoList[i], fields);
                shapedBooks[i].Add("Links", bookLinks);
            }

            var bookCollection = new LinkCollectionWrapper<Entity>(shapedBooks);
            CreateForBook(httpContext,bookCollection);
            return new LinkResponse()
            {
                HasLinks = true,
                LinkedEntities = bookCollection,
            };
        }
        private LinkCollectionWrapper<Entity> CreateForBook(HttpContext httpContext, LinkCollectionWrapper<Entity> bookCollectionWrapper)
        {
            bookCollectionWrapper.Links.Add(
                new Link()
                {
                    Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}"+ $"?pageSize=10&pageNumber=1",
                    Rel = "self",
                    Method = "GET",
                });


            return bookCollectionWrapper;
        }
        private List<Link> CreateForBook(HttpContext httpContext, BookDto bookDto, string fields)
        {
            var links = new List<Link>()
            {
                new Link()
                {
                    Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}"+ $"/{bookDto.Id}",
                    Rel = "self",
                    Method = "GET",
                },
               new Link()
                {
                    Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                    Rel = "create",
                    Method = "POST",
                }
            };

            return links;
        }

        private LinkResponse ReturnShapedBooks(List<Entity> shapedBooks)
        {
            return new LinkResponse()
            {
                ShapedEntities = shapedBooks,
            };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType
                    .SubTypeWithoutSuffix
                    .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<BookDto> bookDto, string fields)
        {
            return _dataShaper
                .ShapeData(bookDto, fields)
                .Select(b => b.Entity)
                .ToList();
        }
    }
}
