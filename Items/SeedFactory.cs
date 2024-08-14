using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public class SeedFactory
    {
        public static Seed Instantiate(string name)
        {
            Random random = new Random();
            double rand = random.NextDouble();
            if (name == "네모")
            {
                Seed seed = new Seed("네모 씨앗", 50, "네모 농산물의 씨앗입니다.", 4);
                seed.Parent = (0.9 < rand) ? CropFactory.Instantiate("꽉찬 네모") : CropFactory.Instantiate("네모");
                return seed;
            }
            else if (name == "세모")
            {
                Seed seed = new Seed("세모 씨앗", 30, "세모 농산물의 씨앗입니다.", 3);
                seed.Parent = (0.9 < rand) ? CropFactory.Instantiate("꽉찬 세모") : CropFactory.Instantiate("세모");
                return seed;
            }
            else if (name == "원형")
            {
                Seed seed = new Seed("원형 씨앗", 30, "원형 농산물의 씨앗입니다.", 2);
                seed.Parent = (0.9 < rand) ? CropFactory.Instantiate("꽉찬 원형") : CropFactory.Instantiate("원형");
                return seed;
            }
            else
            {
                Console.WriteLine("해당 이름의 아이템이 없습니다.");
                return null;
            }
        }
    }
}
