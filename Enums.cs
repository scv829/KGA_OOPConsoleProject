namespace GeometryFarm.Enums
{
    public enum SceneType
    {
        Main, Farm, Town, VarietyStore, Smithy, SIZE
    }

    public enum ItemType
    {
        Crop, Seed, Tool
    }

    public enum ToolRankType
    {
        Normal, Copper, Steel, Golden, MAX
    }

    /// <summary>
    /// 농장맵 타일 타입
    /// Ground = 땅
    /// Fence = 울타리
    /// Field = 밭
    /// Crop = 농작물
    /// Seed = 씨앗
    /// Water = 강물
    /// Portal = 맵 이동
    /// </summary>
    public enum FarmTileType
    {
        Ground, Fence, Field, Seed, Crop, Water, Portal
    }

    public enum ShopTileType
    {
        Ground, Block, Shopkeeper, InterectionPlace, Portal = 5
    }

    public enum TownTileType
    {
        Ground, Block, Portal = 5
    }

}
