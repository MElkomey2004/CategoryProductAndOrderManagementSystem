using System.ComponentModel.DataAnnotations;

namespace CategoryProductAndOrderManagementSystem.Modles
{
	public class Category
	{
		public int Id { get;set; }
		[Required(ErrorMessage = "Category name is required.")]
		[StringLength(100, ErrorMessage = "Category name must be between 3 and 100 characters.", MinimumLength = 3)]
		public string Name { get;set; }
	    public int? ParentCategoryId { get; set; }


		public List<ProductCategory> ProductCategories { get; set; }
	}
}
