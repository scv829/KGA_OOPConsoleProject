using GeometryFarm.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public class GrowingToolFactory
    {
        public static GrowingTool Instantiate(ToolRankType type)
        {
            if(type == ToolRankType.Normal)
            {
                GrowingTool tool = new GrowingTool("물뿌리개" , 0 , "작물에게 물을 주는 도구입니다.", ItemType.Tool);
                tool.Effect = 1;
                tool.Capacity = 3;
                tool.ToolRank = type;

                tool.SetIngredient(2000, new List<(Item, int)>());
                return tool;
            }
            else if (type ==  ToolRankType.Copper)
            {
                GrowingTool tool = new GrowingTool("구리 물뿌리개", 2000, "구리로 만들어진 작물에게 물을 주는 도구입니다.", ItemType.Tool);
                tool.Effect = 2;
                tool.Capacity = 3;
                tool.ToolRank = type;

                tool.SetIngredient(5000, new List<(Item, int)>());
                return tool;
            }
            else if (type ==  ToolRankType.Steel)
            {
                GrowingTool tool = new GrowingTool("강철 물뿌리개", 5000, "강철로 만들어진 작물에게 물을 주는 도구입니다.", ItemType.Tool);
                tool.Effect = 2;
                tool.Capacity = 7;
                tool.ToolRank = type;

                tool.SetIngredient(10000, new List<(Item, int)>());
                return tool;
            }
            else if (type ==  ToolRankType.Golden)
            {
                GrowingTool tool = new GrowingTool("황금 물뿌리개", 10000, "황금으로 만들어진 작물에게 물을 주는 도구입니다.", ItemType.Tool);
                tool.Effect = 3;
                tool.Capacity = 7;
                tool.ToolRank = type;

                return tool;
            }
            else if (type == ToolRankType.MAX)
            {
                return null;
            }
            else
            {
                throw new Exception("해당 이름의 아이템이 없습니다. [GrowingTool]");
            }
        }
    }
}
