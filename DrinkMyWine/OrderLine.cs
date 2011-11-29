using System;

namespace DrinkMyWine
{
    public class OrderLine
    {
        private readonly Wine _wineToBuy;

        public OrderLine(Wine wineToBuy)
        {
            if (wineToBuy == null)
                throw new ArgumentNullException("wineToBuy");

            _wineToBuy = wineToBuy;
            Quantity = 1;
        }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public bool IsWineInStock
        {
            get { return _wineToBuy.InStock; }
        }
    }
}