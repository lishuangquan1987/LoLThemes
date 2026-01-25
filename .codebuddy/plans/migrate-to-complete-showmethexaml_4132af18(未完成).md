---
name: migrate-to-complete-showmethexaml
overview: 将项目中的XamlDisplay替换为完整的ShowMeTheXAML实现，包括所有必要的辅助类（XamlPresenter、XamlResolver、格式化器等），并更新所有XAML引用以达到MaterialDesignInXamlToolkit示例的完整效果。
todos:
  - id: analyze-current-usage
    content: 使用 [subagent:code-explorer] 分析所有 XamlDisplay 使用情况和命名空间引用
    status: pending
  - id: implement-core-classes
    content: 创建完整的 XamlDisplay 核心类（XamlDisplay、XamlDisplayAttribute、XamlFormatter）
    status: pending
    dependencies:
      - analyze-current-usage
  - id: implement-build-task
    content: 实现 MSBuild 构建任务和 XamlResolver，支持编译期 XAML 提取
    status: pending
    dependencies:
      - implement-core-classes
  - id: update-xaml-styles
    content: 更新 LOLThemes.Wpf/Themes/Styles/XamlDisplayStyles.xaml 样式，集成 AvalonEdit
    status: pending
    dependencies:
      - implement-core-classes
  - id: migrate-namespace-references
    content: 批量更新所有 XAML 文件中的命名空间引用，从 ShowMeTheXAML 改为 LOLThemes.Wpf.ShowMeTheXAML
    status: pending
    dependencies:
      - update-xaml-styles
  - id: update-project-references
    content: 移除外部 ShowMeTheXAML 包引用，添加 AvalonEdit 依赖（如果需要）
    status: pending
    dependencies:
      - migrate-namespace-references
  - id: test-and-validate
    content: 验证所有 XamlDisplay 实例功能正常，测试复制、折叠、格式化功能
    status: pending
    dependencies:
      - update-project-references
---

## 项目概述

将项目中的 XamlDisplay 控件从当前简化实现升级为完整的 ShowMeTheXAML 实现，包含所有必要的辅助类（XamlPresenter、XamlResolver、格式化器、构建任务等），达到 MaterialDesignInXamlToolkit 示例的完整功能效果。

## 核心功能

- **完整的 XamlDisplay 控件**：支持自动 XAML 提取、格式化、复制、折叠展开
- **构建时 XAML 提取**：通过 MSBuild 任务在编译期提取 XAML 内容，避免运行时性能开销
- **XAML 格式化器**：自动格式化 XAML 代码，添加缩进和换行
- **AvalonEdit 集成**：支持语法高亮的代码编辑器显示
- **样式统一**：更新所有 XAML 引用，确保控件样式与 MaterialDesignInXamlToolkit 一致

## 视觉效果

- 卡片式布局，包含控件演示区和可折叠的 XAML 代码区
- Material Design 风格的展开/折叠动画
- 深色主题的代码块，类似 VS Code 的语法高亮
- 一键复制代码到剪贴板的功能按钮
- 流畅的悬停和交互动效

## 技术栈

- **框架**：.NET 8.0 + WPF
- **语言**：C# 12
- **构建工具**：MSBuild 自定义任务
- **编辑器组件**：AvalonEdit (用于 XAML 语法高亮)

## 系统架构

### 整体架构

```mermaid
graph TD
    A[XAML 文件] -->|编译期| B[MSBuild 任务]
    B -->|提取 XAML| C[XamlResolver]
    C -->|存储| D[Generated XAML Store]
    E[XamlDisplay 控件] -->|运行时| D
    E -->|显示| F[ContentPresenter]
    E -->|格式化| G[XamlFormatter]
    G -->|输出| H[AvalonEdit/TextBox]
    E -->|复制命令| I[Clipboard]
```

### 模块划分

1. **XamlDisplay 核心模块**（LOLThemes.Wpf.ShowMeTheXAML）

- XamlDisplay.cs：主控件，继承自 ContentControl
- XamlDisplayAttribute.cs：标记 XAML 内容的特性
- XamlFormatter.cs：XAML 格式化逻辑
- TextDocumentValueConverter.cs：AvalonEdit 文档转换器

2. **构建时解析模块**（LOLThemes.Wpf.ShowMeTheXAML.Build）

- XamlResolver.cs：XAML 提取和解析器
- MSBuild 任务：编译期执行 XAML 提取

3. **XAML 资源模块**

- XamlDisplayStyles.xaml：控件样式定义
- Generic.xaml：默认主题资源

### 数据流

1. **编译期流程**：

- MSBuild 任务扫描所有 `[XamlDisplay]` 特性标记的控件
- XamlResolver 提取控件内的 XAML 内容
- 生成静态类存储所有 XAML 字符串，以 UniqueKey 为键

2. **运行时流程**：

- XamlDisplay 控件加载时通过 UniqueKey 从存储中获取 XAML
- XamlFormatter 格式化 XAML 字符串（添加缩进、换行）
- 格式化后的 XAML 显示在 AvalonEdit 或 TextBox 中
- 用户点击复制按钮时，XAML 内容复制到剪贴板

## 实现细节

### 核心文件结构

```
src/LOLThemes.Wpf/
├── ShowMeTheXAML/
│   ├── XamlDisplay.cs                  # 主控件
│   ├── XamlDisplayAttribute.cs         # 特性标记
│   ├── XamlFormatter.cs                # XAML 格式化
│   ├── TextDocumentValueConverter.cs   # AvalonEdit 转换器
│   └── XamlResolver.cs                 # 构建时解析器
├── Themes/
│   ├── Generic.xaml                    # 默认样式
│   └── Styles/
│       └── XamlDisplayStyles.xaml      # XamlDisplay 样式
```

### 关键技术实现

**XamlDisplayAttribute**：用于标记需要提取 XAML 的控件

```
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class XamlDisplayAttribute : Attribute
{
    public string UniqueKey { get; }
    public XamlDisplayAttribute(string uniqueKey)
    {
        UniqueKey = uniqueKey;
    }
}
```

**XamlFormatter**：格式化 XAML 字符串，添加缩进和换行

```
public static class XamlFormatter
{
    public static string Format(string xaml)
    {
        // 实现 XAML 格式化逻辑
        // 1. 解析 XAML 字符串
        // 2. 添加适当缩进
        // 3. 标准化属性排列
        // 4. 返回格式化后的字符串
    }
}
```

**MSBuild 任务配置**：在 .csproj 中添加构建任务

```xml
<Target Name="GenerateXamlDisplayCache" BeforeTargets="BeforeBuild">
  <XamlDisplayResolver 
    AssemblyPath="$(TargetPath)" 
    OutputPath="$(IntermediateOutputPath)XamlDisplayCache.g.cs" />
</Target>
```

## 技术考量

### 性能优化

- 构建时提取 XAML 避免运行时性能开销
- 使用静态字典存储 XAML 内容，O(1) 查询复杂度
- AvalonEdit 使用虚拟化技术处理大段代码
- 延迟加载代码区域，仅在展开时渲染

### 依赖管理

- 移除外部 ShowMeTheXAML 包引用
- 内部实现所有核心功能
- AvalonEdit 作为可选依赖（支持回退到 TextBox）

### 错误处理

- 缺失 XAML 键值时显示友好错误提示
- 格式化失败时显示原始 XAML
- 构建时错误记录到 MSBuild 日志