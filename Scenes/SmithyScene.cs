using GeometryFarm.Enums;
using GeometryFarm.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Scenes
{
    public class SmithyScene : Scene
    {
        int[,] map;
        private ConsoleKey input;

        // 테스트 용 sb
        private StringBuilder sb;

        public SmithyScene(Game game) : base(game)
        {
            sb = new StringBuilder();
            map = new int[7, 10]
                {
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    { 1, 3, 3, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 2, 3, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 3, 3, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    { 1, 1, 1, 1, 1, 1, 1, 5, 1, 1 },
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
            input = Console.ReadKey().Key;
        }

        public override void Render()
        {
            PrintMap();
            PrintPlayer();
        }

        private void PrintMap()
        {
            Console.SetCursorPosition(0, 0);
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

            Console.SetCursorPosition(0, 20);
            Console.WriteLine(sb.ToString());

        }

        private void PrintPlayer()
        {
            Console.SetCursorPosition(game.Player.GetPos().x, game.Player.GetPos().y);
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


        public override void Update()
        {
            // 해당 키를 입력받은 곳으로 이동하기 
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
                game.ChangeScene(SceneType.Farm);
                game.Player.SetPos(4, 1);
            }
        }
    }
}
