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

        /// <summary>
        /// 플레이어의 위치를 가져오는 메서드
        /// </summary>
        /// <returns></returns>
        public Pos GetPos() { return playerPos; }

        /// <summary>
        /// 플레이어의 위치를 새로 초기화 하는 메서드
        /// </summary>
        /// <param name="x">x위치</param>
        /// <param name="y">y위치</param>
        public void SetPos(int x ,int y) { this.playerPos.setPos(x, y); }

        /// <summary>
        /// 맵과 상호작용 하는 메서드
        /// </summary>
        /// <param name="type">상호작용하는 타일</param>
        /// <returns>[임시] 상호작용 했을 때 하는 일</returns>
        public string Interection(Enum type)
        {
            switch(type)
            {
                case FarmTileType:
                    return MapInterection((FarmTileType)type);
                case ShopTileType:
                    return ShopInterection((ShopTileType)type);
                default:
                    return "";
            }
         
        }

        /// <summary>
        /// 농장맵과 상호작용 하는 메서드
        /// </summary>
        /// <param name="type">상호작용하는 타일</param>
        /// <returns>[임시] 상호작용 했을 때 하는 일</returns>
        private string MapInterection(FarmTileType type)
        {
            switch(type)
            {
                case FarmTileType.Crop:
                    return "농작물을 수확합니다";
                case FarmTileType.Seed:
                    return "씨앗에 물을줍니다";
                case FarmTileType.Ground:
                    return "땅을 한번 만져봅니다";
                case FarmTileType.Field:
                    return "밭을 바라봅니다";
                default:
                    return "";
            }
        }

        private string ShopInterection(ShopTileType type)
        {
            switch (type)
            {
                case ShopTileType.InterectionPlace:
                    return "상인과 대화를 합니다.";
                default:
                    return "";
            }
        }
    }
}
