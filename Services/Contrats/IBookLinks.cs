using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contrats
{
    public interface IBookLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<BookDto> bookDto, string fields, HttpContext httpContext);
    }
}
