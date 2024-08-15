using GeometryFarm.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public class FarmingTool : Item
    {
        public FarmingTool(string name, int price, string description, ItemType type) : base(name, price, description, type)
        {
        }

        public override void PrintImage()
        {
            Console.Write($"{"FT",2}");
        }
    }
}
