using GeometryFarm.Enums;
using GeometryFarm.Items;
using GeometryFarm.Util;

namespace GeometryFarm
{
    public class Player
    {

        private Item[] inventory;
        public int gold { get; private set; }
        public int fatigue { get; private set; }
        private string name;
        private int inventoryIndex;
        private Pos playerPos;
        public int currentUsing { get; private set; }


        public Player(string name)
        {
            this.name = name;
            this.inventory = new Item[6];
            this.gold = 3000;
            this.fatigue = 0;
            this.inventoryIndex = 1;
            this.playerPos = new Pos();
            this.currentUsing = 1;

            Crop crop = new Crop("네모", 1000, "네모 농작물");
            Seed seed = new Seed("네모의 씨앗", 50, "네모 농작물의 씨앗");
            seed.SetParent(crop);
            
            inventory[0] = seed;
        }

        public bool isInventoryFull()
        {
            return inventoryIndex >= inventory.Length;
        }

        public void BuyItem(Item item)
        {
            int index = CheckEmpty();
            inventory[index] = item;
            inventoryIndex++;
            gold -= item.price;
        }

        public bool SellItem()
        {
            if( 0 < currentUsing && currentUsing <= inventory.Length )
            {
                if(inventory[currentUsing - 1] != null)
                {
                    gold += inventory[currentUsing - 1].price;
                    inventory[currentUsing - 1] = null;
                    inventoryIndex--;
                    return true;
                }
            }
            return false;
        }

        public void ChangeCurrentUsing(int index)
        {
            this.currentUsing = index;
        }

        public void ShowItem(int y = 15)
        {
            
            for(int slot = 0; slot < inventory.Length; slot++)
            {
                if (slot + 1 == currentUsing)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;  
                }
                Console.SetCursorPosition(0 + slot * 6, y );
                Console.Write(" ┌──┐ ");
                Console.SetCursorPosition(0 + slot * 6, y+1);
                Console.Write($" │{inventory[slot]?.GetType().Name[0],-2}│ ");
                Console.SetCursorPosition(0 + slot * 6, y+2);
                Console.Write(" └──┘ ");
                Console.ResetColor();
            }

     
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
        public void SetPos(int x, int y) { this.playerPos.setPos(x, y); }

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
                    return "땅을 한번 만져봅니다";
                case FarmTileType.Field:
                    return "밭을 바라봅니다";
                case FarmTileType.Crop:
                    return "농작물을 수확합니다";
                case FarmTileType.Seed:
                    return "씨앗에 물을줍니다";
                default:
                    return "";
            }
        }

        public bool MakingField()
        {
            // Todo
            // 만약 내가 호미를 가지고 있다?
            // return true;
            // 아니다 false

            return false;
        }

        public bool PlatingSeed(out Item seed)
        {
            // 만약 내가 씨앗을 들고 있다면
            // return true 하고 item에 seed 넣기
            if ( inventory[currentUsing - 1] is Seed )
            {
                inventoryIndex--;
                seed = inventory[currentUsing - 1];
                inventory[currentUsing - 1] = null;
                return true;
            }

            // 아니다 return false 하고 item에 null 넣기
            seed = null;
            return false;
        }

        public Item WateringSeed(Seed seed)
        {
            // Todo
            // 만약 내가 물뿌리개를 들고 있다면
            // seed에 물뿌리개의 효과만큼 성장시키기
            // 그리고 seed를 반환 
            // 만약 seed가 성장 수치를 다 채웠으면
            // seed의 parent인 crop을 리턴
            // 아니다 그냥 return False로

            return seed.GetParent(); 
        }

        public void harvestingCrop(Crop crop)
        {
            // 해당 농작물을 수확하고 인벤토리에 넣는다
            // 인벤토리에 넣을 때 비어 있는지 확인
            int index = CheckEmpty();

            inventory[index] = crop;
            inventoryIndex++;
        }

        private int CheckEmpty()
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    return i;
                }
            }
            return -1;
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
