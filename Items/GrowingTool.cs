using GeometryFarm.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    {
        private int effect;              // 성장 수치
        private int maximumCapacity;     // 최대 용량
        private int currentCapacity;     // 현재 용량

        private ToolRankType toolRankType { get; set; }

        public int Effect { get { return effect; } set { effect = value; } }
        public int Capacity { get { return maximumCapacity; } set { maximumCapacity = value; currentCapacity = value; } }
        public int CurrentCapacity { get { return currentCapacity; } }
        public ToolRankType ToolRank { get { return toolRankType; } set { toolRankType = value; } }

        

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
