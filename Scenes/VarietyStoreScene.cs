using GeometryFarm.Enums;
using GeometryFarm.Items;
using GeometryFarm.Util;
using System.Text;

namespace GeometryFarm.Scenes
{
    public class VarietyStoreScene : Scene
    {
        private int[,] map;
        private ConsoleKey input;
        private bool usingStore;

        private Item[] itemList;

        // 테스트 용 sb
        private StringBuilder sb;

        public VarietyStoreScene(Game game) : base(game)
        {
            usingStore = false;
            sb = new StringBuilder();
            map = new int[7, 15]
            {
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    { 1, 0, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 3, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            };

            itemList = new Item[2];
            itemList[0] = CropFactory.Instantiate("네모");
            itemList[1] = SeedFactory.Instantiate("네모");
        }

        public override void Enter()
        {
            sb.Clear();
        }

        public override void Exit()
        {
            Console.Clear();
        }

        public override void Input()
        {
            input = Console.ReadKey().Key;
        }

        public override void Render()
        {
            Console.Clear();
            if (usingStore)
            {
                PrintShop();
            }
            else
            {
                PrintMap();
                PrintPlayer();
            }
            Console.SetCursorPosition(0, 20);
            Console.WriteLine(sb.ToString());

        }

        private void PrintMap()
        {
            Console.CursorVisible = false;
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    switch ((ShopTileType)map[y, x])
                    {
                        case ShopTileType.Ground:
                            Console.Write($" ");
                            break;
                        case ShopTileType.Block:
                            Console.Write($"#");
                            //PrintFence(x, y);
                            break;
                        case ShopTileType.Shopkeeper:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"K");
                            Console.ResetColor();
                            break;
                        case ShopTileType.InterectionPlace:
                            Console.Write($" ");
                            break;
                        case ShopTileType.Portal:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write($"@");
                            Console.ResetColor();
                            break;
                    }
                }
                Console.WriteLine();
            }

        }

        private void PrintPlayer()
        {
            Console.SetCursorPosition(game.Player.GetPos().x, game.Player.GetPos().y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("P");
            Console.ResetColor();
        }

        private void PrintShop()
        {
            Console.WriteLine("=====================잡화점======================");
            for (int index = 0; index < itemList.Length; index++)
            {
                Console.WriteLine(" ┌─────────────────────────────────────────────┐");
                Console.WriteLine($"  {index + 1,2}.\t이름 : {itemList[index].name}");
                Console.WriteLine($"    \t가격 : {itemList[index].price}G");
                Console.WriteLine($"    \t설명 : {itemList[index].description}");
                Console.WriteLine(" └─────────────────────────────────────────────┘");
            }
            Console.CursorVisible = true;

            
            game.Player.ShowItem(Console.GetCursorPosition().Top);
            Console.WriteLine($"\n\n\n소지금 : {game.Player.gold}G");
            Console.WriteLine("=========물건 구매(각 번호) || 나가기 (0)========");
            Console.Write("선택한 번호 : ");

        }

        private void PrintFence(int x, int y)
        {
            if (x == 0)
            {
                if (y == 0) Console.Write("┌");
                else if (y == map.GetLength(0) - 1) Console.Write("└");
                else Console.Write("│");
            }
            if (x == map.GetLength(1) - 1)
            {
                if (y == 0) Console.Write("┐");
                else if (y == map.GetLength(0) - 1) Console.Write("┘");
                else Console.Write(" │");
            }
            else if (y == 0 || y == map.GetLength(0) - 1)
            {
                Console.Write("─");
            }
        }


        public override void Update()
        {
            // 해당 키를 입력받은 곳으로 이동하기 

            if (usingStore)
            {
                InputStore();
            }
            else
            {
                InputMove();
            }
        }

        private void InputMove()
        {
            switch (input)
            {
                case ConsoleKey.UpArrow:
                    Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    Move(0, 1);
                    break;
                case ConsoleKey.RightArrow:
                    Move(1, 0);
                    break;
                case ConsoleKey.LeftArrow:
                    Move(-1, 0);
                    break;
                case ConsoleKey.E:
                    sb.Clear();
                    usingStore = true;
                    game.Player.ChangeCurrentUsing(1);
                    break;
            }
            CheckPlayerPos();
        }

        private void InputStore()
        {
            sb.Clear();
            switch (input)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    usingStore = false;
                    sb.Append($"상인과의 대화를 마무리 합니다.");
                    break;
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    if (!game.Player.isInventoryFull() && game.Player.gold >= itemList[0].price)
                    {
                        game.Player.BuyItem(itemList[0]);
                        sb.Append($"{itemList[0].name}을 구매했습니다!");
                    }
                    else
                    {
                        sb.Append("잔액 혹은 인벤토리 자리가 부족합니다.");
                    }
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    if (!game.Player.isInventoryFull() && game.Player.gold >= itemList[1].price)
                    {
                        game.Player.BuyItem(itemList[1]);
                        sb.Append($"{itemList[1].name}을 구매했습니다!");
                    }
                    else
                    {
                        sb.Append("잔액 혹은 인벤토리 자리가 부족합니다.");
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if(game.Player.currentUsing - 1 <= 1)
                    {
                        game.Player.ChangeCurrentUsing(1);
                    }
                    else
                    {
                        game.Player.ChangeCurrentUsing(game.Player.currentUsing - 1);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (game.Player.currentUsing + 1 >= 6)
                    {
                        game.Player.ChangeCurrentUsing(6);
                    }
                    else
                    {
                        game.Player.ChangeCurrentUsing(game.Player.currentUsing + 1);
                    }
                    break;
                case ConsoleKey.E:
                    sb.Clear();
                    if (game.Player.SellItem())
                    {
                        sb.Append("아이템을 팔았습니다!");
                    }
                    else
                    {
                        sb.Append("팔 아이템이 없습니다.");
                    }
                    break;
            }
        }

        private void Move(int x, int y)
        {
            Pos playerPos = game.Player.GetPos();

            if ((ShopTileType)map[playerPos.y + y, playerPos.x + x] != ShopTileType.Shopkeeper &&
                (ShopTileType)map[playerPos.y + y, playerPos.x + x] != ShopTileType.Block)
            {
                game.Player.SetPos(playerPos.x + x, playerPos.y + y);
            }
        }

        private void CheckPlayerPos()
        {
            if ((ShopTileType)map[game.Player.GetPos().y, game.Player.GetPos().x] == ShopTileType.Portal)
            {
                game.ChangeScene(SceneType.Farm);
                game.Player.SetPos(4, 1);
            }
        }
    }
}
