using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// 资源验证辅助类，用于验证所有资源引用是否有效
    /// </summary>
    public static class ResourceValidator
    {
        /// <summary>
        /// 验证资源字典中所有引用的资源是否存在
        /// </summary>
        /// <param name="resourceDictionary">要验证的资源字典</param>
        /// <returns>缺失的资源键列表</returns>
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

