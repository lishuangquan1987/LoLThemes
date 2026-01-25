---
name: Enhance Samples UI with Theme Selector and Code Preview
overview: 为 Samples 应用添加主题选择弹出菜单、改进控件示例并实现侧边栏代码预览功能
todos:
  - id: explore-project
    content: 使用 [subagent:code-explorer] 探索项目结构，定位主题资源和示例文件
    status: completed
  - id: create-theme-manager
    content: 创建 ThemeManager 类实现主题自动加载和切换功能
    status: completed
    dependencies:
      - explore-project
  - id: add-theme-selector
    content: 在 SamplesView 中添加主题选择弹出菜单
    status: completed
    dependencies:
      - create-theme-manager
  - id: enhance-samples
    content: 完善和丰富每个控件的示例展示
    status: completed
    dependencies:
      - explore-project
  - id: implement-sidebar-layout
    content: 实现左右分栏布局结构
    status: completed
    dependencies:
      - explore-project
  - id: integrate-code-preview
    content: 集成 ShowMeTheXaml 到右侧边栏实现代码预览
    status: completed
    dependencies:
      - implement-sidebar-layout
  - id: bind-theme-command
    content: 绑定主题切换命令到 ViewModel
    status: completed
    dependencies:
      - add-theme-selector
      - create-theme-manager
---

## 产品概述

为 Samples 应用添加主题选择器、丰富控件示例，并优化代码预览功能为侧边栏分栏布局。

## 核心功能

- 主题选择弹出菜单：自动罗列所有可用主题（Dark、DarkV2、DarkV3），通过弹出式菜单让用户直观切换主题
- 控件示例增强：完善每个控件的展示示例，增加更多场景和交互
- 侧边栏代码预览：实现左右分栏布局，左侧展示控件效果，右侧实时显示对应的 XAML 代码

## 技术栈

- 框架：WPF (.NET)
- UI 组件库：MaterialDesignInXamlToolkit
- 代码高亮：ShowMeTheXaml

## 架构设计

### 系统架构

采用 MVVM 架构模式，将主题管理、控件展示和代码预览解耦。

### 模块划分

- **主题管理模块**：负责主题资源的加载、切换和应用
- **示例展示模块**：封装各类控件的示例数据和视图
- **代码预览模块**：负责 XAML 代码的提取、格式化和展示

### 数据流

用户选择主题 → 主题管理器更新资源字典 → 应用程序刷新所有控件样式

## 实现细节

### 核心目录结构

```
LoLThemes/
├── Themes/
│   ├── ThemeManager.cs           # 新增：主题管理器
│   └── Dark/                     # 主题资源文件夹
├── Views/
│   └── SamplesView.xaml          # 修改：添加主题选择器和侧边栏布局
├── ViewModels/
│   └── SamplesViewModel.cs       # 修改：添加主题切换逻辑
└── Controls/
    └── CodePreviewer.xaml        # 新增：代码预览组件
```

### 关键代码结构

**ThemeManager 类**：管理主题资源的加载和切换

```
public class ThemeManager
{
    public static readonly List<string> AvailableThemes = new() { "Dark", "DarkV2", "DarkV3" };
    public static void ApplyTheme(string themeName) { }
}
```

**CodePreviewer 控件**：侧边栏代码展示组件

```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="400"/>
    </Grid.ColumnDefinitions>
    <!-- 左侧控件展示 -->
    <!-- 右侧代码预览 -->
</Grid>
```

### 技术实现计划

1. **主题选择器实现**：创建 ThemeManager 类，扫描 Themes 目录，动态加载所有主题资源字典，通过 PopupMenu 展示
2. **控件示例增强**：为每个控件添加多种使用场景示例，包括默认状态、悬停状态、禁用状态等
3. **侧边栏代码预览**：使用 Grid 列布局，集成 ShowMeTheXaml 控件，实时提取并显示控件的 XAML 定义

### 集成点

- ThemeManager 与 App.xaml 的资源字典集成
- CodePreviewer 与 ShowMeTheXaml 的 XamlDisplay 控件集成
- SamplesViewModel 与主题切换命令绑定

## Agent 扩展

### SubAgent

- **code-explorer**
- 用途：搜索和分析现有项目结构，定位主题文件、控件示例代码和现有布局代码
- 预期结果：获取当前项目的文件结构，识别需要修改的视图和视图模型文件