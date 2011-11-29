using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkMyWine
{
    public class Cart
    {
        private readonly List<OrderLine> linesToProcess = new List<OrderLine>();
        
        public int TotalItems
        {
            get { return linesToProcess.Count; }
        }

        public decimal TotalPrice { get; private set; }

        public User User { get; set; }

        public void CheckOut()
        {
            if (linesToProcess.Count == 0)
                throw new InvalidOperationException("Cannot check out cart with no wines");

            if (User == null)
                throw new InvalidOperationException("Cannot perform cart operations on missing user");

            if (User is InvalidUser)
                throw  new InvalidOperationException("User invalid");

            TotalPrice = linesToProcess.Sum(line => line.Price * line.Quantity);
        }

        public void AddLineToCart(OrderLine line)
        {
            if (!line.IsWineInStock)
                throw new InvalidOperationException("Wine is not in out stock.");

            if (line.Quantity <= 0)
                throw new InvalidOperationException(string.Format("Invalid Quantity: {0}", line.Quantity));

            linesToProcess.Add(line);
        }
    }
}