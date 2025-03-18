using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryProductAndOrderManagementSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
        private readonly IOrdersRepository _ordersRepository;   
        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;   
            
        }



        [HttpPost]

        public async Task<IActionResult> Create([FromBody] OrderDto orderDto)
        {

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _ordersRepository.Create(orderDto);


			return Ok("the element with create successfully");
		}


		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var orders = await _ordersRepository.GetAll();	

			return Ok(orders);
		
		}


		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var order = await _ordersRepository.GetById(id);

			if(order == null)
			{
				return NotFound();
			}
			return Ok(order);
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var order = await _ordersRepository.Delete(id);

			if (order == false)
			{
				return NotFound();
			}
			return Ok("This Order is Deleted successfully");
		}



		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id , [FromBody] OrderDto orderDto)
		{
			var order = await _ordersRepository.Update(id , orderDto);

			if (order == null)
			{
				return NotFound();
			}
			return Ok(order);
		}
	}
}
