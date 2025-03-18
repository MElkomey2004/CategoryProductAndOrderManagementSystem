using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Modles;

namespace CategoryProductAndOrderManagementSystem.Repositories
{
	public interface IProductsRepository
	{
		Task<ProductDto> Create(ProductDto productDto);
		Task<ProductDto> Update(int Id , ProductDto productDto);
		Task<bool> Delete(int id);
		Task<IEnumerable<ProductDto>> GetAll();
		Task<ProductDto> GetById(int id);	
	}
}
