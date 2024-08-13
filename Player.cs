using GeometryFarm.Enums;
using GeometryFarm.Items;
using GeometryFarm.Util;
using System;
using System.Collections.Generic;
using System.Data;
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
        private Pos playerPos;


        public Player(string name)
        {
            this.name = name;
            this.inventory = new Item[6];
            this.gold = 3000;
            this.fatigue = 0;
            this.inventoryIndex = 0;
            this.playerPos = new Pos();
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

        public Pos GetPos() { return playerPos; }

        public void SetPos(int x ,int y) { this.playerPos.setPos(x, y); }

        public string Interection(MapTileType type)
        {
            switch(type)
            {
                case MapTileType.Crop:
                    return "농작물을 수확합니다";
                case MapTileType.Seed:
                    return "씨앗에 물을줍니다";
                case MapTileType.Ground:
                    return "땅을 한번 만져봅니다";
                case MapTileType.Field:
                    return "밭을 바라봅니다";
            }
            return "";
        }

    }
}
