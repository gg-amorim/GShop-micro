using AutoMapper;
using GShop.ProductApi.DTOs;
using GShop.ProductApi.Models;
using GShop.ProductApi.Repositories.Interfaces;
using GShop.ProductApi.Services.Interfaces;

namespace GShop.ProductApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private ResponseDTO _response;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _response = new ResponseDTO();
        }

        public async Task<ResponseDTO> GetCategoriesAsync()
        {
            try
            {
                var listCategories = await _categoryRepository.GetAll();
                _response.Data = _mapper.Map<IEnumerable<CategoryDTO>>(listCategories);
                return _response;
            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
                return _response;
            }
           
        }

        public async Task<ResponseDTO> GetCategoriesProductsAsync()
        {
            try
            {
                var listCategories = await _categoryRepository.GetCategoriesProducts();
                _response.Data = _mapper.Map<IEnumerable<CategoryDTO>>(listCategories);
                return _response;
            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
                return _response;
            }
            
        }

        public async Task<ResponseDTO> GetCategoryByIdAsync(Guid id)
        {
           var category = await _categoryRepository.GetById(id);
            if(category is null)
            {
                _response.Success = false;
                _response.Message = "Category not found.";
                return _response;
            }
            _response.Data = _mapper.Map<CategoryDTO>(category);
            return _response;
        }

        public async Task<ResponseDTO> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);
                await _categoryRepository.Create(category);
                categoryDTO.CategoryId = category.CategoryId;
                _response.Data = categoryDTO;
                return _response;
            }
            catch (Exception e) 
            {
                _response.Success = false;
                _response.Message = e.Message;
                return _response;
            }
           
        }

        public async Task<ResponseDTO> UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);
                await _categoryRepository.Update(category);
                _response.Data = categoryDTO;
                return _response;
            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
                return _response; 
            }
           
        }

        public async Task<ResponseDTO> RemoveCategoryAsync(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                if (category is null)
                {
                    _response.Success = false;
                    _response.Message = "Category not found.";
                    return _response;
                }
                await _categoryRepository.Delete(category.CategoryId);
                return _response;
            }
             catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
                return _response;
            }
        }
    }
}
