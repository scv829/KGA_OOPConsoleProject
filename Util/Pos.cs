using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Util
{
    public struct Pos
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetPos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsEqual(int x, int y)
        {
            return (this.x == x && this.y == y);
        }

    }
}
