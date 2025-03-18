using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Modles;

namespace CategoryProductAndOrderManagementSystem.Repositories
{
	public interface IOrdersRepository
	{
		Task Create(OrderDto order);
		Task<OrderDto> Update(int Id, OrderDto order);
		Task<bool> Delete(int id);
		Task<IEnumerable<OrderDto>> GetAll();
		Task<OrderDto> GetById(int id);
	
	}
		
}
