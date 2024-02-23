using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.NodeServices.HostingModels;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        private List<CartLine> listCartLines = new List<CartLine>();

        public int OrderId { get; private set; }

        public Cart()
        {
           OrderId = GenerateOrderId();
        }

        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return listCartLines;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            CartLine existingCartLine = FindProductInCartLines(product.Id);

            if (existingCartLine != null)
            {
                existingCartLine.Quantity += quantity;
            }
            else
            {
                var newCartLine = new CartLine(product, quantity);
                listCartLines.Add(newCartLine);
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            double totalValue = 0.0;

            foreach (var cartLine in listCartLines)
            {
                double totalLine = 0.0;
                totalLine += cartLine.Product.Price * System.Convert.ToDouble(cartLine.Quantity);
                totalValue += totalLine;
            }

            return totalValue;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            if (listCartLines.Count == 0)
            {
                return 0.0;
            }

            double total = GetTotalValue();

            var totalOfProduct = 0;

            foreach (var cartLine in listCartLines)
            {
                totalOfProduct += cartLine.Quantity;
            }

            double average = total / totalOfProduct;

            return average;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public CartLine FindProductInCartLines(int productId)
        {
            foreach (var cartLine in listCartLines)
            {
                if (cartLine.Product.Id == productId)
                {
                    return cartLine;
                }
            }
            return null;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }

        /// <summary>
        /// Generate an ID for the cart
        /// </summary>
        /// <returns></returns>
        public int GenerateOrderId()
        {
            int maxOrderId = 0;

            foreach (var cartLine in listCartLines)
            {
                if (OrderId > maxOrderId)
                {
                    maxOrderId = OrderId;
                }
            }
            return maxOrderId + 1;
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartLine(Product product, int quantity)
        {

            Product = product;
            Quantity = quantity;
        }
    }
}
