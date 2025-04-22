using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Cache;
using Presentation.ActionFilters;
using Services.Contrats;

namespace Presentation.Controllers
{
    //[ApiVersion("1.0")]
    //Api versioning Convention

    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    //[Route("api/{v:apiversion}/books")] //URL versioning
    [Route("api/books")] // Header versioning
    [ApiExplorerSettings(GroupName = "v1")]

    //[ResponseCache(CacheProfileName = "5mins")]  //Expriration Cache with CacheProfile
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public , MaxAge =90)] //Validation Cache with Attribute
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }
        
        [Authorize]
        [HttpHead]
        [HttpGet(Name = "GetAllBooksAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //[ResponseCache(Duration = 60)]  //Expriation Cache
        public async Task<IActionResult> GetAllBooksAsync( [FromQuery] BookParameters bookParameters)
        {
            var linkParameters = new LinkParameters()
            {
                BookParameters = bookParameters,
                HttpContext = HttpContext
            };
            var result = await _manager.BookService.GetAllBooksAsync(linkParameters ,false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize( result.metadata)); 
            return result.linkResponse.HasLinks ? 
                Ok(result.linkResponse.LinkedEntities) : 
                Ok(result.linkResponse.ShapedEntities);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookByIdAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _manager.BookService.GetOneBookByIdAsync(id, false);
            return Ok(book);
        }


        [Authorize(Roles="Admin,Editor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateBookAsync")]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookDtoForInsertion bookDto)
        {

            var book=await _manager.BookService.CreateOneBookAsync(bookDto);
            return StatusCode(201, book);
        }


        [Authorize(Roles = "Admin,Editor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {

           await _manager.BookService.UpdateOneBookAsync(bookDto, id, false);
            return NoContent(); //204
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute(Name = "id")] int id)
        {
                await _manager.BookService.DeleteOneBookAsync(id, true);
                return NoContent();
        }

        [Authorize]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartialyUpdateBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();
            var result = await _manager.BookService.GetBookForPatchAsync(id, true);


            bookPatch.ApplyTo(result.bookDtoForUpdate,ModelState);
            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.BookService.SaveChangesForUpdateAsync(result.bookDtoForUpdate,result.book);
            return NoContent();
        }

        [Authorize]
        [HttpOptions]
        public  IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE,HEAD, OPTIONS");
            return Ok();
        }
    }
}
