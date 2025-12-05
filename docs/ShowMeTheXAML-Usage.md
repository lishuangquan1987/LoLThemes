# ShowMeTheXAML 控件使用指南

## 简介

`ShowMeTheXAML` 是一个自定义控件，用于自动显示包裹在其中的控件的 XAML 代码。它使用 `XamlWriter.Save()` 方法自动提取控件的 XAML 表示，无需手动编写代码示例。

## 基本用法

### 1. 引用命名空间

```xml
<UserControl xmlns:controls="clr-namespace:LOLThemes.Wpf.Controls;assembly=LOLThemes.Wpf">
    <!-- ... -->
</UserControl>
```

### 2. 包裹控件

```xml
<controls:ShowMeTheXAML>
    <Button Content="示例按钮" 
           Style="{StaticResource LOLButtonStyle}"
           Width="150" Height="40"/>
</controls:ShowMeTheXAML>
```

### 3. 包裹多个控件

```xml
<controls:ShowMeTheXAML>
    <StackPanel>
        <Button Content="按钮 1" Style="{StaticResource LOLButtonStyle}"/>
        <Button Content="按钮 2" Style="{StaticResource LOLPrimaryButtonStyle}"/>
        <Button Content="按钮 3" Style="{StaticResource LOLSecondaryButtonStyle}"/>
    </StackPanel>
</controls:ShowMeTheXAML>
```

## 属性

### IsExpanded

控制代码块是否默认展开。

```xml
<controls:ShowMeTheXAML IsExpanded="True">
    <Button Content="示例按钮"/>
</controls:ShowMeTheXAML>
```

## 功能特性

1. **自动代码生成**：自动提取包裹控件的 XAML 代码
2. **代码格式化**：自动格式化 XAML 代码，添加缩进和换行
3. **复制功能**：一键复制代码到剪贴板
4. **可折叠**：使用 Expander 实现代码块的展开/折叠
5. **美观样式**：使用深色代码块样式，类似 VS Code

## 注意事项

1. **XamlWriter 限制**：
   - 无法序列化绑定（Binding）
   - 无法序列化资源引用（StaticResource/DynamicResource）
   - 某些复杂属性可能无法完全保留

2. **性能考虑**：
   - 代码生成在控件加载时执行
   - 对于复杂控件，可能需要一些时间

3. **最佳实践**：
   - 包裹单个控件或简单的控件组合
   - 避免包裹包含大量数据的控件（如 DataGrid 的大量行）

## 示例

### 示例 1：单个按钮

```xml
<controls:ShowMeTheXAML>
    <Button Content="点击我" 
           Style="{StaticResource LOLButtonStyle}"
           Width="200" Height="45"/>
</controls:ShowMeTheXAML>
```

### 示例 2：多个控件

```xml
<controls:ShowMeTheXAML>
    <StackPanel Orientation="Horizontal">
        <Button Content="主要" Style="{StaticResource LOLPrimaryButtonStyle}"/>
        <Button Content="次要" Style="{StaticResource LOLSecondaryButtonStyle}"/>
        <Button Content="成功" Style="{StaticResource LOLSuccessButtonStyle}"/>
    </StackPanel>
</controls:ShowMeTheXAML>
```

### 示例 3：自定义控件

```xml
<controls:ShowMeTheXAML>
    <controls:GlowButton Content="发光按钮"
                        GlowColor="#C8AA6E"
                        GlowIntensity="20"
                        Width="200" Height="45"/>
</controls:ShowMeTheXAML>
```

## 迁移指南

### 从硬编码代码块迁移

**之前（硬编码）：**
```xml
<Expander>
    <Expander.Header>查看 XAML 代码</Expander.Header>
    <Border Style="{StaticResource CodeBlockContainerStyle}">
        <TextBlock Text="&lt;Button Content=&quot;按钮&quot;/&gt;"/>
    </Border>
</Expander>
```

**之后（使用 ShowMeTheXAML）：**
```xml
<controls:ShowMeTheXAML>
    <Button Content="按钮"/>
</controls:ShowMeTheXAML>
```

## 技术实现

- 使用 `XamlWriter.Save()` 方法序列化控件
- 自定义格式化算法处理 XAML 代码的缩进和换行
- 使用 `Clipboard.SetText()` 实现复制功能
- 基于 `ContentControl` 实现，支持任意子控件

