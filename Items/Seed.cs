using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public class Seed : Item
    {
        private Crop parnet;
        public Seed( string name, int price, string description) : base(name, price, description)
        {
        }

        public void SetParent(Crop crop)
        {
            parnet = crop;
        }

        public Crop GetParent ()
        {
            return parnet;
        }

    }
}
