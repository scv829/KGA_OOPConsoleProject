using GeometryFarm.Enums;
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
        public char shape;
        public char Shape { get{ return shape; } set { shape = value; } }

        public Crop(string name, int price, string description, ItemType type) : base(name, price, description, type)
        {
            
        }

        public override void PrintImage()
        {
            Console.Write($"{shape, -2}");
        }
    }
}
