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


		public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;

		}

		public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
		{
			try
			{
				var listCategories = await _categoryRepository.GetAll();
				return _mapper.Map<IEnumerable<CategoryDTO>>(listCategories);

			}
			catch (Exception e)
			{

				return null;
			}

		}

		public async Task<IEnumerable<CategoryDTO>> GetCategoriesProductsAsync()
		{
			try
			{
				var listCategories = await _categoryRepository.GetCategoriesProducts();
				return _mapper.Map<IEnumerable<CategoryDTO>>(listCategories);
				
			}
			catch (Exception e)
			{

				return null;
			}

		}

		public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id)
		{
			var category = await _categoryRepository.GetById(id);
			if (category is null)
				return null;
			return _mapper.Map<CategoryDTO>(category);
			
		}

		public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDTO)
		{
			try
			{
				var category = _mapper.Map<Category>(categoryDTO);
				await _categoryRepository.Create(category);
				categoryDTO.CategoryId = category.CategoryId;
				return categoryDTO;
				
			}
			catch (Exception e)
			{
				return null;
			}

		}

		public async Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categoryDTO)
		{
			try
			{
				var category = _mapper.Map<Category>(categoryDTO);
				await _categoryRepository.Update(category);
				return categoryDTO;
			}
			catch (Exception e)
			{

				return null;
			}

		}

		public async Task<bool> RemoveCategoryAsync(Guid id)
		{
			try
			{
				var category = await _categoryRepository.GetById(id);
				if (category is null)
				{
					return false;
				}
				await _categoryRepository.Delete(category.CategoryId);
				return true;
			}
			catch (Exception e)
			{

				return false;
			}
		}
	}
}
