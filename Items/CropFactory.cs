using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GeometryFarm.Enums;

namespace GeometryFarm.Items
{
    public class CropFactory
    {
        public static Crop Instantiate(string name)
        {
            if(name == "네모")
            {
                Crop crop = new Crop("네모 농작물", 150, "네모 농작물", ItemType.Crop);
                crop.Shape = 'u';
                return crop;
            }
            else if (name == "꽉찬 네모")
            {
                Crop crop = new Crop("꽉찬 네모 농작물", 300, "속이 꽉찬 네모 농작물", ItemType.Crop);
                crop.Shape = 'U';
                return crop;
            }
            else if (name == "세모")
            {
                Crop crop = new Crop("세모 농작물", 100, "세모 농작물", ItemType.Crop);
                crop.Shape = 'v';
                return crop;
            }
            else if (name == "꽉찬 세모")
            {
                Crop crop = new Crop("꽉찬 세모 농작물", 200, "속이 꽉찬 세모 농작물", ItemType.Crop);
                crop.Shape = 'V';
                return crop;
            }
            else if (name == "원형")
            {
                Crop crop = new Crop("원형 농작물", 50, "원형 농작물", ItemType.Crop);
                crop.Shape = 'o';
                return crop;
            }
            else if (name == "꽉찬 원형")
            {
                Crop crop = new Crop("꽉찬 원형 농작물", 100, "속이 꽉찬 원형 농작물", ItemType.Crop);
                crop.Shape = 'O';
                return crop;
            }
            else
            {
                throw new Exception("해당 이름의 아이템이 없습니다. [Crop]");
            }
        }
    }
}
