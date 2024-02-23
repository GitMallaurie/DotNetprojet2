
namespace P2FixAnAppDotNetCode.Models
{
    public interface ICart
    {
        void AddItem(Product product, int quantity);

        void RemoveLine(Product product);

        CartLine FindProductInCartLines(int productId);

        void Clear();

        double GetTotalValue();

        int GenerateOrderId();

        double GetAverageValue();
    }
}