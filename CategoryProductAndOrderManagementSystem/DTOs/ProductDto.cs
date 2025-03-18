﻿using CategoryProductAndOrderManagementSystem.Modles;

namespace CategoryProductAndOrderManagementSystem.DTOs
{
	public class ProductDto
	{
		public string Name { get; set; }
		public double Price { get; set; }
		public List<int> Categories { get; set; }
	}
}
