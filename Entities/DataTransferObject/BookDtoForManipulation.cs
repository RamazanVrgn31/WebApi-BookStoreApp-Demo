using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(100, ErrorMessage = "Title must be less than 100 characters.")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters.")]
        public String Title { get; init; }

        [Required(ErrorMessage = "Price is a required field.")]
        [Range(1, 1000)]
        public decimal Price { get; init; }
    }
}
