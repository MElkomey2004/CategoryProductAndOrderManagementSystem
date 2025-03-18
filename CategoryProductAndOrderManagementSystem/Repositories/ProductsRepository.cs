using AutoMapper;
using CategoryProductAndOrderManagementSystem.Data;
using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Modles;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CategoryProductAndOrderManagementSystem.Repositories
{
	public class ProductsRepository : IProductsRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly ICategoriesRepository _categoriesRepository;
        public ProductsRepository(ApplicationDbContext dbContext , IMapper mapper , ICategoriesRepository categoriesRepository)
        {
			_dbContext = dbContext;	
			_mapper = mapper;	
			_categoriesRepository = categoriesRepository;	

            
        }

		public async Task<ProductDto> Create(ProductDto productDto)
		{
			var categories = await _dbContext.Categories
			.Where(c => productDto.Categories.Contains(c.Id))
				.ToListAsync();

			if (categories.Count != productDto.Categories.Count)
			{
				throw new ArgumentException("One or more category IDs do not exist.");
			}

			var product = new Product
			{
				Name = productDto.Name,
				Price = productDto.Price,
				ProductCategories = categories.Select(c => new ProductCategory
				{
					CategoryId = c.Id
				}).ToList()
			};

			_dbContext.Products.Add(product);
			await _dbContext.SaveChangesAsync();

			return new ProductDto
			{
				Name = product.Name,
				Price = product.Price,
				Categories = product.ProductCategories.Select(pc => pc.CategoryId).ToList()
			};
		}

	

		public async Task<bool> Delete(int id)
		{
			var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
			if(product == null)
			{
				return false;
			}

	
			_dbContext.Products.Remove(product);
			await _dbContext.SaveChangesAsync();

			return true;	

		}

		public async Task<IEnumerable<ProductDto>> GetAll()
		{
		return 	await _dbContext.Products.Include(p => p.ProductCategories)
					
					.Select(p => new ProductDto
					{
						Name = p.Name,
						Price = p.Price,
						Categories = p.ProductCategories.Select(c => c.CategoryId).ToList() // Convert categories to [number, number]
					})
					.ToListAsync();

		}

		public async Task<ProductDto> GetById(int id)
		{
			var product = await _dbContext.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefaultAsync(x => x.Id == id);
			if (product == null)
				return null;

			return new ProductDto
			{
				Name = product.Name,
				Price = product.Price,
				Categories = product.ProductCategories.Select(pc => pc.CategoryId).ToList()
			};

		}




		public async Task<ProductDto> Update(int Id, ProductDto proproductDtoduct)
		{
			var productFromDb = await _dbContext.Products
					.Include(p => p.ProductCategories)
					.FirstOrDefaultAsync(p => p.Id == Id);
			if (productFromDb == null)
			{
				throw new ArgumentException("Product not found.");
			}

			// Update product properties
			productFromDb.Name = proproductDtoduct.Name;
			productFromDb.Price = proproductDtoduct.Price;



			var CategoriesThatExistFromThisProduct = productFromDb.ProductCategories.Select(c => c.CategoryId).ToList();

			var TheNewCategoryIsUpdated = proproductDtoduct.Categories; // new list[numbrer , number]


			var CategoriesToAdd = TheNewCategoryIsUpdated.Except(CategoriesThatExistFromThisProduct).ToList(); //[1 , 2 , 3 , 4] -[2 , 3 , 4] = 1

			var CategoriesToRemove = CategoriesThatExistFromThisProduct.Except(TheNewCategoryIsUpdated).ToList();//[1 , 2 , 3 , 4] - [5] =[1 , 2 , 3 , 4]


			foreach (var categoryId in CategoriesToAdd) 
			{
				var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
				if (category != null)
				{
					productFromDb.ProductCategories.Add(new ProductCategory
					{
						ProductId = productFromDb.Id,
						CategoryId = categoryId,
					});
				}
				else
				{
					throw new ArgumentException($"Category with ID {categoryId} not found.");
				}
			
			}


			foreach(var categoryId in CategoriesToRemove)
			{
				var category = productFromDb.ProductCategories.FirstOrDefault(c => c.CategoryId == categoryId);	
				if(category != null)
				{
					productFromDb.ProductCategories.Remove(category);
					
				}
				else
				{
					throw new ArgumentException($"Category with ID {categoryId} not found.");
				}

			}
			 _dbContext.Products.Update(productFromDb);

			await _dbContext.SaveChangesAsync();


			var updatedProduct = await _dbContext.Products
				.Include(p => p.ProductCategories)
					.ThenInclude(pc => pc.Category)
				.FirstOrDefaultAsync(p => p.Id == Id);

			return new ProductDto
			{
				Name = updatedProduct.Name,
				Price = updatedProduct.Price,
				Categories = updatedProduct.ProductCategories.Select(pc => pc.CategoryId).ToList()
			};
		}


	}
}
