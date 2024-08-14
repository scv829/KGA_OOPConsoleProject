namespace GeometryFarm.Enums
{
    public enum SceneType
    {
        Main, Farm, VarietyStore, Smithy, SIZE
    }

    public enum ItemType
    {
        Crop, Harvest, Grow
    }

    /// <summary>
    /// 농장맵 타일 타입
    /// Ground = 땅
    /// Fence = 울타리
    /// Field = 밭
    /// Crop = 농작물
    /// Seed = 씨앗
    /// Portal = 맵 이동
    /// </summary>
    public enum FarmTileType
    {
        Ground, Fence, Field, Seed, Crop, Portal
    }

    public enum ShopTileType
    {
        Ground, Block, Shopkeeper, InterectionPlace, Portal = 5
    }
}
