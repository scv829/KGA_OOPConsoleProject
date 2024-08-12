using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public abstract class Item
    {
        private string name;
        public int price { get; private set; }
        private string description;

        public Item(string name, int price, string description)
        {
            this.name = name;
            this.price = price;
            this.description = description;
        }

        
    }
}
