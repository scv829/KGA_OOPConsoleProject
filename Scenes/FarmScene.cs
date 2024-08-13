using GeometryFarm.Enums;
using GeometryFarm.Util;
using System.Net.Security;
using System.Text;

namespace GeometryFarm.Scenes
{
    public class FarmScene : Scene
    {
        private int[,] map;
        private ConsoleKey input;

        // 테스트 용 sb
        private StringBuilder sb;

        public FarmScene(Game game) : base(game)
        {
            sb = new StringBuilder();

            Console.CursorVisible = false;
            map = new int[6, 6]
                {
                    {1, 1, 1, 1, 1 ,1 },
                    {5, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 1 },
                    {1, 2, 0, 0, 4, 1 },
                    {1, 0, 0, 3, 0, 1 },
                    {1, 1, 1, 1, 1, 1 },
                };

            // 임시로 위치를 설정
            game.Player.SetPos(2, 2);
        }

        public override void Enter()
        {
            
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
                    switch ((MapTileType)map[y, x])
                    {
                        case MapTileType.Ground:
                            Console.Write($" ");
                            break;
                        case MapTileType.Fence:
                            Console.Write($"#");
                            //PrintFence(x, y);
                            break;
                        case MapTileType.Field:
                            Console.Write($"F");
                            break;
                        case MapTileType.Crop:
                            Console.Write($"C");
                            break;
                        case MapTileType.Seed:
                            Console.Write($"S");
                            break;
                        case MapTileType.Portal:
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
                    sb.Append(game.Player.Interection((MapTileType)map[game.Player.GetPos().y, game.Player.GetPos().x]));
                    break;
            }

            CheckPlayerPos();
        }

        private void Move(int x, int y)
        {
            Pos playerPos = game.Player.GetPos();

            if (map[playerPos.y + y, playerPos.x + x] != 1)
            {
                game.Player.SetPos(playerPos.x + x, playerPos.y + y);
            }
        }

        private void CheckPlayerPos()
        {
            if ((MapTileType)map[game.Player.GetPos().y, game.Player.GetPos().x] == MapTileType.Portal)
            {
                game.ChangeScene(SceneType.Main);
            }

        }

    }
}
