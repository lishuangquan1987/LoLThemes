# LOLThemes.Wpf - 英雄联盟风格 WPF 控件库

<div align="center">

[English](README.md) | [中文文档](README.zh.md)

</div>

> **🤖 本项目完全由 AI IDE (Kiro) / Cursor 自动生成！**  
> 这是一个展示 AI 辅助开发能力的开源项目，从需求分析、设计到实现，全程由 AI 完成，人负责技术方向把控。

## 📖 项目简介

LOLThemes.Wpf 是一个开源的 WPF 控件库，为 .NET 8 桌面应用程序提供英雄联盟（League of Legends）游戏客户端的视觉风格。该项目包含完整的主题化控件集合，使开发者能够轻松创建具有 LOL 风格的桌面应用程序。

## ✨ 特性

- 🎨 **完整的主题系统** - 包含 LOL 标志性的金色、深蓝色配色方案
- 🌓 **双主题支持** - 支持暗色和亮色主题，运行时动态切换
- 📏 **尺寸主题系统** - 支持紧凑、中等、宽大三种尺寸主题，满足不同使用场景
- 🎯 **28+ 基础控件样式** - 覆盖所有常用 WPF 控件
- 🔧 **5 个自定义控件** - 六边形按钮、发光按钮、英雄卡片等
- 📦 **最小依赖** - 仅依赖 WPF 框架和 Material.Icons.WPF（用于图标）
- 🎭 **流畅动画效果** - 悬停、点击、焦点等交互动画
- 🔌 **易于集成** - 通过资源字典即可应用主题
- 🎮 **1:1 界面还原** - 示例应用展示游戏界面还原
- 🏗️ **MVVM 架构** - 示例应用采用 MVVM 模式，便于学习和扩展

## 🖼️ 截图展示

项目包含多个界面还原示例，展示控件库的实际效果：

![Screenshot 1](docs/screensoot/screenshot-1765083589850.png)
![Screenshot 2](docs/screensoot/screenshot-1765083636863.png)
![Screenshot 3](docs/screensoot/screenshot-1765083651420.png)
![Screenshot 4](docs/screensoot/screenshot-1765083660775.png)
![Screenshot 5](docs/screensoot/screenshot-1765083672845.png)

## 🚀 快速开始

### 安装

#### 方式 1: 从源码编译

```bash
# 克隆仓库
git clone https://github.com/yourusername/LOLThemes.git
cd LOLThemes

# 编译控件库
dotnet build src/LOLThemes.Wpf/LOLThemes.Wpf.csproj

# 运行示例应用
dotnet run --project src/LOLThemes.Wpf.Samples/LOLThemes.Wpf.Samples.csproj
```

#### 方式 2: NuGet 包（即将推出）

```bash
dotnet add package LOLThemes.Wpf
```

### 快速上手 - 5 步集成 LOLThemes.Wpf

#### 步骤 1: 添加项目引用

在您的 WPF 项目中，添加对 `LOLThemes.Wpf` 项目的引用：

```xml
<ProjectReference Include="path\to\LOLThemes.Wpf\LOLThemes.Wpf.csproj" />
```

或者通过 NuGet（即将推出）：
```bash
dotnet add package LOLThemes.Wpf
```

#### 步骤 2: 在 App.xaml 中引用主题资源

在 `App.xaml` 中引用 `Generic.xaml` 和默认主题资源：

```xml
<Application x:Class="YourApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!-- 引用 LOLThemes.Wpf 的主题资源 -->
                <ResourceDictionary Source="pack://application:,,,/LOLThemes.Wpf;component/Themes/Generic.xaml"/>
                
                <!-- 直接引用默认主题和尺寸资源（支持运行时动态切换） -->
                <ResourceDictionary Source="pack://application:,,,/LOLThemes.Wpf;component/Themes/Colors.Dark.xaml"/>
                <ResourceDictionary Source="pack://application:,,,/LOLThemes.Wpf;component/Themes/Sizes.Medium.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

> **重要提示**：
> - `Generic.xaml` 包含所有控件样式和字体资源
> - 必须同时在 `App.xaml` 中直接引用 `Colors.xxx.xaml` 和 `Sizes.xxx.xaml`，这样 `ThemeManager` 才能在运行时动态切换
> - 不要在 `Generic.xaml` 中引用 `Colors.xaml` 或 `Sizes.xaml`（它们已被移除）

#### 步骤 3: 在 Window 中使用控件样式

所有控件会自动应用 LOL 主题样式，无需额外设置（使用隐式样式）：

```xml
<Window x:Class="YourApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <StackPanel Margin="20">
        <!-- 默认样式自动应用 -->
        <Button Content="默认按钮" Width="200" Height="40" Margin="0,10"/>
        
        <!-- 或使用命名样式 -->
        <Button Content="主要按钮" 
                Style="{StaticResource LOLPrimaryButtonStyle}" 
                Width="200" Height="40" Margin="0,10"/>
        
        <!-- 文本框自动应用样式 -->
        <TextBox Width="200" Height="35" Margin="0,10"/>
        
        <!-- 组合框自动应用样式 -->
        <ComboBox Width="200" Height="35" Margin="0,10">
            <ComboBoxItem Content="选项 1"/>
            <ComboBoxItem Content="选项 2"/>
            <ComboBoxItem Content="选项 3"/>
        </ComboBox>
    </StackPanel>
</Window>
```

#### 步骤 4: 使用自定义控件和附加属性

```xml
<Window xmlns:lol="clr-namespace:LOLThemes.Wpf.Controls;assembly=LOLThemes.Wpf"
        xmlns:helpers="clr-namespace:LOLThemes.Wpf.Helpers;assembly=LOLThemes.Wpf">
    <StackPanel Margin="20">
        <!-- 使用自定义控件 -->
        <lol:HexagonButton Content="六边形按钮" Width="120" Height="120"/>
        
        <!-- 使用附加属性 -->
        <TextBox helpers:TextBoxHelper.Placeholder="请输入用户名..." Width="200"/>
    </StackPanel>
</Window>
```

#### 步骤 5: 实现运行时主题切换

使用 `ThemeManager` 在运行时动态切换主题和尺寸：

```csharp
using LOLThemes.Wpf.Helpers;

// 切换颜色主题（暗色/亮色）
ThemeManager.SwitchTheme(Theme.Light);  // 切换到亮色主题

// 切换尺寸主题（紧凑/中等/宽大）
ThemeManager.SwitchSizeTheme(SizeTheme.Large);  // 切换到宽大尺寸

// 订阅主题变更事件
ThemeManager.ThemeChanged += (s, e) =>
{
    Console.WriteLine($"主题已切换到: {e.NewTheme}");
};

// 获取当前主题
Theme currentTheme = ThemeManager.CurrentTheme;
SizeTheme currentSize = ThemeManager.CurrentSizeTheme;
```

### 完整示例

查看 `src/LOLThemes.Wpf.Samples` 项目，了解完整的集成示例和最佳实践。

## 📦 可用控件

### 基础控件样式

LOLThemes.Wpf 为以下 WPF 基础控件提供了完整的样式：

#### 输入控件
- ✅ **Button** - 按钮（已实现）
- ✅ **TextBox** - 文本输入框（已实现）
- ✅ **PasswordBox** - 密码输入框（已实现）
- ✅ **ComboBox** - 组合框/下拉列表（已实现）
- ✅ **CheckBox** - 复选框（已实现）
- ✅ **RadioButton** - 单选按钮（已实现）
- ✅ **ToggleButton** - 切换按钮（已实现）
- ✅ **Slider** - 滑块（已实现）

#### 显示控件
- ✅ **Label** - 标签（已实现）
- ✅ **TextBlock** - 文本块（已实现）
- ✅ **Image** - 图片（已实现）
- ✅ **ProgressBar** - 进度条（已实现）
- ✅ **ToolTip** - 提示框（已实现）

#### 容器控件
- ✅ **Border** - 边框（已实现）
- ✅ **GroupBox** - 分组框（已实现）
- ✅ **Expander** - 展开面板（已实现）
- ✅ **TabControl** - 选项卡控件（已实现）
- ✅ **Window** - 窗口（已实现）

#### 列表控件
- ✅ **ListBox** - 列表框（已实现）
- ✅ **ListView** - 列表视图（已实现）
- ✅ **DataGrid** - 数据网格（已实现）
- ✅ **TreeView** - 树形视图（已实现）

#### 菜单控件
- ✅ **Menu** - 菜单栏（已实现）
- ✅ **MenuItem** - 菜单项（已实现）
- ✅ **ContextMenu** - 上下文菜单（已实现）

#### 其他控件
- ✅ **ScrollBar** - 滚动条（已实现）
- ✅ **StatusBar** - 状态栏（已实现）
- ✅ **Separator** - 分隔符（已实现）
- ✅ **Calendar** - 日历（已实现）
- ✅ **DatePicker** - 日期选择器（已实现）
- ✅ **RichTextBox** - 富文本框（已实现）

### 自定义控件

- ✅ **HexagonButton** - 六边形按钮（已实现）
- ✅ **GlowButton** - 发光边框按钮（已实现）
- ✅ **AnimatedBorder** - 动画边框容器（已实现）
- ✅ **ChampionCard** - 英雄卡片（已实现）
- ✅ **SkillButton** - 技能按钮（支持冷却动画）（已实现）

### 辅助类和转换器

- ✅ **ButtonHelper** - Button 附加属性（已实现）
- ✅ **TextBoxHelper** - TextBox 附加属性（已实现）
- ✅ **CornerRadiusHelper** - 通用圆角辅助（已实现）
- ✅ **GlowEffectHelper** - 发光效果辅助（已实现）
- ✅ **WindowHelper** - Window 附加属性（已实现）
- ✅ **HexToColorConverter** - 十六进制颜色转换器（已实现）
- ✅ **BoolToVisibilityConverter** - 布尔到可见性转换器（已实现）
- ✅ **PercentageConverter** - 百分比转换器（已实现）
- ✅ **LevelToIndentConverter** - TreeView 缩进转换器（已实现）

## 🏗️ 项目结构

```
LOLThemes/
├── src/
│   ├── LOLThemes.Wpf/                    # 核心控件库
│   │   ├── Themes/                       # 主题资源
│   │   │   ├── Generic.xaml              # 主资源字典（包含所有控件样式）
│   │   │   ├── Colors.Dark.xaml          # 暗色主题颜色定义
│   │   │   ├── Colors.Light.xaml         # 亮色主题颜色定义
│   │   │   ├── Sizes.Compact.xaml        # 紧凑尺寸定义
│   │   │   ├── Sizes.Medium.xaml         # 中等尺寸定义
│   │   │   ├── Sizes.Large.xaml          # 宽大尺寸定义
│   │   │   ├── Fonts.xaml                # 字体定义
│   │   │   ├── Animations.xaml           # 动画定义
│   │   │   └── Styles/                   # 控件样式（38个样式文件）
│   │   │       ├── ButtonStyles.xaml
│   │   │       ├── TextBoxStyles.xaml
│   │   │       └── ...
│   │   ├── Controls/                     # 自定义控件
│   │   ├── Converters/                   # 值转换器
│   │   ├── Helpers/                      # 辅助类
│   │   │   ├── ButtonHelper.cs
│   │   │   ├── TextBoxHelper.cs
│   │   │   └── ...
│   │   └── LOLThemes.Wpf.csproj
│   │
│   └── LOLThemes.Wpf.Samples/            # 示例应用程序
│       ├── Views/                        # 视图页面
│       ├── ViewModels/                   # 视图模型
│       ├── Assets/                       # 资源文件
│       └── LOLThemes.Wpf.Samples.csproj
│
├── docs/                                 # 文档目录
│   ├── screensoot/                       # 示例截图
│   └── ShowMeTheXAML-Usage.md           # ShowMeTheXAML 使用说明
├── LICENSE                               # 许可证
└── README.md                             # 本文件
```

## 🎨 主题颜色

LOLThemes.Wpf 使用英雄联盟的标志性配色方案，支持**暗色主题**和**亮色主题**两种模式，可在运行时动态切换。

### 暗色主题（默认）

| 颜色名称 | 十六进制 | 用途 |
|---------|---------|------|
| 主要金色 | `#C8AA6E` | 强调色、边框、按钮 |
| 深蓝色 | `#010A13` | 主要背景色 |
| 青色 | `#0AC8B9` | 次要强调色、成功状态 |
| 中等背景 | `#1E2328` | 输入框背景 |
| 浅背景 | `#2A2E35` | 悬停背景 |
| 主要文本 | `#F0E6D2` | 主要文本颜色 |
| 次要文本 | `#A09B8C` | 次要文本颜色 |
| 禁用文本 | `#5B5A56` | 禁用状态文本 |

### 亮色主题

亮色主题使用浅色背景和深色文本，提供更好的可读性和现代感，同时保持英雄联盟的设计风格。

## 🤝 贡献指南

我们欢迎所有形式的贡献！无论是报告 Bug、提出新功能建议，还是提交代码改进。

### 如何贡献

1. **Fork 本仓库**
2. **创建特性分支** (`git checkout -b feature/AmazingFeature`)
3. **提交更改** (`git commit -m 'Add some AmazingFeature'`)
4. **推送到分支** (`git push origin feature/AmazingFeature`)
5. **开启 Pull Request**

### 报告问题

如果您发现了 Bug 或有功能建议，请：

1. 在 [Issues](https://github.com/yourusername/LOLThemes/issues) 页面创建新问题
2. 使用清晰的标题和详细的描述
3. 如果是 Bug，请提供复现步骤和环境信息
4. 如果可能，附上截图或代码示例

### 开发指南

- 遵循现有的代码风格和命名约定
- 为新功能添加相应的文档
- 确保代码能够成功编译
- 每个控件样式应该有独立的 XAML 文件
- 使用 Helper 类通过附加属性扩展控件功能

## 📄 许可证

本项目采用 MIT 许可证 - 详见 [LICENSE](LICENSE) 文件

## 🙏 致谢

- **Riot Games** - 感谢英雄联盟提供的视觉设计灵感
- **AI 技术** - 本项目完全由 AI IDE (Kiro) 生成，展示了 AI 辅助开发的强大能力
- **开源社区** - 感谢所有为开源项目做出贡献的开发者

## 📞 联系方式

- 项目主页: [https://github.com/yourusername/LOLThemes](https://github.com/yourusername/LOLThemes)
- 问题反馈: [https://github.com/yourusername/LOLThemes/issues](https://github.com/yourusername/LOLThemes/issues)

## 🔮 未来计划

- [x] 完成所有 28+ 基础控件样式 ✅
- [x] 实现所有 5 个自定义控件（HexagonButton, GlowButton, AnimatedBorder, ChampionCard, SkillButton）✅
- [x] 添加更多主题变体（暗色/亮色）✅
- [x] 支持主题动态切换 ✅
- [x] 实现尺寸主题切换（紧凑/中等/宽大）✅
- [ ] 发布 NuGet 包
- [ ] 创建交互式文档网站
- [ ] 添加更多示例和教程
- [ ] 添加更多 LOL 特色控件

---

⭐ 如果这个项目对您有帮助，请给我们一个 Star！

🤖 **再次强调：本项目完全由 AI 自动生成，展示了 AI 在软件开发领域的巨大潜力！**
