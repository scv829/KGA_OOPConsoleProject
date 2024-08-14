using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{

    public class Seed : Item
    {
        private int growingTime;

        private Crop parent;
        public Crop Parent { get { return parent; } set { parent = value; } }

        public Seed( string name, int price, string description, int time) : base(name, price, description)
        {
            this.growingTime = time;
        }

        /// <summary>
        /// 식물이 성장하는 시간 
        /// </summary>
        /// <param name="time">성장시키는 도구의 감소 효과</param>
        public bool Grow(int time)
        {
            if (growingTime == 0)
            {
                return true;
            }
            else
            {
                growingTime -= time;
                return false;
            }
        }
    }
}
