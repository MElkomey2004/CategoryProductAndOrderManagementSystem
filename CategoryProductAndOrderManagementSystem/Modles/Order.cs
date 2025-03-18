using System.ComponentModel.DataAnnotations;

namespace CategoryProductAndOrderManagementSystem.Modles
{
	public class Order
	{
		public int Id { get;set; }
		public int UserId { get; set; }
		public List<OrderItem> Items { get; set;}
	}
}
