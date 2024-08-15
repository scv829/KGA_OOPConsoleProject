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
        public bool isGrew { get; private set; }


        private Crop parent;
        public Crop Parent { get { return parent; } set { parent = value; } }

        public Seed( string name, int price, string description, int time) : base(name, price, description)
        {
            this.growingTime = time;
            this.isGrew = false;
        }

        /// <summary>
        /// 식물이 성장하는 시간 
        /// </summary>
        /// <param name="time">성장시키는 도구의 감소 효과</param>
        public void Grow(int time)
        {
            growingTime -= time;

            if (growingTime <= 0)
            {
                isGrew = true;
            }

        }

        
    }
}
