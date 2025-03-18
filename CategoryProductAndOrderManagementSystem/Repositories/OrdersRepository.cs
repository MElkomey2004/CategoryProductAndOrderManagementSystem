using AutoMapper;
using CategoryProductAndOrderManagementSystem.Data;
using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Modles;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;

namespace CategoryProductAndOrderManagementSystem.Repositories
{
	public class OrdersRepository : IOrdersRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly ICategoriesRepository _categoriesRepository;
        public OrdersRepository(ApplicationDbContext dbContext , IMapper mapper , ICategoriesRepository categoriesRepository)
        {
			_dbContext = dbContext;	
			_mapper = mapper;	
			_categoriesRepository = categoriesRepository;	

            
        }

		public async Task Create(OrderDto order)
		{
			var orderToDb = _mapper.Map<Order>(order);

			await _dbContext.Orders.AddAsync(orderToDb);
			await _dbContext.SaveChangesAsync();

			
		}

	

		public async Task<bool> Delete(int id)
		{
			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
			if(order == null)
				return false;


			 _dbContext.Orders.Remove(order);
			await _dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<OrderDto>> GetAll()
		{
			return await _dbContext.Orders
				.Include(p => p.Items)
				.Select(p => new OrderDto
				{
					UserId = p.UserId,
					Items = p.Items.Select(o => new OrderItemDto
					{
						Quantity = o.Quantity,
						ProductId = o.ProductId
					}).ToList()
				}).ToListAsync();
		}

		public async Task<OrderDto> GetById(int id)
		{
			var order = await _dbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(x => x.Id == id);
			if (order == null)
				return null;


			return new OrderDto
			{
				UserId = order.UserId,
				Items = order.Items.Select(o => new OrderItemDto { ProductId = o.ProductId, Quantity = o.Quantity }).ToList()

			};

		}


		public async Task<OrderDto> Update( int Id, OrderDto order)
		{
			var upateOrder = await _dbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(x => x.Id == Id);
			if (upateOrder == null)
				return null;
			upateOrder.UserId = order.UserId;
			upateOrder.Items = order.Items.Select(o => new OrderItem { Quantity = o.Quantity, ProductId = o.ProductId }).ToList();


			 _dbContext.Orders.Update(upateOrder);
			await _dbContext.SaveChangesAsync();



			return new OrderDto { UserId = upateOrder.UserId, Items = upateOrder.Items.Select(o => new OrderItemDto
			{
				Quantity = o.Quantity,
				ProductId = o.ProductId,

			}) .ToList()};
		}


	}
}
