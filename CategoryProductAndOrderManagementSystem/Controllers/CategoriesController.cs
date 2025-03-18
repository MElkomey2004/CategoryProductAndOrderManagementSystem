using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryProductAndOrderManagementSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
			_categoriesRepository = categoriesRepository;	
            
        }


        [HttpPost]

        public async Task<IActionResult> Create([FromBody]CategoryDto categoryDto)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

		    await _categoriesRepository.Create(categoryDto);
			

			return Ok("the element with create successfully");
        }

		[HttpGet]
		[Route("{Id:int}")]

		public async Task<IActionResult> GetById([FromRoute] int Id)
		{
			var category = await _categoriesRepository.GetById(Id);	
			if (category == null)
			{
				return NotFound();
			}

			return Ok(category);
		}



		[HttpDelete]
		[Route("{Id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{

			var category = await _categoriesRepository.Delete(Id);

			if (!category)
				return NotFound();

			return Ok($"The Category With Id: {Id} is Deleted Successfully");
		}


		[HttpGet]

		public async Task<IActionResult> GetAll()
		{
			var categories = await _categoriesRepository.GetAll();

			return Ok(categories);
	
		}



		[HttpPut]
		[Route("{Id:int}")]

		public async Task<IActionResult> Update([FromRoute] int Id , [FromBody] CategoryDto categoryDto)
		{
			var category = await _categoriesRepository.Update(Id, categoryDto);

			if(category == null)
			{
				return NotFound();
			}
			return Ok(category);
		}



	}
}
