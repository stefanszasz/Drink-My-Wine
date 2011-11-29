using System;

namespace DrinkMyWine
{
    public class Wine
    {
        public Wine()
        {
            Id = Guid.NewGuid().ToString();
            InStock = true;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public bool InStock { get; set; }
    }
}