using GeometryFarm.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public abstract class Item
    {
        public string name { get; private set; }
        public int price { get; private set; }
        public string description { get; private set; }

        public ItemType itemType { get; private set; }

        public Item(string name, int price, string description, ItemType type)
        {
            this.name = name;
            this.price = price;
            this.description = description;
            this.itemType = type;
        }

        public abstract void PrintImage();
    }
}
