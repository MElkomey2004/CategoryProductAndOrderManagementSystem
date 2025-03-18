using AutoMapper;
using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Modles;
using CategoryProductAndOrderManagementSystem.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryProductAndOrderManagementSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductsRepository _productsRepository;
        public ProductsController(IProductsRepository productsRepository )
        {
			_productsRepository = productsRepository;	
        }


        [HttpPost]

        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
		
		
			var product = await _productsRepository.Create(productDto);
			return Ok(product);
		}

		[HttpGet]
		[Route("{Id:int}")]

		public async Task<IActionResult> GetById([FromRoute] int Id)
		{
			var product = await _productsRepository.GetById(Id);	
			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}



		[HttpDelete]
		[Route("{Id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{

			var product = await _productsRepository.Delete(Id);

			if (!product)
				return NotFound();

			return Ok($"The product With Id: {Id} is Deleted Successfully");
		}


		[HttpGet]

		public async Task<IActionResult> GetAll()
		{
			var products = await _productsRepository.GetAll();

			return Ok(products);
	
		}



		[HttpPut]
		[Route("{Id:int}")]

		public async Task<IActionResult> Update([FromRoute] int Id , [FromBody] ProductDto productDto)
		{
			var product = await _productsRepository.Update(Id, productDto);

			if(product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}



	}
}
