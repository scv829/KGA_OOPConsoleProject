using GeometryFarm.Enums;
using GeometryFarm.Items;
using GeometryFarm.Util;
using System;
using System.Net.Security;
using System.Numerics;
using System.Text;

namespace GeometryFarm.Scenes
{
    public class VarietyStoreScene : Scene
    {
        private int[,] map;
        private ConsoleKey input;
        private bool usingStore;

        private Item[] itemList;
        private int sellCount;

        private bool selectSellItem;
        private bool isSell;

        // 테스트 용 sb
        private StringBuilder sb;

        public VarietyStoreScene(Game game) : base(game)
        {
            selectSellItem = false;
            usingStore = false;
            isSell = false;
            sb = new StringBuilder();
            sellCount = -1;
            map = new int[7, 10]
            {
                    { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
                    { 1, 0, 3, 3, 3, 0, 0, 1, 0, 0 },
                    { 1, 0, 3, 2, 3, 0, 0, 1, 0, 0 },
                    { 1, 0, 3, 3, 3, 0, 0, 1, 1, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
                    { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
                    { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
            };

            itemList = new Item[3];
            itemList[0] = SeedFactory.Instantiate("원형");
            itemList[1] = SeedFactory.Instantiate("세모");
            itemList[2] = SeedFactory.Instantiate("네모");
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
            input = Console.ReadKey(true).Key;
        }

        public override void Render()
        {
            Console.Clear();
            if (usingStore)
            {
                if (game.Player.inventory.isUsingInventory)
                {
                    game.Player.inventory.ShowInventory();
                }
                else if (selectSellItem == true) { PrintSell(); }
                else
                {
                    PrintShop();
                }
            }
            else
            {
                PrintMap();
                PrintPlayer();
                if (game.Player.inventory.isUsingInventory)
                {
                    game.Player.inventory.ShowInventory();
                }
  
            }
            Console.SetCursorPosition(0, 25);
            Console.WriteLine(sb.ToString());

        }

        private void PrintMap()
        {
            Console.WriteLine("=====================잡화점======================");
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
            Console.SetCursorPosition(game.Player.GetPos().x, game.Player.GetPos().y + 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("P");
            Console.ResetColor();
        }

        private void PrintShop()
        {
            Console.WriteLine("=====================잡화점[구매]======================");
            for (int index = 0; index < itemList.Length; index++)
            {
                Console.WriteLine(" ┌─────────────────────────────────────────────┐");
                Console.WriteLine($"  {index + 1,2}.\t이름 : {itemList[index].name}");
                Console.WriteLine($"    \t가격 : {itemList[index].price}G");
                Console.WriteLine($"    \t설명 : {itemList[index].description}");
                Console.WriteLine(" └─────────────────────────────────────────────┘");
            }
            Console.CursorVisible = true;

            Console.WriteLine($"\n\n\n소지금 : {game.Player.gold}G");
            Console.WriteLine("=========물건 구매(각 번호) || 판매 전환 (E) || 나가기 (0)========");
            Console.Write("선택한 번호 : ");
        }

        private void PrintSell()
        {
            

            Item item = game.Player.inventory.GetHodingCurrentItem();
            int count = game.Player.inventory.GetHodingCurrentItemCount();
            Console.WriteLine("=====================잡화점[판매]======================");
            Console.WriteLine(" ┌─────────────────────────────────────────────┐");
            Console.WriteLine($"    \t이름 : {item.name}");
            Console.WriteLine($"    \t가격 : {item.price}G");
            Console.WriteLine($"    \t설명 : {item.description}");
            Console.WriteLine($"    \t수량 : {count}");
            Console.WriteLine(" └─────────────────────────────────────────────┘");

            Console.WriteLine($"=========물건 판매(E) || 구매 전환 (0)========");
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
                if (game.Player.inventory.isUsingInventory) 
                {
                    selectSellItem = game.Player.inventory.SellInput(input); // 아이템 팔기 선택
                }
                else if(selectSellItem)
                {
                    SellInput();
                }
                else InputStore();
            }
            else
            {
                if (game.Player.inventory.isUsingInventory)
                {
                    game.Player.inventory.Input(input);
                }
                else InputMove();
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
                case ConsoleKey.I:
                    game.Player.inventory.OpenInventory();
                    break;
                case ConsoleKey.E:
                    sb.Clear();
                    usingStore = isAroundKeeper();
                    break;
            }
            CheckPlayerPos();
        }

        private void InputStore()
        {
            
            sb.Clear();
            {
                switch (input)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        usingStore = false;
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        if (!game.Player.inventory.isInventoryFull() && game.Player.gold >= itemList[0].price)
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
                        if (!game.Player.inventory.isInventoryFull() && game.Player.gold >= itemList[1].price)
                        {
                            game.Player.BuyItem(itemList[1]);
                            sb.Append($"{itemList[1].name}을 구매했습니다!");
                        }
                        else
                        {
                            sb.Append("잔액 혹은 인벤토리 자리가 부족합니다.");
                        }
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        if (!game.Player.inventory.isInventoryFull() && game.Player.gold >= itemList[1].price)
                        {
                            game.Player.BuyItem(itemList[1]);
                            sb.Append($"{itemList[1].name}을 구매했습니다!");
                        }
                        else
                        {
                            sb.Append("잔액 혹은 인벤토리 자리가 부족합니다.");
                        }
                        break;
                    case ConsoleKey.E:
                        sb.Clear();
                            // 인벤토리 열기
                            game.Player.inventory.OpenInventory();
                            game.Player.inventory.SetMessage("E : 아이템 판매");
                        break;
                }
            }
        }

        private void SellInput()
        {
            switch (input)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    selectSellItem = false;
                    break;
                case ConsoleKey.E:
                    sb.Clear();
                    Console.WriteLine($"판매할 수량을 적어주세요[1 ~ {game.Player.inventory.GetHodingCurrentItemCount()}] : ");

                    int.TryParse(Console.ReadLine(), out sellCount);
                    if(game.Player.SellItem(sellCount))
                    {
                        sb.Append("아이템을 판매했습니다!");
                    }
                    else
                    {
                        sb.Append("수량 혹은 아이템이 없습니다.");
                    }
                    selectSellItem = false;
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
                game.ChangeScene(SceneType.Town);
                game.Player.SetPos(5, 12);
            }

        }

        private bool isAroundKeeper()
        {
            if ((ShopTileType)map[game.Player.GetPos().y, game.Player.GetPos().x] == ShopTileType.InterectionPlace)
            {
                return true;
            }
            return false;
        }
    }
}
