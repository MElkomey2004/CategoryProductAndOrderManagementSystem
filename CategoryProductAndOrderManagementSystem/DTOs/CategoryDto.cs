using System.ComponentModel.DataAnnotations;

namespace CategoryProductAndOrderManagementSystem.DTOs
{
	public class CategoryDto
	{
		public int Id { get;set; }	
		[Required(ErrorMessage = "Category name is required.")]
		[StringLength(100, ErrorMessage = "Category name must be between 3 and 100 characters.", MinimumLength = 3)]
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
	}
}
