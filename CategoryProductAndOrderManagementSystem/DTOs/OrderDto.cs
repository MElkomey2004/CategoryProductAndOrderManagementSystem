namespace CategoryProductAndOrderManagementSystem.DTOs
{
	public class OrderDto
	{
		
		public int UserId { get; set; }
		public List<OrderItemDto> Items { get; set; }
	}
}
