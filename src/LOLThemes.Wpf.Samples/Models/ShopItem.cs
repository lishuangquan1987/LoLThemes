namespace LOLThemes.Wpf.Samples.Models
{
    /// <summary>
    /// 商店物品数据模型
    /// </summary>
    public class ShopItem
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int GoldCost { get; set; }
        public int RPCost { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsOwned { get; set; }
        public bool IsOnSale { get; set; }
    }
}

