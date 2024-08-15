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

        private bool isUsingInventory;

        private bool isChanging;

        private int startX = 70;

        public Inventory()
        {
            MAX = new Pos(6, 6);
            items = new (Item, int)[ MAX.y , MAX.x ];
            itemPos = new Pos(0, 0);

            isUsingInventory = false;
            inventoryIndex = 0;
            currentUsing = new Pos(5, 0);
            isChanging = false;

            InsertItem(GrowingToolFactory.Instantiate(ToolRankType.Normal));
            InsertItem(GrowingToolFactory.Instantiate(ToolRankType.Copper));
            InsertItem(GrowingToolFactory.Instantiate(ToolRankType.Steel));
            InsertItem(GrowingToolFactory.Instantiate(ToolRankType.Golden));
        }

        public void InsertItem(Item item, int count = 1)
        {
            if (!isInventoryFull())
            {
                Pos pos = CheckEmptyIndex();
                items[pos.y ,pos.x] = (item, count);
            }
        }

        public void RemoveItem(Pos current)
        {
            items[current.y, current.x].Item1 = null;
            items[current.y, current.x].Item2 = 0;
        }

        public void ExchangeItem(Pos current, Pos next)
        {
            (Item, int) temp = items[current.y ,current.x];
            items[current.y ,current.x] = items[next.y ,next.x];
            items[next.y ,next.x] = temp;

            isChanging = false;
        }

        public void ShowItemInfo(Pos pos)
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
        }

        public void ShowInventory()
        {
            Console.SetCursorPosition(startX , 0);
            Console.Write("=====================인벤토리======================");
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
                    if(currentUsing.Equals(itemPos) && isChanging)
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
                    Console.Write("──────────────∧ 퀵 슬롯 || 인벤토리 ∨──────────────"); 
                }
            }

            Console.WriteLine();
            ShowItemInfo(currentUsing);
        }

        // 아이템 박스를 미리 그리자
        public void ShowQuickSlot(int y = 15)
        {
            for (int slot = 0; slot < 6; slot++)
            {
                if (slot + 1 == currentUsing.x)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
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
        public void ChangeCurrentUsing(int x, int y = 0)
        {
            if( 0 <= currentUsing.x + x && currentUsing.x + x < items.GetLength(1) &&
                0 <= currentUsing.y + y && currentUsing.y + y < items.GetLength(0) )
            {
                this.currentUsing.SetPos(currentUsing.x + x, currentUsing.y + y);
            }
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

        public bool Input(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.UpArrow:
                    ChangeCurrentUsing(1, 0);
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
                    break;
                case ConsoleKey.E:
                    break;
                case ConsoleKey.C:
                    // C를 눌렀어
                    if(!isChanging)
                    {
                        // c를 누른 위치를 기억하고 Change 한다고 알려줘
                        itemPos = currentUsing;
                        isChanging = true;
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

            return isUsingInventory;
        }
    }
}
