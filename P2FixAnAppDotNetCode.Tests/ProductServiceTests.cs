using System.Collections.Generic;
using System.Linq;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Repositories;
using P2FixAnAppDotNetCode.Models.Services;
using Xunit;

namespace P2FixAnAppDotNetCode.Tests
{
    /// <summary>
    /// The ProductService test class
    /// </summary>
    public class ProductServiceTests
    {

        [Fact]
        public void Product()
        {
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            var products = productService.GetAllProducts();

            //Assert.IsType<List<Product>>(products);
            Assert.IsType<List<Product>>(products);
        }

        [Fact]
        public void UpdateProductQuantities()
        {
            Cart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            IEnumerable<Product> products = productService.GetAllProducts();
            cart.AddItem(products.First(p => p.Id == 1), 1);
            cart.AddItem(products.First(p => p.Id == 3), 2);
            cart.AddItem(products.First(p => p.Id == 5), 3);

            productService.UpdateProductQuantities(cart);
            Assert.Equal(9, products.First(p => p.Id == 1).Stock);
            Assert.Equal(28, products.First(p => p.Id == 3).Stock);
            Assert.Equal(47, products.First(p => p.Id == 5).Stock);


            //do a second run adding items to cart. Resetting the repo and service and cart
            //will simulate the process from the front end perspective
            //here testing that product stock values are decreasing for each cart checkout, not just a single time
            cart = new Cart();
            // productRepository = new ProductRepository(); // A CONFIRMER
            // productService = new ProductService(productRepository, orderRepository); // PAREIL (Transent > Singleton?)

            products = productService.GetAllProducts();
            cart.AddItem(products.First(p => p.Id == 1), 1);
            cart.AddItem(products.First(p => p.Id == 3), 2);
            cart.AddItem(products.First(p => p.Id == 5), 3);

            productService.UpdateProductQuantities(cart);
            Assert.Equal(8, products.First(p => p.Id == 1).Stock);
            Assert.Equal(26, products.First(p => p.Id == 3).Stock);
            Assert.Equal(44, products.First(p => p.Id == 5).Stock);
        }

        [Fact]
        public void GetProductById()
        {
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);
            int id = 3;

            Product product = productService.GetProductById(id);

            Assert.Same("JVC HAFX8R Headphone", product.Name);
            Assert.Equal(69.99, product.Price);
        }
    }
}



//User
//[Fact]
//public void UpdateProductQuantities()
//{
//    Cart cart = new Cart();
//    IProductRepository productRepository = new ProductRepository();
//    IOrderRepository orderRepository = new OrderRepository();
//    IProductService productService = new ProductService(productRepository, orderRepository);

//    List<Product> products = productService.GetAllProducts();
//    cart.AddItem(GetProductById(products, 1), 1);
//    cart.AddItem(GetProductById(products, 3), 2);
//    cart.AddItem(GetProductById(products, 5), 3);

//    productService.UpdateProductQuantities(cart);

//    Assert.Equal(9, GetProductById(products, 1).Stock);
//    Assert.Equal(28, GetProductById(products, 3).Stock);
//    Assert.Equal(47, GetProductById(products, 5).Stock);

//    // Deuxième exécution en ajoutant des articles au panier
//    // Réinitialisation du repo, du service et du panier
//    // Cela simule le processus du point de vue du front-end
//    // Teste ici que les valeurs de stock des produits diminuent à chaque passage en caisse, pas seulement une seule fois
//    cart = new Cart();
//    productRepository = new ProductRepository();
//    productService = new ProductService(productRepository, orderRepository);
//    products = productService.GetAllProducts();
//    cart.AddItem(GetProductById(products, 1), 1);
//    cart.AddItem(GetProductById(products, 3), 2);
//    cart.AddItem(GetProductById(products, 5), 3);
//    productService.UpdateProductQuantities(cart);
//    Assert.Equal(8, GetProductById(products, 1).Stock);
//    Assert.Equal(26, GetProductById(products, 3).Stock);
//    Assert.Equal(44, GetProductById(products, 5).Stock);
//}