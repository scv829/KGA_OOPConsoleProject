using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GeometryFarm.Items
{
    public class Crop : Item
    {
        private char shape;
        private int growingTime;
        private bool isGrew;

        public Crop(string name, int price, string description) : base(name, price, description)
        {
        }

        /// <summary>
        /// 식물이 성장하는 시간 
        /// </summary>
        /// <param name="time">성장시키는 도구의 감소 효과</param>
        public void Grow(int time)
        {
            if (growingTime == 0)
            {
                isGrew = true;
            }
            else
            {
                growingTime -= time;
            }
        }

    }
}
