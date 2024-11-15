﻿using EPStoredP.Entities;

namespace EPStoredP.Repositories.Interface
{
    public interface IProductService
    {

        public Task<List<Product>> GetProductListAsync();
        public Task<IEnumerable<Product>> GetProductByIdAsync(int Id);
        public Task<int> AddProductAsync(Product product);
        public Task<int> UpdateProductAsync(Product product);
        public Task<int> DeleteProductAsync(int Id);
    }
}
