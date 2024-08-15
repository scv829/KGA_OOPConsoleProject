using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public class GrowingTool : Item
    {
        private int effect;              // 성장 수치
        private int maximumCapacity;     // 최대 용량
        private int currentCapacity;     // 현재 용량

        public int Effect { get { return effect; } set { effect = value; } }
        public int Capacity { get { return maximumCapacity; } set { maximumCapacity = value; currentCapacity = value; } }
        public int CurrentCapacity { get { return currentCapacity; } }

        public GrowingTool(string name, int price, string description) : base(name, price, description)
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
    }
}
