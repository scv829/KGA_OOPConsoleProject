using GeometryFarm.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Items
{
    public interface IUpgrade
    {
        /// <summary>
        /// 업그레이드 조건 설정하는 메서드
        /// </summary>
        public void SetIngredient(int gold, List<(Item, int)> items);

        /// <summary>
        /// 업그레이드 조건에 맞는지 여부 확인하는 메소드
        /// </summary>
        /// <param name="playerGold">플레이어의 소지금</param>
        /// <param name="playerInventory">플레이어의 인벤토리</param>
        /// <returns>업그레이드 조건 부합 여부</returns>
        public List<bool> CheckIngredient(int playerGold, Inventory playerInventory);

        /// <summary>
        /// 업그레이드를 시켜주는 메소드
        /// </summary> 
        /// <returns>업그레이드된 아이템 </returns>
        public Item Upgrade(int upgradeGold, Inventory playerInventory);
    }
}
