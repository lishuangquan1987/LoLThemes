---
name: Fix All MVVM Architecture Issues Using CommunityToolkit.Mvvm
overview: 使用CommunityToolkit.Mvvm的RelayCommand移除所有x:Code块，将逻辑移到ViewModel中，符合MVVM架构"
todos:
  - id: explore-codebase
    content: 使用[code-explorer]探索项目结构，定位所有包含x:Code块的XAML文件
    status: completed
  - id: fix-mainview
    content: 移除MainView.xaml中的x:Code块，在MainViewModel中实现SelectThemeCommand
    status: completed
    dependencies:
      - explore-codebase
  - id: fix-button-showcase
    content: 移除ButtonShowcaseView.xaml中的x:Code块，添加ToggleCodeExpanderCommand绑定
    status: completed
    dependencies:
      - explore-codebase
  - id: fix-textbox-showcase
    content: 移除TextBoxShowcaseView.xaml中的x:Code块并修复语法错误，添加ToggleCodeExpanderCommand绑定
    status: completed
    dependencies:
      - explore-codebase
  - id: fix-mainviewmodel-syntax
    content: 修复MainViewModel.cs第82行的多余右大括号语法错误
    status: completed
    dependencies:
      - fix-mainview
  - id: verify-refactoring
    content: 验证所有Command绑定正常工作，MVVM架构符合规范
    status: completed
    dependencies:
      - fix-mainview
      - fix-button-showcase
      - fix-textbox-showcase
      - fix-mainviewmodel-syntax
---

## Product Overview

重构WPF项目的MVVM架构，使用CommunityToolkit.Mvvm的RelayCommand替换所有x:Code块，将事件处理逻辑移至ViewModel中，修复语法错误，确保代码符合标准MVVM模式。

## Core Features

- 移除MainView.xaml中的x:Code块，实现ThemeButton_Click命令绑定
- 移除ButtonShowcaseView.xaml中的x:Code块，实现ToggleCodeExpander命令绑定
- 移除TextBoxShowcaseView.xaml中的x:Code块并修复语法错误，实现ToggleCodeExpander命令绑定
- 修复MainViewModel.cs第82行的多余右大括号语法错误
- 在MainViewModel中添加缺失的SelectThemeCommand
- 使用CommunityToolkit.Mvvm的RelayCommand实现所有命令绑定

## Tech Stack

- Framework: WPF (.NET)
- MVVM Library: CommunityToolkit.Mvvm
- Language: C#

## Tech Architecture

### 系统架构

- 架构模式：MVVM (Model-View-ViewModel)
- 现有项目结构：View层（XAML）+ ViewModel层（C#）+ Model层
- 修改策略：在现有架构基础上，将View的code-behind逻辑迁移到ViewModel层

### 模块划分

- **MainView模块**：主界面视图，包含主题切换按钮
- **ButtonShowcaseView模块**：按钮展示视图，包含代码折叠/展开功能
- **TextBoxShowcaseView模块**：文本框展示视图，包含代码折叠/展开功能
- **MainViewModel模块**：主视图模型，包含SelectThemeCommand命令

### 数据流

用户交互（点击按钮） → 触发RelayCommand → 执行ViewModel中的逻辑 → 更新UI状态

## Implementation Details

### 核心目录结构

仅显示修改或新增的文件：

```
e:/Project2025/LoLThemes/
├── Views/
│   ├── MainView.xaml          # 修改：移除x:Code块，添加Command绑定
│   ├── ButtonShowcaseView.xaml  # 修改：移除x:Code块，添加Command绑定
│   └── TextBoxShowcaseView.xaml # 修改：移除x:Code块，添加Command绑定
└── ViewModels/
    └── MainViewModel.cs       # 修改：修复语法错误，添加SelectThemeCommand
```

### 关键代码结构

**SelectThemeCommand**: 使用CommunityToolkit.Mvvm的RelayCommand实现的主题切换命令

```
[RelayCommand]
private void SelectTheme(string themeName)
{
    // 主题切换逻辑
}
```

**ToggleCodeExpander Command**: 使用RelayCommand实现的代码折叠/展开命令

```
[RelayCommand]
private void ToggleCodeExpander()
{
    IsCodeExpanded = !IsCodeExpanded;
}
```

### 技术实现计划

1. **问题分析**: 移除x:Code块，识别需要迁移的事件处理逻辑
2. **ViewModel增强**: 在ViewModel中添加对应的RelayCommand属性
3. **Command绑定**: 在XAML中通过Command属性替换Click事件处理器
4. **语法修复**: 修复MainViewModel.cs第82行的多余右大括号
5. **测试验证**: 确保所有功能在重构后正常工作

### 集成点

- View与ViewModel通过DataContext关联
- XAML Command属性绑定到ViewModel的RelayCommand属性
- 保持现有UI布局和视觉效果不变

## Agent Extensions

### SubAgent

- **code-explorer**
- Purpose: 探索现有代码结构，查找x:Code块和事件处理逻辑
- Expected outcome: 定位所有需要修改的XAML文件和ViewModel文件，理解现有代码结构