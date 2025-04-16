using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

namespace Presentation.Controllers
{
    //[ApiVersion("2.0", Deprecated =true)] //Deprecation of the version
    //Api versioning Convention
    [ApiController]
    //[Route("api/{v:apiversion}/books")] //URL versioning
    [Route("api/books")]

    public class BookV2Controller :ControllerBase
    {
        private readonly IServiceManager _manager;

        public BookV2Controller(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            var booksV2 = books.Select(b =>new 
            {
                Id =b.Id,
                Title = b.Title
            });
            return Ok(booksV2);
        }

    }
}
