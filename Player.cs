using GeometryFarm.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm
{
    public class Player
    {
        private Item[] inventory;
        public int gold { get; private set; }
        public int fatigue { get; private set; }
        private string name;
        private int inventoryIndex;

        public Player(string name)
        {
            this.name = name;
            this.inventory = new Item[6];
            this.gold = 3000;
            this.fatigue = 0;
            this.inventoryIndex = 0;
        }

        public bool isInventoryFull()
        {
            return inventoryIndex >= inventory.Length;
        }

        public void BuyItem(Item item)
        {
            inventory[inventoryIndex++] = item;
            gold -= item.price;
        }

    }
}
