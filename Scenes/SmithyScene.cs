using GeometryFarm.Enums;
using GeometryFarm.Items;
using GeometryFarm.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Scenes
{
    public class SmithyScene : Scene
    {
        int[,] map;
        private ConsoleKey input;

        private bool usingSmithy;
        private bool usingUpgrade;

        private Pos selectTool;
        private List<Pos> itemList;
        private bool isUpgradable;


        // 테스트 용 sb
        private StringBuilder sb;

        public SmithyScene(Game game) : base(game)
        {
            sb = new StringBuilder();

            usingSmithy = false;
            usingUpgrade = false;
            isUpgradable = false;
            selectTool = new Pos(-1, -1);
            itemList = new List<Pos>();

            map = new int[7, 10]
                {
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    { 1, 3, 3, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 2, 3, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 3, 3, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
                    { 5, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                };
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
            if(!usingSmithy) input = Console.ReadKey().Key;
        }

        public override void Render()
        {
            Console.Clear();
            if (usingUpgrade)
            {
                PrintUpgrade();
            }
            else if(usingSmithy)
            {
                PrintSmithy();
            }
            else
            {
                PrintMap();
                PrintPlayer();
                if (game.Player.inventory.isUsingInventory) game.Player.inventory.ShowInventory();
            }
        }

        #region 맵 내에서 이동[Render]
        private void PrintMap()
        {
            Console.WriteLine("=====================대장간======================");
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

            Console.SetCursorPosition(0, 25);
            Console.WriteLine(sb.ToString());

        }

        private void PrintPlayer()
        {
            Console.SetCursorPosition(game.Player.GetPos().x, game.Player.GetPos().y + 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("P");
            Console.ResetColor();
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
        #endregion

        #region 대장간 이용 [Render]
        private void PrintSmithy()
        {
            itemList = game.Player.inventory.FindItemsByIUpgrade();

            Console.WriteLine("=====================대장간======================");
            for (int index = 0; index < itemList.Count; index++)
            {
                Item item = game.Player.inventory.FindItemByPos(itemList[index]).Item1;
                Console.WriteLine(" ┌─────────────────────────────────────────────┐");
                item.PrintImage();
                Console.WriteLine($"  {index + 1,2}.\t이름 : {item.name}");
                Console.WriteLine($"    \t가격 : {item.price}G");
                Console.WriteLine($"    \t설명 : {item.description}");
                Console.WriteLine(" └─────────────────────────────────────────────┘");
            }

            Console.WriteLine($"=========장비 선택(1~{itemList.Count}) || 나가기 (0)========");
            Console.Write("선택한 번호 : ");
        }
        #endregion

        #region 업그레이드 이용 [Render]
        private void PrintUpgrade()
        {
            Item item = game.Player.inventory.FindItemByPos(selectTool).Item1;


            Console.WriteLine("===================== 대장간[업그레이드] ======================");

            if (item is GrowingTool)
            {
                GrowingTool tool = (GrowingTool)item;

                PrintSpec(tool);

                Console.WriteLine("\t\t\t↓↓");
                Console.WriteLine("\t\t\t↓↓");
                Console.WriteLine("\t\t\t↓↓");

                PrintSpec(GrowingToolFactory.Instantiate(tool.ToolRank + 1));

                if(tool.hasNext() && tool.CheckIngredient(game.Player.gold, game.Player.inventory).Contains(false))
                {
                    isUpgradable = false;
                }
                else
                {
                    isUpgradable = true;
                }

            }

            if(isUpgradable)
            {
                Console.WriteLine("=============    업그레이드 하기(1)     || 뒤로 가기(0)============");
            }
            else
            {
                Console.WriteLine("===========업그레이드 불가[재료 불충분] || 뒤로 가기(0)============");
            }
            Console.Write("선택 : ");

        }

        private void PrintSpec(GrowingTool tool)
        {
            Console.WriteLine(" ┌─────────────────────────────────────────────┐");
            if (tool == null) { Console.WriteLine("\n\n\t\t\t없음\n\n"); }
            else
            {
                Console.WriteLine($"    \t이름 : {tool.name}");
                Console.Write("  "); tool.PrintImage();
                Console.WriteLine($"    \t효과 : {tool.Effect}");
                Console.WriteLine($"    \t설명 : {tool.description}");
            }
            Console.WriteLine(" └─────────────────────────────────────────────┘");
        }

        #endregion

        public override void Update()
        {
            if(usingUpgrade)        // 업그레이드 이용
            {
                InputUpgradeTool();
            }
            else if(usingSmithy)    // 대장간 이용
            {
                InputSelectTool();
            }
            else                    // 대장간 맵 이용
            {
                if (game.Player.inventory.isUsingInventory) game.Player.inventory.Input(input);
                else InputMove();
            }
        }

        #region 맵 내에서 이동[Input]
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
                    usingSmithy = isAroundKeeper();
                    sb.Append(game.Player.Interection((ShopTileType)map[game.Player.GetPos().y, game.Player.GetPos().x]));
                    break;
            }

            CheckPlayerPos();
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
                game.Player.SetPos(9, 13);
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
        #endregion

        #region 대장간 이용[Input]
        private void InputSelectTool()
        { 
            if(int.TryParse(Console.ReadLine(), out int N))
            {
                if (N == 0)
                {
                    usingSmithy = false;
                }
                else if ( 0 < N && N <= itemList.Count) 
                {
                    selectTool = itemList[N-1];
                    usingUpgrade = true;
                    usingSmithy = false;
                }
            }

        }
        #endregion

        #region 업그레이드 이용[Input]
        private void InputUpgradeTool()
        {
            switch (input)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    usingUpgrade = false;
                    usingSmithy = true;
                    break;
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    // Upgrade 로직
                    if (isUpgradable)
                    {
                        game.Player.inventory.UpgradeItemByPos(selectTool);
                    }
                    break;
            }

        }
        #endregion


    }
}
