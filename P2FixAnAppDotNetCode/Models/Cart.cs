using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        private List<CartLine> listCartLine = new List<CartLine>();

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
            return listCartLine;
        }
        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(int orderLineId, Product product, int quantity)
        {
            bool productExistsInCart = false;

            foreach (var cartLine in listCartLine)
            {
                if (cartLine.Product == product)
                {
                    cartLine.Quantity += quantity;
                    productExistsInCart = true;
                    break;
                }
            }

            if (!productExistsInCart)
            {
                var newCartLine = new CartLine(orderLineId, product, quantity);
                listCartLine.Add(newCartLine);
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

            foreach (var cartLine in listCartLine)
            {
                Product product = cartLine.Product;
                totalValue += (product.Price);
            }

            return totalValue;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            if (listCartLine.Count == 0)
            {
                return 0.0;
            }

            double total = GetTotalValue();

            double average = total / listCartLine.Count;

            return average;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // TODO implement the method
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

        public int GenerateOrderId()
        {
            int maxOrderId = 0;

            foreach (var CartLine in listCartLine)
            {
                if (CartLine.OrderLineId > maxOrderId)
                {
                    maxOrderId = CartLine.OrderLineId;
                }
            }
            return maxOrderId + 1;
        }
    }



    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartLine(int orderLineId, Product product, int quantity)
        {
            OrderLineId = orderLineId;
            Product = product;
            Quantity = quantity;
        }
    }
}
