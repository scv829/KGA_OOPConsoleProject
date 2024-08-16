using GeometryFarm.Enums;
using GeometryFarm.Util;
using GeometryFarm.Items;
using System.Net.Security;
using System.Text;

namespace GeometryFarm.Scenes
{
    public class FarmScene : Scene
    {
        private (int, Item)[,] map;
        private ConsoleKey input;

        // 테스트 용 sb
        private StringBuilder sb;

        public FarmScene(Game game) : base(game)
        {
            sb = new StringBuilder();

            Console.CursorVisible = false;

            Crop crop = CropFactory.Instantiate("원형");
            Seed seed = SeedFactory.Instantiate("원형");

            map = new (int, Item)[20, 45]
            {
                    { (1, null), (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null)  , (1, null) , (1, null) , (1, null) , (1, null), (1, null) },
                    { (1, null), (5, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (6, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null), (0, null), (0, null), (0, null), (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null) , (0, null)  , (0, null) , (0, null) , (0, null) , (0, null), (1, null) },
                    { (1, null), (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null), (1, null), (1, null), (1, null), (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null) , (1, null)  , (1, null) , (1, null) , (1, null) , (1, null), (1, null) },
            };

            // 임시로 위치를 설정
            game.Player.SetPos(2, 2);
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
            game.Player.inventory.ShowQuickSlot();
            if (game.Player.inventory.isUsingInventory)
            {
                game.Player.inventory.ShowInventory();
            }
        }

        private void PrintMap()
        {
            Console.WriteLine("=====================농 장======================");

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    switch ((FarmTileType)map[y, x].Item1)
                    {
                        case FarmTileType.Ground:
                            Console.Write($" ");
                            break;
                        case FarmTileType.Fence:
                            Console.Write($"#");
                            //PrintFence(x, y);
                            break;
                        case FarmTileType.Field:
                            Console.Write($"F");
                            break;
                        case FarmTileType.Crop:
                            Crop crop = map[y, x].Item2 as Crop;
                            Console.Write(crop.Shape);
                            break;
                        case FarmTileType.Seed:
                            Console.Write($"S");
                            break;
                        case FarmTileType.Water:
                            Console.Write("W");
                            break;
                        case FarmTileType.Portal:
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


        public override void Update()
        {
            if (game.Player.inventory.isUsingInventory)
            {
                game.Player.inventory.Input(input);
            }
            else
            {
                InputMove();
            }
        }

        private void InputMove()
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
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    game.Player.inventory.SetCurrentUsing(0);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    game.Player.inventory.SetCurrentUsing(1);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    game.Player.inventory.SetCurrentUsing(2);
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    game.Player.inventory.SetCurrentUsing(3);
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    game.Player.inventory.SetCurrentUsing(4);
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    game.Player.inventory.SetCurrentUsing(5);
                    break;
                case ConsoleKey.I:
                    game.Player.inventory.OpenInventory();
                    break;
                case ConsoleKey.E:
                    sb.Clear();
                    sb.Append(game.Player.Interection((FarmTileType)map[game.Player.GetPos().y, game.Player.GetPos().x].Item1));
                    CheckTile(ref map[game.Player.GetPos().y, game.Player.GetPos().x]);
                    break;
            }

            CheckPlayerPos();
        }

        private void CheckTile(ref (int, Item) tile)
        {
            switch((FarmTileType)tile.Item1)
            {
                // 상호작용한 타일이 땅일 때
                case FarmTileType.Ground:
                    tile.Item1 = game.Player.MakingField() ? (int)FarmTileType.Field : tile.Item1;
                    break;
                case FarmTileType.Field:
                    tile.Item1 = game.Player.PlatingSeed(out tile.Item2) ? (int)FarmTileType.Seed : tile.Item1;
                    break;
                case FarmTileType.Seed:
                    tile.Item2 = game.Player.WateringSeed(tile.Item2 as Seed);
                    tile.Item1 = (tile.Item2 is Crop) ? (int)FarmTileType.Crop : (int)FarmTileType.Seed; 
                    break;
                case FarmTileType.Water:
                    game.Player.FillWater();
                    break;
                case FarmTileType.Crop:
                    if (!game.Player.inventory.isInventoryFull())
                    {
                        game.Player.harvestingCrop(tile.Item2 as Crop);
                        tile.Item2 = null;
                        tile.Item1 = (int)FarmTileType.Field;
                    }
                    break;
            }
        }

        private void Move(int x, int y)
        {
            Pos playerPos = game.Player.GetPos();

            if (map[playerPos.y + y, playerPos.x + x].Item1 != 1 )
            {
                game.Player.SetPos(playerPos.x + x, playerPos.y + y);
            }
        }

        private void CheckPlayerPos()
        {
            if ((FarmTileType)map[game.Player.GetPos().y, game.Player.GetPos().x].Item1 == FarmTileType.Portal)
            {
                game.ChangeScene(SceneType.Town);
                game.Player.SetPos(1, 1);
            }

        }

    }
}
