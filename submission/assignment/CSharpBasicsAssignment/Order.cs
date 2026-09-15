using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasicsAssignment
{
    public class Order
    {
        public int OrderId;
        public string CustomerName = string.Empty;
        public int Quantity;
        public decimal UnitPrice;
        public decimal TotalPrice;
        public bool IsPaid;
        public double DiscountPercent;
        public string ShippingCity = string.Empty;
        public char Priority; // 'H', 'M', or 'L'
        public long ItemCode;

        public void CalculateTotal()
        {
            decimal discountFactor = 1 - ((decimal)DiscountPercent / 100);
            TotalPrice = Quantity * UnitPrice * discountFactor;
        }

        public void PrintSummary()
        {
            Console.WriteLine($"Order #{OrderId} | {CustomerName} | Total: {TotalPrice:C} | Paid: {IsPaid}");
        }
    }
}
