namespace Entities.DataTransferObject.CategoryDto
{
    public record CategoryDtoForUpdate : CategoryDtoForManipulation
    {
        public int CategoryId { get; init; }
    }
}
