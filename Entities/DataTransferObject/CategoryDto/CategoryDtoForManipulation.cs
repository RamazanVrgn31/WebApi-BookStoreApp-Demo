using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject.CategoryDto
{
    public abstract record CategoryDtoForManipulation
    {
        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(40, ErrorMessage = "Title must be less than 100 characters.")]
        public string CategoryName { get; init; }
    }
}
