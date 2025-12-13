using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// 资源验证辅助类，用于验证所有资源引用是否有效。
    /// 主要用于开发阶段的调试和文档生成，帮助发现未定义的资源引用。
    /// </summary>
    /// <remarks>
    /// 此类的验证逻辑目前是基础实现，主要用于文档和调试目的。
    /// 未来可以扩展为更完整的资源验证工具，包括：
    /// - 检查所有 DynamicResource 和 StaticResource 引用
    /// - 验证资源类型匹配
    /// - 检查资源字典的合并顺序
    /// </remarks>
    public static class ResourceValidator
    {
        /// <summary>
        /// 验证资源字典中所有引用的资源是否存在。
        /// </summary>
        /// <param name="resourceDictionary">要验证的资源字典</param>
        /// <returns>缺失的资源键列表（当前实现返回空列表）</returns>
        /// <remarks>
        /// 当前实现会收集所有资源键，但验证逻辑尚未完全实现。
        /// 主要用于获取资源字典中的所有键，供其他工具使用。
        /// </remarks>
        public static List<string> ValidateResources(ResourceDictionary resourceDictionary)
        {
            var missingResources = new List<string>();
            var allResourceKeys = GetAllResourceKeys(resourceDictionary);

            // 这里可以添加更详细的验证逻辑
            // 目前主要用于文档和调试

            return missingResources;
        }

        private static HashSet<string> GetAllResourceKeys(ResourceDictionary resourceDictionary)
        {
            var keys = new HashSet<string>();

            foreach (var key in resourceDictionary.Keys)
            {
                if (key is string stringKey)
                {
                    keys.Add(stringKey);
                }
            }

            // 递归检查合并的资源字典
            foreach (var mergedDict in resourceDictionary.MergedDictionaries)
            {
                var mergedKeys = GetAllResourceKeys(mergedDict);
                foreach (var key in mergedKeys)
                {
                    keys.Add(key);
                }
            }

            return keys;
        }
    }
}

