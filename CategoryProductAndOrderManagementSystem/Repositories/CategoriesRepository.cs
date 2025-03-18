using AutoMapper;
using CategoryProductAndOrderManagementSystem.Data;
using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Modles;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CategoryProductAndOrderManagementSystem.Repositories
{
	public class CategoriesRepository : ICategoriesRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;
        public CategoriesRepository(ApplicationDbContext dbContext , IMapper mapper)
        {
			_dbContext = dbContext;	
			_mapper = mapper;	
            
        }
		public async Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids)
		{
			return await _dbContext.Categories
				.Where(c => ids.Contains(c.Id))
				.ToListAsync();
		}
		public async Task Create(CategoryDto category)
		{
			var categoryToDb = _mapper.Map<Category>(category);
			await _dbContext.Categories.AddAsync(categoryToDb);
			await _dbContext.SaveChangesAsync();

			
		}

		public async Task<bool> Delete(int id)
		{
			var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if(category == null)
			{
				return false;
			}

	
			_dbContext.Categories.Remove(category);
			await _dbContext.SaveChangesAsync();

			return true;	

		}

		public async Task<IEnumerable<CategoryDto>> GetAll()
		{
			var categories = await _dbContext.Categories.ToListAsync();

			return _mapper.Map<List<CategoryDto>>(categories);
			
		}

		public async Task<CategoryDto> GetById(int id)
		{
			var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if (category == null)
				return null;

			return _mapper.Map<CategoryDto>(category);

		}



		public async Task<CategoryDto> Update(int Id, CategoryDto category)
		{
			var categoryFromDb =  await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);

			if (categoryFromDb == null)
				return null;

		    _mapper.Map<Category>(category);

			await _dbContext.SaveChangesAsync();

			return category;
		}
	}
}
