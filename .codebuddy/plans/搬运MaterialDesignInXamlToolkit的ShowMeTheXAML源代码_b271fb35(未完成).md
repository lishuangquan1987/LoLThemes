---
name: 搬运MaterialDesignInXamlToolkit的ShowMeTheXAML源代码
overview: 直接将MaterialDesignInXamlToolkit中的ShowMeTheXAML源代码文件搬运到当前项目中，替换当前的实现，解决IsExpanded属性不存在的编译错误。
todos:
  - id: explore-project
    content: 使用[code-explorer:code-explorer]搜索并定位当前项目中ShowMeTheXAML相关文件
    status: completed
  - id: analyze-current-code
    content: 分析当前ShowMeTheXAML实现，确定需要替换的具体文件
    status: completed
    dependencies:
      - explore-project
  - id: fetch-source-code
    content: 从MaterialDesignInXamlToolkit获取ShowMeTheXAML完整源代码
    status: completed
    dependencies:
      - analyze-current-code
  - id: replace-implementation
    content: 替换当前项目中的ShowMeTheXAML源代码文件
    status: pending
    dependencies:
      - fetch-source-code
  - id: verify-compilation
    content: 验证IsExpanded属性存在且项目编译通过
    status: in_progress
    dependencies:
      - replace-implementation
---

## Product Overview

将MaterialDesignInXamlToolkit项目中的ShowMeTheXAML源代码文件搬运到当前项目中，替换当前有缺陷的实现，解决IsExpanded属性不存在的编译错误。

## Core Features

- 定位当前项目中ShowMeTheXAML相关的源代码文件
- 获取MaterialDesignInXamlToolkit中ShowMeTheXAML的完整源代码实现
- 替换当前项目的ShowMeTheXAML实现，包含IsExpanded属性支持
- 确保替换后的代码能够正常编译运行

## Tech Stack

- 目标框架：WPF (.NET)
- 源代码仓库：MaterialDesignInXamlToolkit (GitHub开源项目)

## Tech Architecture

### 系统架构

- 当前项目结构：现有WPF应用程序
- 修改范围：ShowMeTheXAML相关控件文件及其依赖项
- 替换策略：直接复制源代码文件到项目相应目录

### 模块划分

- **ShowMeTheXAML控件模块**：核心控件实现，包含IsExpanded等属性
- **辅助工具模块**：控件相关的转换器、帮助类等

### 数据流

ShowMeTheXAML控件加载 → IsExpanded属性绑定 → UI状态更新 → 展开/折叠功能执行

## Implementation Details

### Core Directory Structure

```
project-root/
├── src/
│   └── Controls/              # 或实际的ShowMeTheXAML所在目录
│       ├── ShowMeTheXAML.cs    # 需要替换的核心控件文件
│       └── ShowMeTheXAML.xaml  # 对应的XAML文件（如存在）
```

### 关键代码结构

**ShowMeTheXAML类**：实现核心的XAML展示功能，包含IsExpanded属性用于控制展开/折叠状态

```
// 预期需要添加的属性结构
public partial class ShowMeTheXAML : Control
{
    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register(
            nameof(IsExpanded),
            typeof(bool),
            typeof(ShowMeTheXAML),
            new PropertyMetadata(default(bool)));
            
    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }
}
```

### 技术实施计划

1. **问题定位**：确定当前ShowMeTheXAML文件位置和相关依赖
2. **源码获取**：从MaterialDesignInXamlToolkit仓库提取ShowMeTheXAML完整实现
3. **代码替换**：用新实现替换旧文件，保留必要的命名空间调整
4. **依赖处理**：确保替换后的代码不依赖外部库，所有依赖项都已包含
5. **编译验证**：验证IsExpanded属性正常工作，无编译错误

## Agent Extensions

### SubAgent

- **code-explorer**
- Purpose: 搜索当前项目中ShowMeTheXAML相关文件的位置和结构
- Expected outcome: 定位到需要替换的源代码文件路径及其依赖关系