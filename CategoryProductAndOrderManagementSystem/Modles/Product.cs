namespace CategoryProductAndOrderManagementSystem.Modles
{
	public class Product
	{
		
		public int Id { get; set; }
		public string Name { get; set; }	
		public double Price { get; set; }

		public List<ProductCategory> ProductCategories { get; set; }


	}
}
