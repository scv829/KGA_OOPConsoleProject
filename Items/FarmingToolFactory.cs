using GeometryFarm.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public class FarmingToolFactory
    {
        public static  FarmingTool Instantiate(ToolRankType type)
        {
            if (type == ToolRankType.Normal)
            {
                FarmingTool tool = new FarmingTool("호미", 0, "토양을 경작시키는 도구입니다.", ItemType.Tool);

                return tool;
            }
            else if (type == ToolRankType.Copper)
            {
                FarmingTool tool = new FarmingTool("구리 호미", 2000, "구리로 만들어진 토양을 경작시키는 도구입니다.", ItemType.Tool);

                return tool;
            }
            else if (type == ToolRankType.Steel)
            {
                FarmingTool tool = new FarmingTool("강철 호미", 5000, "강철로 만들어진 토양을 경작시키는 도구입니다.", ItemType.Tool);

                return tool;
            }
            else if (type == ToolRankType.Golden)
            {
                FarmingTool tool = new FarmingTool("황금 호미", 10000, "황금으로 만들어진 토양을 경작시키는 도구입니다.", ItemType.Tool);

                return tool;
            }
            else
            {
                throw new Exception("해당 이름의 아이템이 없습니다. [FarmingTool]");
            }
        }

    }
}
