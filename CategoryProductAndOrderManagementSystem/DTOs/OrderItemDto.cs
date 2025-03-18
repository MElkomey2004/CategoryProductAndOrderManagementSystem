using CategoryProductAndOrderManagementSystem.Modles;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoryProductAndOrderManagementSystem.DTOs
{
	public class OrderItemDto
	{

		public int ProductId { get; set; }

		public int Quantity { get; set; }
	}
}
