using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_REST_API.Data;
using Simple_REST_API.Models;

namespace Simple_REST_API.Service
{
    public class ProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<List<ProductDTO>> GetAllProducts()
        {
            return await _context.Products
                .Select(p => new ProductDTO
                {
                 Id = p.Id,
                   Name = p.Name,
                   Description = p.Description ?? "",
                   Price = p.Price
                }).ToListAsync();
        }
        public async Task<ProductDTO?> GetProductById(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDTO
                { 
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description ?? "",
                    Price = p.Price
                }).FirstOrDefaultAsync();
        }
        public async Task<ProductDTO?> AddProduct(ProductDTO product)
        {
            if(string.IsNullOrWhiteSpace(product.Name) || !product.Price.HasValue)
            {
                return null;
            }
            var newProduct = new Product(product.Name, product.Description ?? "", product.Price.Value);
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return product with { Id = newProduct.Id };
        }
        public async Task<ProductDTO?> UpdateProduct(int Id, ProductDTO Product)
        {

            var OldProduct = await _context.Products.FindAsync(Id);

            if (OldProduct == null)
                return null;


            OldProduct.Name = Product.Name ?? OldProduct.Name;
            OldProduct.Description = Product.Description ?? OldProduct.Description;
            OldProduct.Price = Product.Price ?? OldProduct.Price;

            await _context.SaveChangesAsync();

            
            return Product with
            {
                Id = OldProduct.Id,
                Name = OldProduct.Name,
                Price = OldProduct.Price,
                Description = OldProduct.Description
            };


        }
        public async Task<bool> DeleteProduct(int Id)
        {
            var Product = await _context.Products.FindAsync(Id);
            if (Product == null)
                return false;
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
