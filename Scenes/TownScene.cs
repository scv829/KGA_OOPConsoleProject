using GeometryFarm.Enums;
using GeometryFarm.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Scenes
{
    public class TownScene : Scene
    {
        private int[,] map;
        private ConsoleKey input;

        private Pos VarietyStorePos;
        private Pos SmithyPos;
        private Pos FarmPos;

        // 테스트 용 sb
        private StringBuilder sb;

        public TownScene(Game game) : base(game)
        {
            map = new int[15, 15]
            {                      /*채석장*/           /*마*/ /*을*/
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },/*회*/
     /*농장*/   {5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },/*관*/
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                {1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, /* 낚시터 */
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                {1, 0, 0, 0, 5, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 5, 0, 0, 0, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                   /*잡화점*/                   /*대장간*/
            };

            FarmPos = new Pos(0, 1);
            VarietyStorePos = new Pos(4, 12);
            SmithyPos = new Pos(10, 13);

            sb = new StringBuilder();

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
            PrintMap();
            PrintPlayer();
            if(game.Player.inventory.isUsingInventory) game.Player.inventory.ShowInventory();
        }

        private void PrintMap()
        {
            Console.WriteLine("=====================마 을======================");
            Console.CursorVisible = false;
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    switch ((TownTileType)map[y, x])
                    {
                        case TownTileType.Ground:
                            Console.Write($" ");
                            break;
                        case TownTileType.Block:
                            Console.Write($"#");
                            //PrintFence(x, y);
                            break;
                        case TownTileType.Portal:
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

        public override void Update()
        {
            if (game.Player.inventory.isUsingInventory)
            {
                game.Player.inventory.Input(input);
            }
            else InputMove();
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
            }
            CheckPlayerPos();
        }

        private void Move(int x, int y)
        {
            Pos playerPos = game.Player.GetPos();

            if ((TownTileType)map[playerPos.y + y, playerPos.x + x] != TownTileType.Block)
            {
                game.Player.SetPos(playerPos.x + x, playerPos.y + y);
            }
        }

        private void CheckPlayerPos()
        {
            if ((TownTileType)map[game.Player.GetPos().y, game.Player.GetPos().x] == TownTileType.Portal)
            {
                if(game.Player.GetPos().Equals(FarmPos))
                {
                    game.ChangeScene(SceneType.Farm);
                    game.Player.SetPos(43, 1);
                }
                else if(game.Player.GetPos().Equals(VarietyStorePos))
                {
                    game.ChangeScene(SceneType.VarietyStore);
                    game.Player.SetPos(9, 4);
                }
                else if (game.Player.GetPos().Equals(SmithyPos))
                {
                    game.ChangeScene(SceneType.Smithy);
                    game.Player.SetPos(1, 5);
                }
            }
        }

    }
}
