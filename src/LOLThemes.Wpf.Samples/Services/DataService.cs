using System.Collections.Generic;
using System.Linq;
using LOLThemes.Wpf.Samples.Models;

namespace LOLThemes.Wpf.Samples.Services
{
    /// <summary>
    /// 数据服务，用于生成和管理模拟数据
    /// </summary>
    public static class DataService
    {
        /// <summary>
        /// 获取所有英雄列表
        /// </summary>
        public static List<Champion> GetChampions()
        {
            return new List<Champion>
            {
                new Champion
                {
                    Name = "亚索",
                    Title = "疾风剑豪",
                    Description = "亚索是一个近战刺客，擅长使用风之力量进行快速移动和攻击。",
                    Roles = new List<string> { "刺客", "战士" },
                    Tags = new List<string> { "中单", "上单" },
                    Difficulty = 8,
                    Attack = 8,
                    Defense = 4,
                    Magic = 4,
                    Cost = 4800,
                    IsOwned = true,
                    Skills = new List<Skill>
                    {
                        new Skill { Key = "Q", Name = "斩钢闪", Description = "向前出剑，造成物理伤害。" },
                        new Skill { Key = "W", Name = "风之障壁", Description = "形成一个气流之墙，可以阻挡敌方的所有飞行道具。" },
                        new Skill { Key = "E", Name = "踏前斩", Description = "向目标敌人突进，造成魔法伤害。" },
                        new Skill { Key = "R", Name = "狂风绝息斩", Description = "闪烁到一个被击飞的敌方英雄身边，造成物理伤害。" }
                    }
                },
                new Champion
                {
                    Name = "劫",
                    Title = "影流之主",
                    Description = "劫是一名以能量为武器的刺客，擅长使用影分身进行突袭。",
                    Roles = new List<string> { "刺客" },
                    Tags = new List<string> { "中单" },
                    Difficulty = 9,
                    Attack = 9,
                    Defense = 3,
                    Magic = 1,
                    Cost = 4800,
                    IsOwned = true
                },
                new Champion
                {
                    Name = "阿卡丽",
                    Title = "离群之刺",
                    Description = "阿卡丽是一名灵活的刺客，擅长使用烟雾和双镰进行暗杀。",
                    Roles = new List<string> { "刺客" },
                    Tags = new List<string> { "中单", "上单" },
                    Difficulty = 7,
                    Attack = 7,
                    Defense = 4,
                    Magic = 5,
                    Cost = 4800,
                    IsOwned = false
                },
                new Champion
                {
                    Name = "卡特琳娜",
                    Title = "不祥之刃",
                    Description = "卡特琳娜是一名高机动性的刺客，擅长使用匕首进行连击。",
                    Roles = new List<string> { "刺客", "法师" },
                    Tags = new List<string> { "中单" },
                    Difficulty = 8,
                    Attack = 8,
                    Defense = 3,
                    Magic = 3,
                    Cost = 4800,
                    IsOwned = true
                },
                new Champion
                {
                    Name = "辛德拉",
                    Title = "暗黑元首",
                    Description = "辛德拉是一名强大的法师，擅长使用暗黑法球进行远程攻击。",
                    Roles = new List<string> { "法师" },
                    Tags = new List<string> { "中单" },
                    Difficulty = 6,
                    Attack = 2,
                    Defense = 3,
                    Magic = 9,
                    Cost = 4800,
                    IsOwned = true
                },
                new Champion
                {
                    Name = "妖姬",
                    Title = "诡术妖姬",
                    Description = "乐芙兰是一名灵活的法师刺客，擅长使用幻象和位移技能。",
                    Roles = new List<string> { "刺客", "法师" },
                    Tags = new List<string> { "中单" },
                    Difficulty = 9,
                    Attack = 6,
                    Defense = 4,
                    Magic = 8,
                    Cost = 4800,
                    IsOwned = false
                },
                new Champion
                {
                    Name = "发条",
                    Title = "发条魔灵",
                    Description = "奥莉安娜是一名控制型法师，擅长使用机械球进行远程攻击和控制。",
                    Roles = new List<string> { "法师" },
                    Tags = new List<string> { "中单" },
                    Difficulty = 7,
                    Attack = 4,
                    Defense = 3,
                    Magic = 8,
                    Cost = 4800,
                    IsOwned = true
                },
                new Champion
                {
                    Name = "瑞兹",
                    Title = "符文法师",
                    Description = "瑞兹是一名强大的法师，擅长使用符文魔法进行持续输出。",
                    Roles = new List<string> { "法师", "战士" },
                    Tags = new List<string> { "中单" },
                    Difficulty = 7,
                    Attack = 2,
                    Defense = 4,
                    Magic = 8,
                    Cost = 4800,
                    IsOwned = true
                },
                new Champion
                {
                    Name = "卡萨丁",
                    Title = "虚空行者",
                    Description = "卡萨丁是一名灵活的法师刺客，擅长使用虚空魔法进行突袭。",
                    Roles = new List<string> { "刺客", "法师" },
                    Tags = new List<string> { "中单" },
                    Difficulty = 8,
                    Attack = 3,
                    Defense = 5,
                    Magic = 8,
                    Cost = 4800,
                    IsOwned = false
                },
                new Champion
                {
                    Name = "维克托",
                    Title = "机械先驱",
                    Description = "维克托是一名科技型法师，擅长使用机械装置进行战斗。",
                    Roles = new List<string> { "法师" },
                    Tags = new List<string> { "中单" },
                    Difficulty = 7,
                    Attack = 2,
                    Defense = 4,
                    Magic = 9,
                    Cost = 4800,
                    IsOwned = true
                },
                new Champion
                {
                    Name = "佐伊",
                    Title = "暮光星灵",
                    Description = "佐伊是一名灵活的法师，擅长使用星灵魔法进行远程攻击。",
                    Roles = new List<string> { "法师" },
                    Tags = new List<string> { "中单" },
                    Difficulty = 8,
                    Attack = 1,
                    Defense = 4,
                    Magic = 8,
                    Cost = 4800,
                    IsOwned = false
                },
                new Champion
                {
                    Name = "艾克",
                    Title = "时间刺客",
                    Description = "艾克是一名灵活的刺客，擅长使用时间魔法进行战斗。",
                    Roles = new List<string> { "刺客", "法师" },
                    Tags = new List<string> { "中单", "打野" },
                    Difficulty = 8,
                    Attack = 5,
                    Defense = 3,
                    Magic = 7,
                    Cost = 4800,
                    IsOwned = true
                }
            };
        }

        /// <summary>
        /// 获取商店物品列表
        /// </summary>
        public static List<ShopItem> GetShopItems()
        {
            return new List<ShopItem>
            {
                new ShopItem { Name = "无尽之刃", Description = "增加大量攻击力和暴击伤害", Category = "攻击", GoldCost = 3400, RPCost = 0, IsOwned = false },
                new ShopItem { Name = "饮血剑", Description = "增加攻击力和生命偷取", Category = "攻击", GoldCost = 3500, RPCost = 0, IsOwned = false },
                new ShopItem { Name = "三相之力", Description = "全面的属性加成", Category = "攻击", GoldCost = 3333, RPCost = 0, IsOwned = false },
                new ShopItem { Name = "灭世者的死亡之帽", Description = "大幅增加法术强度", Category = "法术", GoldCost = 3600, RPCost = 0, IsOwned = false },
                new ShopItem { Name = "卢登的回声", Description = "增加法术强度和移动速度", Category = "法术", GoldCost = 3200, RPCost = 0, IsOwned = false },
                new ShopItem { Name = "中亚沙漏", Description = "增加法术强度和护甲，主动效果可免疫伤害", Category = "法术", GoldCost = 3000, RPCost = 0, IsOwned = false },
                new ShopItem { Name = "日炎圣盾", Description = "增加生命值和护甲", Category = "防御", GoldCost = 3000, RPCost = 0, IsOwned = false },
                new ShopItem { Name = "兰顿之兆", Description = "增加生命值和护甲，减少攻击速度", Category = "防御", GoldCost = 3000, RPCost = 0, IsOwned = false }
            };
        }

        /// <summary>
        /// 获取玩家数据
        /// </summary>
        public static PlayerData GetPlayerData()
        {
            return new PlayerData
            {
                Gold = 15680,
                BlueEssence = 45680,
                RiotPoints = 2450,
                Rank = "钻石",
                Level = 30,
                OwnedChampions = new List<string> { "亚索", "劫", "卡特琳娜", "辛德拉", "发条", "瑞兹", "维克托", "艾克" },
                TotalChampions = 162
            };
        }

        /// <summary>
        /// 根据位置筛选英雄
        /// </summary>
        public static List<Champion> FilterChampionsByPosition(List<Champion> champions, string position)
        {
            return champions.Where(c => c.Tags.Contains(position)).ToList();
        }

        /// <summary>
        /// 根据类型筛选英雄
        /// </summary>
        public static List<Champion> FilterChampionsByType(List<Champion> champions, List<string> types)
        {
            return champions.Where(c => c.Roles.Any(r => types.Contains(r))).ToList();
        }
    }
}

