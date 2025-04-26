using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject.CategoryDto;
using Entities.Models;

namespace Services.Contrats
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetOneCategoryByIdAsync(int id ,bool trackChanges);
        Task<CategoryDto> CreateOneCategoryAsync(CategoryDtoForInsertion categoryDto);
        Task UpdateOneCategoryAsync(int id ,CategoryDtoForUpdate categoryDto, bool trackChanges);
        Task DeleteOneCategoryAsync(int id, bool trackChanges);
    }
}
