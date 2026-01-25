---
name: Fix DatePicker Crash Bug
overview: 修复点击日期选择器崩溃的问题：1) 移除 DatePickerStyles.xaml 中的全局默认样式；2) 在 Colors.Dark.V3.xaml 中添加缺失的 PrimaryPurpleBrush 兼容性画刷
todos:
  - id: remove-global-style
    content: 移除 DatePickerStyles.xaml 中的全局默认样式
    status: completed
  - id: add-brush-resource
    content: 在 Colors.Dark.V3.xaml 中添加 PrimaryPurpleBrush 画刷
    status: completed
---

## Product Overview

修复日期选择器（DatePicker）的崩溃问题，确保应用能够正常显示和使用日期选择功能。

## Core Features

- 移除 DatePickerStyles.xaml 中的全局默认样式，避免影响所有 DatePicker 控件
- 在 Colors.Dark.V3.xaml 中添加 PrimaryPurpleBrush 画刷资源，解决运行时资源找不到的问题

## Tech Stack

- 框架: WPF (Windows Presentation Foundation)
- 语言: XAML

## Implementation Details

### Core Directory Structure

针对现有项目的修改，仅显示需要修改的文件：

```
project-root/
├── Resources/
│   ├── Styles/
│   │   └── DatePickerStyles.xaml      # 需要修改：移除全局样式
│   └── Themes/
│       └── Colors.Dark.V3.xaml        # 需要修改：添加缺失画刷
```

### Key Code Structures

**DatePickerStyles.xaml 修改**:
移除第20行的全局默认样式：

```xml
<!-- 需要删除的代码 -->
<Style BasedOn="{StaticResource LOLDatePickerStyle}" TargetType="{x:Type DatePicker}" />
```

**Colors.Dark.V3.xaml 修改**:
在资源字典中添加缺失的画刷资源：

```xml
<!-- 需要添加的资源 -->
<SolidColorBrush x:Key="PrimaryPurpleBrush" Color="#8B5CF6" />
```