using System.Collections.Generic;

namespace LOLThemes.Wpf.Samples.Models
{
    /// <summary>
    /// 玩家数据模型
    /// </summary>
    public class PlayerData
    {
        public int Gold { get; set; }
        public int BlueEssence { get; set; }
        public int RiotPoints { get; set; }
        public string Rank { get; set; } = string.Empty;
        public int Level { get; set; }
        public List<string> OwnedChampions { get; set; } = new();
        public int TotalChampions { get; set; }
    }
}

