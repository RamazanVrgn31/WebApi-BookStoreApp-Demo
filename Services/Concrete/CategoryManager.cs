using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObject;
using Entities.DataTransferObject.CategoryDto;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contrats;
using Services.Contrats;

namespace Services.Concrete
{
    
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CategoryManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            return await _manager.Category.GetAllCategoriesAsync(trackChanges);
        }

        public async Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges)
        {
            return await _manager.Category.GetOneCategoryByIdAsync(id, trackChanges);
        }

        public async Task<CategoryDto> CreateOneCategoryAsync(CategoryDtoForInsertion categoryDto)
        {
            var entity = _mapper.Map<Category>(categoryDto);
            if (categoryDto is null) 
                throw new ArgumentNullException(nameof(categoryDto));
          
             _manager.Category.CreateOneCategory(entity);
            await _manager.SaveAsync();
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task DeleteOneCategoryAsync(int id,bool trackChanges)
        {
            var category = await GetOneCategoryByIdAndCheckExists(id,trackChanges);
            _manager.Category.DeleteOneCategory(category);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneCategoryAsync(int id, CategoryDtoForUpdate categoryDto, bool trackChanges)
        {
            var category = await GetOneCategoryByIdAndCheckExists(id,trackChanges);
            
            category = _mapper.Map<Category>(categoryDto);
            _manager.Category.UpdateOneCategory(category);
            await _manager.SaveAsync();
        }




        private async Task<Category> GetOneCategoryByIdAndCheckExists(int id, bool trackChanges)
        {
            var category = await _manager.Category.GetOneCategoryByIdAsync(id, trackChanges);

            if (category is null)
                throw new CategoryNotFoundException(id);
            return category;
        }

      
    }
}
