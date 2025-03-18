using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Modles;

namespace CategoryProductAndOrderManagementSystem.Repositories
{
	public interface ICategoriesRepository
	{
		Task Create(CategoryDto category);
		Task<CategoryDto> Update(int Id, CategoryDto category);
		Task<bool> Delete(int id);
		Task<IEnumerable<CategoryDto>> GetAll();
		Task<CategoryDto> GetById(int id);
		Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids);
	}
		
}
