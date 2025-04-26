using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject.CategoryDto
{
    public record CategoryDto
    {
        public int CategoryId { get; init; }
        public String? CategoryName { get; init; }
    }
}
