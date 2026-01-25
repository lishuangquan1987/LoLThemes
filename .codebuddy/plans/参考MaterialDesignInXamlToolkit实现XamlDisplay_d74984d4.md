---
name: 参考MaterialDesignInXamlToolkit实现XamlDisplay
overview: 分析MaterialDesignInXamlToolkit中XamlDisplay的使用方式、引用的库和样式定义，直接复制其配置到当前项目，达到相同的视觉效果。
todos:
  - id: explore-materialdesign-source
    content: 使用 [subagent:code-explorer] 分析 MaterialDesignInXamlToolkit 中的 XamlDisplay 源代码
    status: completed
  - id: identify-dependencies
    content: 识别并整理 XamlDisplay 依赖的库和命名空间
    status: completed
    dependencies:
      - explore-materialdesign-source
  - id: analyze-current-implementation
    content: 使用 [subagent:code-explorer] 分析当前项目中 XamlDisplay 的实现状态
    status: completed
  - id: migrate-xaml-styles
    content: 将 MaterialDesignInXamlToolkit 的 XamlDisplay 样式迁移到当前项目
    status: completed
    dependencies:
      - identify-dependencies
      - analyze-current-implementation
  - id: implement-control-logic
    content: 完善 XamlDisplay.cs 控件逻辑（复制、展示等功能）
    status: completed
    dependencies:
      - migrate-xaml-styles
  - id: integrate-with-themes
    content: 将 XamlDisplay 样式集成到现有主题系统（Dark.V3 等）
    status: completed
    dependencies:
      - implement-control-logic
  - id: create-showcase-demo
    content: 在 Samples 项目中创建 XamlDisplay 使用示例和演示页面
    status: completed
    dependencies:
      - integrate-with-themes
---

## 产品概述

参考 MaterialDesignInXamlToolkit 中的 XamlDisplay 实现方式，分析其引用的库和样式定义，将相关配置迁移到当前 LoLThemes.Wpf 项目中，实现相同的视觉效果和交互功能。

## 核心功能

- 分析 MaterialDesignInXamlToolkit 中 XamlDisplay 的源代码实现
- 识别并复制必要的依赖库引用
- 迁移样式定义和模板资源
- 在当前项目中实现等效的 XamlDisplay 控件
- 确保代码展示、复制等交互功能完整

## 技术栈

- 框架：WPF (Windows Presentation Foundation)
- 语言：C# + XAML
- 目标项目：LoLThemes.Wpf 控件库

## 技术架构

### 系统架构

采用组件化设计，将 XamlDisplay 作为独立控件集成到现有 LoLThemes.Wpf 控件库中。

### 模块划分

- **XamlDisplay 核心模块**：控件逻辑实现
- **样式资源模块**：XAML 样式和模板定义
- **主题集成模块**：与现有主题系统（Colors.Dark.V3.xaml 等）集成
- **示例展示模块**：在 Samples 项目中演示用法

### 实现细节

#### 关键代码结构

**XamlDisplay 类**：继承自 ContentControl，提供 XAML 代码展示和复制功能

```
// 核心控件类
public class XamlDisplay : ContentControl
{
    // XAML 代码内容
    public string Xaml { get; set; }
    
    // 复制命令
    public ICommand CopyCommand { get; }
    
    // 代码展示区域
    public TextBox CodeTextBox { get; private set; }
}
```

**样式资源**：定义控件模板，包含内容展示区、代码展示区、复制按钮等视觉元素

```
<!-- 控件样式模板 -->
<Style TargetType="{x:Type local:XamlDisplay}">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type local:XamlDisplay}">
                <!-- 布局结构：顶部内容区 + 底部代码区 -->
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Agent Extensions

### SubAgent

- **code-explorer**
- 用途：搜索和分析 MaterialDesignInXamlToolkit 源代码库中的 XamlDisplay 实现
- 预期结果：定位 XamlDisplay 的源代码文件、依赖库引用和样式定义位置
- 用途：分析当前 LoLThemes 项目中与 XamlDisplay 相关的文件结构
- 预期结果：识别已有的 XamlDisplay.cs、XamlDisplayStyles.xaml 等文件内容