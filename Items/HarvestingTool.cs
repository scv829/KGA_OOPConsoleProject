using GeometryFarm.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public class HarvestingTool : Item
    {
        public HarvestingTool(string name, int price, string description) : base(name, price, description, ItemType.Tool)
        {
        }

        public override void PrintImage()
        {
            throw new NotImplementedException();
        }
    }
}
