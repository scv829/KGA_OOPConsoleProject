using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public class GrowingTool : Item
    {
        private int effect;

        public GrowingTool(string name, int price, string description) : base(name, price, description)
        {
            effect = 1;
        }


        /// <summary>
        /// 도형을 성장시키는 도구 ex) 물뿌리개
        /// </summary>
        public void Grow(Seed seed)
        {
            seed.Grow(this.effect);
        }
    }
}
