---
name: xamldisplay_migration
overview: 将XamlDisplay的命名空间从LOLThemes.Wpf.Controls迁移到LOLThemes.Wpf.ShowMeTheXAML，并确保IsExpanded属性正常工作，不使用PowerShell批量替换。
todos:
  - id: search-namespace-references
    content: 搜索项目中所有LOLThemes.Wpf.Controls命名空间引用
    status: pending
  - id: update-xamldisplay-styles
    content: 手动更新XamlDisplayStyles.xaml中的命名空间声明
    status: pending
    dependencies:
      - search-namespace-references
  - id: update-button-showcase
    content: 更新ButtonShowcaseView.xaml的命名空间和控件引用
    status: pending
    dependencies:
      - update-xamldisplay-styles
  - id: update-textbox-showcase
    content: 更新TextBoxShowcaseView.xaml的命名空间和控件引用
    status: pending
    dependencies:
      - update-xamldisplay-styles
  - id: check-remaining-views
    content: 检查并更新其他视图文件中的命名空间引用
    status: pending
    dependencies:
      - update-button-showcase
      - update-textbox-showcase
  - id: verify-isexpanded-property
    content: 编译并验证IsExpanded属性功能正常
    status: pending
    dependencies:
      - check-remaining-views
  - id: run-sample-application
    content: 运行示例应用，确认XamlDisplay显示正确
    status: pending
    dependencies:
      - verify-isexpanded-property
---

## 产品概述

将XamlDisplay组件的命名空间从LOLThemes.Wpf.Controls迁移到LOLThemes.Wpf.ShowMeTheXAML，确保IsExpanded属性功能正常，并通过手动方式更新所有相关文件引用，避免使用PowerShell批量替换导致的编码问题。

## 核心功能

- 命名空间迁移：将XamlDisplay类从LOLThemes.Wpf.Controls迁移到LOLThemes.Wpf.ShowMeTheXAML
- XAML引用更新：手动更新所有XAML文件中的命名空间声明和控件引用
- IsExpanded属性验证：确保IsExpanded依赖属性在迁移后正常工作
- 样式文件适配：更新XamlDisplayStyles.xaml中的命名空间引用
- 代码完整性检查：验证所有视图文件正确引用新的命名空间

## 技术栈

- 框架：WPF (.NET)
- 语言：C# / XAML
- 项目类型：控件库与示例项目

## 技术架构

### 系统架构

本项目为WPF主题控件库，采用分层架构：

- **LOLThemes.Wpf**：核心控件库，包含XamlDisplay组件
- **LOLThemes.Wpf.Samples**：示例展示应用，包含多个视图和样式文件

### 模块划分

- **XamlDisplay核心模块**：LOLThemes.Wpf/XamlDisplay.cs - 已创建，包含IsExpandedProperty
- **样式模块**：LOLThemes.Wpf.Samples/Styles/XamlDisplayStyles.xaml - 需要更新命名空间
- **视图模块**：LOLThemes.Wpf.Samples/Views/*.xaml - 需要更新命名空间引用
- **示例模块**：LOLThemes.Wpf.Samples/ViewModels/*.cs - 可能包含相关代码引用

### 数据流

XamlDisplay作为展示控件，主要流程：
XAML文件引用 → 命名空间解析 → XamlDisplay实例化 → IsExpanded属性绑定 → 样式应用 → UI渲染

## 实现细节

### 核心目录结构

```
e:/Project2025/LoLThemes/src/
├── LOLThemes.Wpf/
│   └── XamlDisplay.cs                      # 已创建，命名空间LOLThemes.Wpf.ShowMeTheXAML
├── LOLThemes.Wpf.Samples/
│   ├── Styles/
│   │   └── XamlDisplayStyles.xaml         # 需要更新命名空间
│   └── Views/
│       ├── ButtonShowcaseView.xaml        # 需要更新命名空间引用
│       ├── TextBoxShowcaseView.xaml       # 需要更新命名空间引用
│       └── ...                            # 其他可能引用XamlDisplay的视图文件
```

### 关键代码结构

**XamlDisplay类**：已创建在LOLThemes.Wpf/XamlDisplay.cs，命名空间为LOLThemes.Wpf.ShowMeTheXAML，包含IsExpanded依赖属性。

**XAML命名空间声明**：需要从

```xml
xmlns:controls="clr-namespace:LOLThemes.Wpf.Controls"
```

更新为

```xml
xmlns:smtx="clr-namespace:LOLThemes.Wpf.ShowMeTheXAML;assembly=LOLThemes.Wpf"
```

**控件引用**：所有使用`<controls:XamlDisplay>`需要更新为`<smtx:XamlDisplay>`

### 技术实现计划

1. **问题陈述**：需要手动更新多个XAML文件中的命名空间引用，避免编码问题
2. **解决方案**：逐个文件打开，查找替换命名空间声明和控件引用
3. **关键技术**：使用VS Code或IDE的查找替换功能，逐个文件确认
4. **实现步骤**：

- 搜索所有xaml文件中的LOLThemes.Wpf.Controls命名空间引用
- 更新XamlDisplayStyles.xaml的命名空间声明
- 更新所有视图文件中的命名空间声明和控件标签
- 编译验证无错误

5. **测试策略**：编译项目，运行示例应用，验证XamlDisplay显示和IsExpanded功能