﻿using Microsoft.EntityFrameworkCore;
using ShopProduct.Api.Data;
using ShopProduct.Api.Entities;
using ShopProduct.Api.Repositories.Contracts;

namespace ShopProduct.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _context;

        public ProductRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var toDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if(toDelete == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            _context.Products.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            if(products.Count < 1)
            {
                return new List<Product>();
            }
            return products;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();  
        }

    }
}
