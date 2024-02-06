using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest.Serialization;
using P2FixAnAppDotNetCode.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        ProductRepository productRepository = new ProductRepository();
        private List<Product> listProduct = new List<Product>();

        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts().ToList();
        }


        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            foreach (Product product in GetAllProducts())
            {
                if (product.Id == id)
                {
                    return product;
                }
            }
            return null;
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            foreach (CartLine cartLine in cart.Lines)
            {
                _productRepository.UpdateProductStocks(cartLine.Product.Id, cartLine.Quantity);
            }
        }
    }
}
