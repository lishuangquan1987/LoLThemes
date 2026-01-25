---
name: Create Colors.Dark.V3 Modern Theme
overview: 创建全新的现代化深色主题 Colors.Dark.V3.xaml，更新 ThemeManager 支持 V3 主题切换，并修改 samples 应用默认使用 V3 主题
todos:
  - id: explore-structure
    content: Use [subagent:code-explorer] to explore project structure and locate theme files
    status: completed
  - id: create-v3-theme
    content: Create Colors.Dark.V3.xaml with modern dark color scheme
    status: completed
    dependencies:
      - explore-structure
  - id: update-enum
    content: Add DarkV3 to ThemeType enum
    status: completed
    dependencies:
      - explore-structure
  - id: update-manager
    content: Extend ThemeManager to support V3 theme loading
    status: completed
    dependencies:
      - update-enum
  - id: update-samples
    content: Configure samples app to use DarkV3 as default theme
    status: completed
    dependencies:
      - update-manager
---

## Product Overview

为 LoLThemes 项目创建名为 Colors.Dark.V3.xaml 的全新现代化深色主题，更新 ThemeManager 以支持 V3 主题切换，并修改 samples 应用默认使用 V3 主题。

## Core Features

- 创建 Colors.Dark.V3.xaml 资源文件，定义现代化配色方案
- 更新 ThemeType 枚举，添加 DarkV3 选项
- 修改 ThemeManager 逻辑，支持 V3 主题的加载和切换
- 更新 samples 应用配置，将默认主题设置为 DarkV3

## Tech Stack

- 项目类型: WPF/XAML 项目
- 开发语言: C# + XAML
- 资源管理: ResourceDictionary

## Architecture Design

### System Architecture

基于现有项目的主题管理系统架构，不引入新的架构模式。

### Module Division

- **Resources Module**: 主题资源文件（Colors.Dark.xaml, Colors.Dark.V2.xaml, Colors.Dark.V3.xaml）
- **ThemeManager Module**: 主题管理器，负责主题枚举定义和主题切换逻辑
- **Samples Application**: 示例应用，使用并展示主题效果

### Data Flow

应用启动 → ThemeManager 加载默认主题（DarkV3）→ 合并对应的 ResourceDictionary → 应用样式到 UI

## Implementation Details

### Core Directory Structure (Modified/New Files)

```
LoLThemes/
├── Themes/
│   └── Colors.Dark.V3.xaml      # 新建: V3 深色主题资源文件
├── ThemeManager/
│   └── ThemeType.cs             # 修改: 添加 DarkV3 枚举值
│   └── ThemeManager.cs          # 修改: 添加 V3 主题加载逻辑
└── samples/
    └── App.xaml                 # 修改: 设置默认主题为 DarkV3
```

### Key Code Structures

**ThemeType 枚举扩展**: 在现有枚举中添加 DarkV3 选项

```
public enum ThemeType
{
    Light,
    Dark,
    DarkV2,
    DarkV3  // 新增
}
```

**ThemeManager 主题切换方法**: 扩展 SwitchTheme 方法支持 V3

```
public void SwitchTheme(ThemeType theme)
{
    string resourceUri = theme switch
    {
        ThemeType.Light => "/Themes/Colors.Light.xaml",
        ThemeType.Dark => "/Themes/Colors.Dark.xaml",
        ThemeType.DarkV2 => "/Themes/Colors.Dark.V2.xaml",
        ThemeType.DarkV3 => "/Themes/Colors.Dark.V3.xaml",  // 新增
        _ => throw new ArgumentException("Unknown theme")
    };
    // 加载并应用资源字典
}
```

### Technical Implementation Plan

1. **主题文件创建**: 参考 Colors.Dark.V2.xaml 的结构，设计现代化的深色配色方案
2. **枚举更新**: 在 ThemeType 中添加 DarkV3 枚举值
3. **管理器扩展**: 修改 ThemeManager.SwitchTheme 方法，添加 V3 资源路径映射
4. **应用配置**: 更新 samples/App.xaml，将默认主题设置从 DarkV2 改为 DarkV3

### Integration Points

- ThemeManager 与 WPF 资源系统集成
- ResourceDictionary 合并机制
- Application.Current.Resources 的动态更新

## Agent Extensions

### SubAgent

- **code-explorer**
- Purpose: 探索 LoLThemes 项目结构，定位 Colors.Dark.V2.xaml、ThemeType.cs、ThemeManager.cs 和 samples/App.xaml 的具体位置和内容
- Expected outcome: 获取现有主题文件的结构、ThemeManager 的实现逻辑、以及 samples 应用的配置方式，为创建 V3 主题和修改相关代码提供准确的参考