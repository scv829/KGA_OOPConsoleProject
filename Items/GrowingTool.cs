using GeometryFarm.Enums;
using GeometryFarm.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public class GrowingTool : Item, IUpgrade
    {
        private int effect;              // 성장 수치
        private int maximumCapacity;     // 최대 용량
        private int currentCapacity;     // 현재 용량

        private ToolRankType toolRankType { get; set; }         // 아이템 랭크 <- 팩토리로 생성할 때 사용하는 키

        public int Effect { get { return effect; } set { effect = value; } }
        public int Capacity { get { return maximumCapacity; } set { maximumCapacity = value; currentCapacity = value; } }
        public int CurrentCapacity { get { return currentCapacity; } }
        public ToolRankType ToolRank { get { return toolRankType; } set { toolRankType = value; } }

        private int upgradeGold;            // 업그레이드 비용
        private List<(Item, int)> materialList;    // 업그레이드 재료


        public GrowingTool(string name, int price, string description, ItemType type) : base(name, price, description, type)
        {
        }

        public void Grow(Seed seed)
        {
            if(currentCapacity > 0)
            {
                seed.Grow(this.effect);
                currentCapacity--;
            }
        }

        public void Charge()
        {
            currentCapacity = maximumCapacity;
        }

        public void SetIngredient(int gold, List<(Item, int)> items)
        {
            this.materialList = items;
            this.upgradeGold = gold;
        }

        public List<bool> CheckIngredient(int playerGold, Inventory playerInventory)
        {
            List<bool> result = new List<bool>();
            
            if (playerGold < this.upgradeGold) result.Add(false);
            else result.Add(true);

            foreach((Item, int) item in materialList)
            {

                if(playerInventory.ContainItem(item)) result.Add(true);
                else result.Add(false);
            }
 
            return result;
        }

        public Item Upgrade(int upgradeGold, Inventory playerInventory)
        {
            // 재료를 제거하는 로직
            return GrowingToolFactory.Instantiate(toolRankType + 1);
        }

        public bool hasNext()
        {
            return toolRankType + 1 < ToolRankType.MAX;
        }

        public override void PrintImage()
        {
            switch(this.toolRankType)
            {
                case ToolRankType.Normal:
                    break;
                case ToolRankType.Copper:
                    Console.BackgroundColor= ConsoleColor.DarkRed;
                    break;
                case ToolRankType.Steel:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case ToolRankType.Golden:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
            }
            Console.Write($"{"GT",2}");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
