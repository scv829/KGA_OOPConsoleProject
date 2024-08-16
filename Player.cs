using GeometryFarm.Enums;
using GeometryFarm.Items;
using GeometryFarm.Util;
using System.Net.Security;

namespace GeometryFarm
{
    public class Player
    {
        public Inventory inventory;
        private string name;
        private Pos playerPos;

        public int gold { get; private set; }
        public int fatigue { get; private set; }


        public Player(string name)
        {
            this.inventory = new Inventory();
            this.name = name;
            this.gold = 3000;
            this.fatigue = 0;
            this.playerPos = new Pos();

        }

        public void BuyItem(Item item)
        {
            inventory.InsertItem(item);
            gold -= item.price;
        }

        public bool SellItem(int sellCount)
        {
            if(1 <= sellCount && sellCount <= inventory.GetHodingCurrentItemCount())
            {
                gold += (inventory.GetHodingCurrentItem().price * sellCount);
                inventory.ConsumptionItem(sellCount);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 플레이어의 위치를 가져오는 메서드
        /// </summary>
        /// <returns></returns>
        public Pos GetPos() { return playerPos; }

        /// <summary>
        /// 플레이어의 위치를 새로 초기화 하는 메서드
        /// </summary>
        /// <param name="x">x위치</param>
        /// <param name="y">y위치</param>
        public void SetPos(int x, int y) { this.playerPos.SetPos(x, y); }

        /// <summary>
        /// 맵과 상호작용 하는 메서드
        /// </summary>
        /// <param name="type">상호작용하는 타일</param>
        /// <returns>[임시] 상호작용 했을 때 하는 일</returns>
        public string Interection(Enum type)
        {
            switch (type)
            {
                case FarmTileType:
                    return FarmMapInterection((FarmTileType)type);
                case ShopTileType:
                    return ShopInterection((ShopTileType)type);
                default:
                    return "";
            }
        }

        /// <summary>
        /// 농장맵과 상호작용 하는 메서드
        /// </summary>
        /// <param name="type">상호작용하는 타일</param>
        /// <returns>[임시] 상호작용 했을 때 하는 일</returns>
        public string FarmMapInterection(FarmTileType type)
        {
            switch (type)
            {
                case FarmTileType.Ground:
                    if (inventory.GetHodingCurrentItem() is FarmingTool) return "땅을 경작합니다.";
                    return "땅을 한번 만져봅니다";
                case FarmTileType.Field:
                    if (inventory.GetHodingCurrentItem() is Seed) return "씨앗을 심습니다.";
                    return "밭을 바라봅니다";
                case FarmTileType.Crop:
                    return "농작물을 수확합니다";
                case FarmTileType.Seed:
                    if (inventory.GetHodingCurrentItem() is GrowingTool) return "씨앗에 물을 줍니다.";
                        return "씨앗을 확인합니다";
                case FarmTileType.Water:
                    if (inventory.GetHodingCurrentItem() is GrowingTool) return "강물에서 물을 채웁니다.";
                    return "물의 흐름을 관찰합니다.";
                default:
                    return "";
            }
        }

        public bool MakingField()
        {
            // 만약 내가 경작 도구를 가지고 있다?
            if (inventory.GetHodingCurrentItem() is FarmingTool)
            {
                return true;
            }
            // 아니다 false
            return false;
        }

        public bool PlatingSeed(out Item seed)
        {
            // 만약 내가 씨앗을 들고 있다면
            // return true 하고 item에 seed 넣기
            if (inventory.GetHodingCurrentItem() is Seed )
            {
                seed = inventory.GetHodingCurrentItem();
                inventory.ConsumptionItem();
                return true;
            }

            // 아니다 return false 하고 item에 null 넣기
            seed = null;
            return false;
        }

        public Item WateringSeed(Seed seed)
        {
            // 만약 내가 물뿌리개를 들고 있다면
            if (inventory.GetHodingCurrentItem() is GrowingTool)
            {
                // seed에 물뿌리개의 효과만큼 성장시키기
                GrowingTool tool = (GrowingTool)inventory.GetHodingCurrentItem();
                tool.Grow(seed);
                // 만약 seed가 성장 수치를 다 채웠으면
                if (seed.isGrew)
                {
                    // seed의 parent인 crop을 리턴
                    return seed.Parent;
                }
            }
            // 아니다 그냥 seed를 반환 
            return seed;
        }

        public void harvestingCrop(Crop crop)
        {
            // 인벤토리에 넣을 때 비어 있는지 확인
            // 해당 농작물을 수확하고 인벤토리에 넣는다
            inventory.InsertItem(crop);

        }

        public void FillWater()
        {
            inventory.Charge();
        }

        private string ShopInterection(ShopTileType type)
        {
            switch (type)
            {
                case ShopTileType.InterectionPlace:
                    return "상인과 대화를 합니다.";
                default:
                    return "";
            }
        }
    }
}
