using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryFarm.Enums;
using GeometryFarm.Items;

namespace GeometryFarm.Util
{
    public class Inventory
    {
        
        private int expansionCount = 1;                 // 현재 인벤토리 증축 횟수
        private const int MaxExpansionCount = 6;        // 최대 증축 횟수

        private Pos MAX;                                // 최대 인벤토리의 크기

        private (Item, int)[,] items;                   // (아이템, 아이템의 갯수)를 가진 아이템 배열

        private Pos itemPos;                            // 변경할 아이템의 좌표

        private int inventoryIndex;                     // 현재 인벤토리가 들어있는 아이템의 수

        public Pos currentUsing { get; private set; }

        public bool isUsingInventory { get; private set; }

        private bool isChanging;

        private StringBuilder sb;

        public Inventory()
        {
            MAX = new Pos(6, 6);
            items = new (Item, int)[ MAX.y , MAX.x ];
            itemPos = new Pos(-1, -1);

            isUsingInventory = false;
            inventoryIndex = 0;
            currentUsing = new Pos(0, 0);
            isChanging = false;
            sb = new StringBuilder();

            InsertItem(GrowingToolFactory.Instantiate(ToolRankType.Normal));
            InsertItem(FarmingToolFactory.Instantiate(ToolRankType.Normal));
        }

        public void InsertItem(Item item, int count = 1)
        {
            if (!isInventoryFull())
            {
                Pos pos = CheckEmptyIndex();
                items[pos.y ,pos.x] = (item, count);
                inventoryIndex++;
            }
        }

        public void RemoveItem(Pos current)
        {
            items[current.y, current.x].Item1 = null;
            items[current.y, current.x].Item2 = 0;
            inventoryIndex--;
        }

        public void ExchangeItem(Pos current, Pos next)
        {
            (Item, int) temp = items[current.y ,current.x];
            items[current.y ,current.x] = items[next.y ,next.x];
            items[next.y ,next.x] = temp;

            itemPos = new Pos(-1, -1);
            isChanging = false;
            SetMessage("C : 아이템 위치 변경 / I : 인벤토리 닫기 / D : 아이템 삭제");
        }

        public void ShowItemInfo(Pos pos, int startX = 70)
        {
            if (items[pos.y, pos.x].Item1 == null)
            {
                Console.SetCursorPosition(startX, Console.GetCursorPosition().Top);
                Console.Write(" ─────────────────────────────────────────────");
                Console.SetCursorPosition(startX, Console.GetCursorPosition().Top + 6);
                Console.Write(" ─────────────────────────────────────────────");
            }
            else
            {
                Console.SetCursorPosition(startX, Console.GetCursorPosition().Top);
                Console.Write(" ─────────────────────────────────────────────");
                Console.SetCursorPosition(startX, Console.GetCursorPosition().Top + 1);
                items[pos.y, pos.x].Item1.PrintImage();
                Console.SetCursorPosition(startX, Console.GetCursorPosition().Top + 1);
                Console.Write($"   \t이름 : {items[pos.y, pos.x].Item1.name}");
                Console.SetCursorPosition(startX, Console.GetCursorPosition().Top + 1);
                Console.Write($"   \t가격 : {items[pos.y, pos.x].Item1.price}G");
                Console.SetCursorPosition(startX, Console.GetCursorPosition().Top + 1);
                Console.Write($"   \t설명 : {items[pos.y, pos.x].Item1.description}");
                Console.SetCursorPosition(startX, Console.GetCursorPosition().Top + 1);
                Console.Write($"   \t수량 : {items[pos.y, pos.x].Item2}");
                Console.SetCursorPosition(startX, Console.GetCursorPosition().Top + 1);
                Console.Write(" ─────────────────────────────────────────────");
            }
            Console.SetCursorPosition(startX, Console.GetCursorPosition().Top + 1);
            Console.Write(sb.ToString());

        }

        public void ShowInventory(int startX = 70)
        {
            Console.SetCursorPosition(startX , 0);
            Console.Write("====================인벤토리====================");
            int xPos = 0, yPos = 0;

            for (int y = 0; y < items.GetLength(0); y++)
            {
                for(int x = 0; x < items.GetLength(1); x++)
                {
                    xPos = startX + x * 8;
                    yPos = y == 0 ? (3 * y) : (3 * y) + 3;
                    if (currentUsing.IsEqual(x,y))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    if(itemPos.IsEqual(x,y))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.SetCursorPosition(xPos, yPos + 1);
                    Console.Write($"{" ┌───┐ "}");
                    Console.SetCursorPosition(xPos, yPos + 2);
                    Console.Write($" │ ");
                    if ((items[y, x].Item1 == null))
                    {
                        Console.Write($"{" ",2}");
                    }
                    else
                    {
                        items[y, x].Item1.PrintImage();
                    }
                    Console.Write($"│ ");
                    Console.SetCursorPosition(xPos, yPos + 3);
                    Console.Write($"{" └───┘ "}");
                    Console.ResetColor();
                }
                if (y == 0) 
                { 
                    Console.SetCursorPosition(startX , 5);
                    Console.Write("────────────∧ 퀵 슬롯 || 인벤토리 ∨───────────"); 
                }
            }

            Console.WriteLine();
            ShowItemInfo(currentUsing);
        }

   
        public void ShowQuickSlot(int y = 25)
        {
            for (int slot = 0; slot < 6; slot++)
            {
                if (slot == currentUsing.x && currentUsing.y == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    if (items[0, slot].Item1 is GrowingTool)
                    {
                        GrowingTool tool = items[0, slot].Item1 as GrowingTool;
                        Console.SetCursorPosition(0 + slot * 8, y + 3);
                        Console.Write($"  {tool.CurrentCapacity} / {tool.Capacity}");
                    }
                }
                Console.SetCursorPosition(0 + slot * 8, y);
                Console.Write($"{" ┌───┐ "}");
                Console.SetCursorPosition(0 + slot * 8, y + 1);
                Console.Write($" │ ");
                if ((items[0,slot].Item1 == null))
                {
                    Console.Write($"{" ",2}");
                }
                else
                {
                    items[0, slot].Item1.PrintImage();
                }
                Console.Write($"│ ");
                Console.SetCursorPosition(0 + slot * 8, y + 2);
                Console.Write($"{" └───┘ "}");
                
                Console.ResetColor();

            }
        }

        /// <summary>
        /// 인벤토리 꽉 찼는지 확인하는 메서드
        /// </summary>
        /// <returns>인벤토리가 꽉 찼는지 여부</returns>
        public bool isInventoryFull()
        {
            return inventoryIndex >= (MAX.x * MAX.y);
        }

        // 현재 인덱스 부분
        public void ChangeCurrentUsing(int x, int y)
        {
            if( 0 <= currentUsing.x + x && currentUsing.x + x < items.GetLength(1) &&
                0 <= currentUsing.y + y && currentUsing.y + y < items.GetLength(0) )
            {
                currentUsing = new Pos(currentUsing.x + x, currentUsing.y + y);
            }
        }

        public void SetCurrentUsing(int index)
        {
            currentUsing = new Pos(index, 0);
        }

        /// <summary>
        /// 인벤토리에서 비어있는 곳을 찾는 메서드
        /// -1은 자리가 없다는 의미
        /// </summary>
        /// <returns>비어있는 인덱스</returns>
        private Pos CheckEmptyIndex()
        {
            for (int y = 0; y < items.GetLength(0); y++)
            {
                for(int x = 0; x < items.GetLength(1); x++)
                {
                    if (items[y, x].Item1 == null)
                    {
                        return new Pos(x, y);
                    }
                }
            }
            return new Pos(-1, -1);
        }

        public void Input(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.UpArrow:
                    ChangeCurrentUsing(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    ChangeCurrentUsing(0, 1);
                    break;
                case ConsoleKey.RightArrow:
                    ChangeCurrentUsing(1, 0);
                    break;
                case ConsoleKey.LeftArrow:
                    ChangeCurrentUsing(-1, 0);
                    break;
                case ConsoleKey.I:
                    isUsingInventory = false;
                    Console.Clear();
                    break;
                case ConsoleKey.C:
                    // C를 눌렀어
                    if(!isChanging)
                    {
                        // C를 누른 위치를 기억하고 Change 한다고 알려줘
                        itemPos = currentUsing;
                        isChanging = true;
                        SetMessage("변경할 위치로 이동해서 C 를 눌러주세요");
                    }
                    // C를 한번 더 눌러
                    else
                    {
                        // 그럼 입력한 위치와 전에 누른 위치를 서로 변경해
                        ExchangeItem(itemPos, currentUsing);
                    }
                    break;
                case ConsoleKey.D:
                    RemoveItem(currentUsing);
                    break;
            }
        }

        public bool SellInput(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.UpArrow:
                    ChangeCurrentUsing(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    ChangeCurrentUsing(0, 1);
                    break;
                case ConsoleKey.RightArrow:
                    ChangeCurrentUsing(1, 0);
                    break;
                case ConsoleKey.LeftArrow:
                    ChangeCurrentUsing(-1, 0);
                    break;
                case ConsoleKey.I:
                    isUsingInventory = false;
                    Console.Clear();
                    break;
                case ConsoleKey.E:
                    isUsingInventory = false;
                    return true;
            }
            return false;
        }

        public void OpenInventory()
        {
            isUsingInventory = true;
            currentUsing.SetPos(0, 0);
            SetMessage("C : 아이템 위치 변경 / I : 인벤토리 닫기 / D : 아이템 삭제");
        }

        public Item GetHodingCurrentItem()
        {
            return items[currentUsing.y, currentUsing.x].Item1;
        }

        public int GetHodingCurrentItemCount()
        {
            return items[currentUsing.y, currentUsing.x].Item2;
        }

        public void ConsumptionItem(int count = 1)
        {
            items[currentUsing.y, currentUsing.x].Item2 -= count;
            if(items[currentUsing.y, currentUsing.x].Item2 == 0)
            {
                RemoveItem(currentUsing);
            }
        }

        public void SetMessage(string msg)
        {
            sb.Clear();
            sb.Append(msg);
        }

        public (Item, int) FindItemByPos(Pos pos)
        {
            return items[pos.y, pos.x];
        }

        /// <summary>
        /// 아이템이 있는지 확인하는 메서드
        /// </summary>
        /// <param name="item">확인할 아이템</param>
        /// <returns>만약 있으면 확인할 아이템의 좌표 반환 없으면 -1,-1 로 반환</returns>
        public Pos ContainItem(Item item)
        {
            for (int y = 0; y < items.GetLength(0); y++)
            {
                for (int x = 10; x < items.GetLength(1); x++)
                {
                    if (items[y, x].Item1 == item) return new Pos(x, y);
                }
            }
            return new Pos(-1, -1);
        }

        /// <summary>
        /// 도구 업그레이드에서 재료의 수를 만족하는지 확인하는 메서드
        /// </summary>
        /// <param name="item">재료(아이템, 수량)</param>
        /// <returns>재료가 충분히 있는지 여부</returns>
        public bool ContainItem((Item, int) item)
        {
            for(int y = 0; y < items.GetLength(0); y++)
            {
                for(int x = 10; x < items.GetLength(1); x++)
                {
                    if (items[y, x].Item1 == item.Item1 && items[y, x].Item2 <= item.Item2) return true;
                }
            }
            return false;
        }

        /// <summary>
        ///  업그레이드 가능한 도구를 찾는 메서드
        /// </summary>
        /// <returns>업그레이드 가능한 도구들</returns>
        public List<Pos> FindItemsByIUpgrade()
        {
            List<Pos> result = new List<Pos>();

            for (int y = 0; y < items.GetLength(0); y++)
            {
                for (int x = 0; x < items.GetLength(1); x++)
                {
                    if (items[y, x].Item1 is IUpgrade) result.Add(new Pos(x, y));
                }
            }
            return result;
        }

        public void UpgradeItemByPos(Pos pos)
        {
            (Item, int) item = FindItemByPos(pos);

            if(item.Item1 is GrowingTool)
            {
                GrowingTool growingTool = (GrowingTool)item.Item1;
                items[pos.y, pos.x] = (GrowingToolFactory.Instantiate(growingTool.ToolRank + 1), item.Item2);
            }
        }

        public void Charge()
        {
            if (GetHodingCurrentItem() is GrowingTool)
            {
                GrowingTool tool = GetHodingCurrentItem() as GrowingTool;
                tool.Charge();
                items[currentUsing.y, currentUsing.x].Item1 = tool;
            }
        }
    }
}
