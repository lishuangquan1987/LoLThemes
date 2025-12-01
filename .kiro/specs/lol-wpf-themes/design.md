# Design Document - LOL WPF Themes

## Overview

LOLThemes.Wpf 是一个开源的WPF控件库，旨在为.NET 8桌面应用程序提供英雄联盟（League of Legends）游戏客户端的视觉风格。该项目包含两个主要组件：

1. **LOLThemes.Wpf** - 核心控件库，提供完整的主题化控件集合
2. **LOLThemes.Wpf.Samples** - 示例应用程序，展示控件使用方法并1:1还原游戏界面截图

设计目标：
- 提供完整的WPF基础控件样式（28+个控件）
- 创建LOL特色的自定义控件
- 实现流畅的交互动画和视觉反馈
- 保持代码的可维护性和可扩展性
- 零第三方UI库依赖

## Architecture

### 项目结构

```
src/
├── LOLThemes.Wpf/                    # 核心控件库
│   ├── Themes/                       # 主题资源
│   │   ├── Generic.xaml              # 主资源字典
│   │   ├── Colors.xaml               # 颜色定义
│   │   ├── Fonts.xaml                # 字体定义
│   │   ├── Styles/                   # 控件样式
│   │   │   ├── ButtonStyles.xaml
│   │   │   ├── TextBoxStyles.xaml
│   │   │   ├── ComboBoxStyles.xaml
│   │   │   ├── ListBoxStyles.xaml
│   │   │   ├── DataGridStyles.xaml
│   │   │   ├── CheckBoxStyles.xaml
│   │   │   ├── RadioButtonStyles.xaml
│   │   │   ├── ProgressBarStyles.xaml
│   │   │   ├── SliderStyles.xaml
│   │   │   ├── ScrollBarStyles.xaml
│   │   │   ├── TabControlStyles.xaml
│   │   │   ├── MenuStyles.xaml
│   │   │   ├── WindowStyles.xaml
│   │   │   └── ... (其他控件样式)
│   │   └── Animations.xaml           # 动画资源
│   ├── Controls/                     # 自定义控件
│   │   ├── HexagonButton.cs
│   │   ├── GlowButton.cs
│   │   ├── AnimatedBorder.cs
│   │   ├── ChampionCard.cs
│   │   ├── SkillButton.cs
│   │   └── ... (其他自定义控件)
│   ├── Converters/                   # 值转换器
│   │   ├── HexToColorConverter.cs
│   │   ├── BoolToVisibilityConverter.cs
│   │   ├── PercentageConverter.cs
│   │   └── ... (其他转换器)
│   ├── Helpers/                      # 辅助类（每个基础控件一个Helper）
│   │   ├── ButtonHelper.cs           # Button附加属性
│   │   ├── TextBoxHelper.cs          # TextBox附加属性
│   │   ├── ComboBoxHelper.cs         # ComboBox附加属性
│   │   ├── ListBoxHelper.cs          # ListBox附加属性
│   │   ├── ProgressBarHelper.cs      # ProgressBar附加属性
│   │   ├── CornerRadiusHelper.cs     # 通用圆角辅助
│   │   ├── GlowEffectHelper.cs       # 通用发光效果辅助
│   │   └── ... (其他控件Helper)
│   └── LOLThemes.Wpf.csproj
│
└── LOLThemes.Wpf.Samples/            # 示例应用程序
    ├── Views/                        # 视图页面
    │   ├── MainWindow.xaml
    │   ├── Screenshot1View.xaml      # 对应截图1
    │   ├── Screenshot2View.xaml      # 对应截图2
    │   ├── Screenshot3View.xaml
    │   ├── Screenshot4View.xaml
    │   ├── Screenshot5View.xaml
    │   ├── Screenshot6View.xaml
    │   └── ControlShowcaseView.xaml  # 控件展示页
    ├── ViewModels/                   # 视图模型
    │   ├── MainViewModel.cs
    │   └── ... (其他ViewModel)
    ├── Assets/                       # 资源文件
    │   ├── Images/
    │   └── Fonts/
    ├── App.xaml                      # 应用程序入口
    ├── App.xaml.cs
    └── LOLThemes.Wpf.Samples.csproj
```

### 架构层次

```
┌─────────────────────────────────────┐
│   LOLThemes.Wpf.Samples (示例层)    │
│   - Views (XAML界面)                │
│   - ViewModels (数据绑定)           │
└─────────────────────────────────────┘
              ↓ 引用
┌─────────────────────────────────────┐
│   LOLThemes.Wpf (控件库层)          │
│   ┌───────────────────────────────┐ │
│   │ Custom Controls (自定义控件)  │ │
│   └───────────────────────────────┘ │
│   ┌───────────────────────────────┐ │
│   │ Styles & Templates (样式模板) │ │
│   └───────────────────────────────┘ │
│   ┌───────────────────────────────┐ │
│   │ Theme System (主题系统)       │ │
│   │ - Colors, Fonts, Animations   │ │
│   └───────────────────────────────┘ │
│   ┌───────────────────────────────┐ │
│   │ Helpers & Converters (工具层) │ │
│   └───────────────────────────────┘ │
└─────────────────────────────────────┘
              ↓ 基于
┌─────────────────────────────────────┐
│   WPF Framework (.NET 8)            │
└─────────────────────────────────────┘
```

## Components and Interfaces

### 1. Theme System (主题系统)

**Colors.xaml** - 颜色资源定义
```xml
<ResourceDictionary>
    <!-- 主要颜色 -->
    <Color x:Key="PrimaryGold">#C8AA6E</Color>
    <Color x:Key="PrimaryDarkBlue">#010A13</Color>
    <Color x:Key="PrimaryCyan">#0AC8B9</Color>
    
    <!-- 背景颜色 -->
    <Color x:Key="BackgroundDark">#010A13</Color>
    <Color x:Key="BackgroundMedium">#1E2328</Color>
    <Color x:Key="BackgroundLight">#2A2E35</Color>
    
    <!-- 文本颜色 -->
    <Color x:Key="TextPrimary">#F0E6D2</Color>
    <Color x:Key="TextSecondary">#A09B8C</Color>
    <Color x:Key="TextDisabled">#5B5A56</Color>
    
    <!-- 边框颜色 -->
    <Color x:Key="BorderGold">#C8AA6E</Color>
    <Color x:Key="BorderDark">#1E2328</Color>
    <Color x:Key="BorderLight">#463714</Color>
    
    <!-- 状态颜色 -->
    <Color x:Key="HoverGold">#F0E6D2</Color>
    <Color x:Key="PressedGold">#785A28</Color>
    <Color x:Key="SuccessGreen">#0AC8B9</Color>
    <Color x:Key="ErrorRed">#E84057</Color>
    
    <!-- SolidColorBrush -->
    <SolidColorBrush x:Key="PrimaryGoldBrush" Color="{StaticResource PrimaryGold}"/>
    <!-- ... 其他Brush定义 -->
</ResourceDictionary>
```

**Fonts.xaml** - 字体资源定义
```xml
<ResourceDictionary>
    <!-- 字体族 -->
    <FontFamily x:Key="PrimaryFont">Segoe UI, Arial, sans-serif</FontFamily>
    
    <!-- 字体大小 -->
    <System:Double x:Key="FontSizeSmall">12</System:Double>
    <System:Double x:Key="FontSizeNormal">14</System:Double>
    <System:Double x:Key="FontSizeMedium">16</System:Double>
    <System:Double x:Key="FontSizeLarge">20</System:Double>
    <System:Double x:Key="FontSizeXLarge">24</System:Double>
    <System:Double x:Key="FontSizeXXLarge">32</System:Double>
    
    <!-- 字体权重 -->
    <FontWeight x:Key="FontWeightRegular">Normal</FontWeight>
    <FontWeight x:Key="FontWeightMedium">Medium</FontWeight>
    <FontWeight x:Key="FontWeightBold">Bold</FontWeight>
</ResourceDictionary>
```

**Animations.xaml** - 动画资源定义
```xml
<ResourceDictionary>
    <!-- 颜色过渡动画 -->
    <Storyboard x:Key="ButtonHoverAnimation">
        <ColorAnimation Duration="0:0:0.2" 
                       Storyboard.TargetProperty="(Border.BorderBrush).(SolidColorBrush.Color)"
                       To="{StaticResource HoverGold}"/>
    </Storyboard>
    
    <!-- 缩放动画 -->
    <Storyboard x:Key="ScaleUpAnimation">
        <DoubleAnimation Duration="0:0:0.15"
                        Storyboard.TargetProperty="(UIElement.RenderTransform).(ScaleTransform.ScaleX)"
                        To="1.05"/>
        <DoubleAnimation Duration="0:0:0.15"
                        Storyboard.TargetProperty="(UIElement.RenderTransform).(ScaleTransform.ScaleY)"
                        To="1.05"/>
    </Storyboard>
    
    <!-- 透明度动画 -->
    <Storyboard x:Key="FadeInAnimation">
        <DoubleAnimation Duration="0:0:0.3"
                        Storyboard.TargetProperty="Opacity"
                        From="0" To="1"/>
    </Storyboard>
</ResourceDictionary>
```

### 2. Control Styles (控件样式)

每个基础控件都有独立的样式文件，包含完整的ControlTemplate定义。

**ButtonStyles.xaml** - 按钮样式示例
```xml
<Style x:Key="LOLButtonStyle" TargetType="Button">
    <Setter Property="Background" Value="Transparent"/>
    <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}"/>
    <Setter Property="BorderBrush" Value="{StaticResource BorderGoldBrush}"/>
    <Setter Property="BorderThickness" Value="2"/>
    <Setter Property="Padding" Value="20,10"/>
    <Setter Property="FontFamily" Value="{StaticResource PrimaryFont}"/>
    <Setter Property="FontSize" Value="{StaticResource FontSizeNormal}"/>
    <Setter Property="FontWeight" Value="{StaticResource FontWeightMedium}"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border x:Name="border"
                       Background="{TemplateBinding Background}"
                       BorderBrush="{TemplateBinding BorderBrush}"
                       BorderThickness="{TemplateBinding BorderThickness}"
                       CornerRadius="0">
                    <ContentPresenter HorizontalAlignment="Center"
                                    VerticalAlignment="Center"
                                    Margin="{TemplateBinding Padding}"/>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter TargetName="border" Property="Background" 
                               Value="{StaticResource PrimaryGoldBrush}"/>
                        <Setter Property="Foreground" 
                               Value="{StaticResource BackgroundDarkBrush}"/>
                    </Trigger>
                    <Trigger Property="IsPressed" Value="True">
                        <Setter TargetName="border" Property="Background" 
                               Value="{StaticResource PressedGoldBrush}"/>
                    </Trigger>
                    <Trigger Property="IsEnabled" Value="False">
                        <Setter TargetName="border" Property="BorderBrush" 
                               Value="{StaticResource TextDisabledBrush}"/>
                        <Setter Property="Foreground" 
                               Value="{StaticResource TextDisabledBrush}"/>
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

### 3. Custom Controls (自定义控件)

**HexagonButton.cs** - 六边形按钮
```csharp
public class HexagonButton : Button
{
    static HexagonButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(HexagonButton),
            new FrameworkPropertyMetadata(typeof(HexagonButton)));
    }
    
    // 六边形路径几何
    public static readonly DependencyProperty HexagonGeometryProperty =
        DependencyProperty.Register(
            nameof(HexagonGeometry),
            typeof(Geometry),
            typeof(HexagonButton),
            new PropertyMetadata(CreateHexagonGeometry()));
    
    public Geometry HexagonGeometry
    {
        get => (Geometry)GetValue(HexagonGeometryProperty);
        set => SetValue(HexagonGeometryProperty, value);
    }
    
    private static Geometry CreateHexagonGeometry()
    {
        // 创建六边形路径
        var geometry = new PathGeometry();
        var figure = new PathFigure { StartPoint = new Point(50, 0) };
        figure.Segments.Add(new LineSegment(new Point(100, 25), true));
        figure.Segments.Add(new LineSegment(new Point(100, 75), true));
        figure.Segments.Add(new LineSegment(new Point(50, 100), true));
        figure.Segments.Add(new LineSegment(new Point(0, 75), true));
        figure.Segments.Add(new LineSegment(new Point(0, 25), true));
        figure.IsClosed = true;
        geometry.Figures.Add(figure);
        return geometry;
    }
}
```

**GlowButton.cs** - 发光按钮
```csharp
public class GlowButton : Button
{
    static GlowButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(GlowButton),
            new FrameworkPropertyMetadata(typeof(GlowButton)));
    }
    
    // 发光颜色
    public static readonly DependencyProperty GlowColorProperty =
        DependencyProperty.Register(
            nameof(GlowColor),
            typeof(Color),
            typeof(GlowButton),
            new PropertyMetadata(Colors.Gold));
    
    public Color GlowColor
    {
        get => (Color)GetValue(GlowColorProperty);
        set => SetValue(GlowColorProperty, value);
    }
    
    // 发光强度
    public static readonly DependencyProperty GlowIntensityProperty =
        DependencyProperty.Register(
            nameof(GlowIntensity),
            typeof(double),
            typeof(GlowButton),
            new PropertyMetadata(10.0));
    
    public double GlowIntensity
    {
        get => (double)GetValue(GlowIntensityProperty);
        set => SetValue(GlowIntensityProperty, value);
    }
}
```

**ChampionCard.cs** - 英雄卡片
```csharp
public class ChampionCard : ContentControl
{
    static ChampionCard()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ChampionCard),
            new FrameworkPropertyMetadata(typeof(ChampionCard)));
    }
    
    // 英雄名称
    public static readonly DependencyProperty ChampionNameProperty =
        DependencyProperty.Register(
            nameof(ChampionName),
            typeof(string),
            typeof(ChampionCard),
            new PropertyMetadata(string.Empty));
    
    public string ChampionName
    {
        get => (string)GetValue(ChampionNameProperty);
        set => SetValue(ChampionNameProperty, value);
    }
    
    // 英雄图片
    public static readonly DependencyProperty ChampionImageProperty =
        DependencyProperty.Register(
            nameof(ChampionImage),
            typeof(ImageSource),
            typeof(ChampionCard),
            new PropertyMetadata(null));
    
    public ImageSource ChampionImage
    {
        get => (ImageSource)GetValue(ChampionImageProperty);
        set => SetValue(ChampionImageProperty, value);
    }
    
    // 是否选中
    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(
            nameof(IsSelected),
            typeof(bool),
            typeof(ChampionCard),
            new PropertyMetadata(false));
    
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
}
```

**SkillButton.cs** - 技能按钮
```csharp
public class SkillButton : Button
{
    static SkillButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SkillButton),
            new FrameworkPropertyMetadata(typeof(SkillButton)));
    }
    
    // 冷却进度 (0-1)
    public static readonly DependencyProperty CooldownProgressProperty =
        DependencyProperty.Register(
            nameof(CooldownProgress),
            typeof(double),
            typeof(SkillButton),
            new PropertyMetadata(0.0));
    
    public double CooldownProgress
    {
        get => (double)GetValue(CooldownProgressProperty);
        set => SetValue(CooldownProgressProperty, value);
    }
    
    // 技能图标
    public static readonly DependencyProperty SkillIconProperty =
        DependencyProperty.Register(
            nameof(SkillIcon),
            typeof(ImageSource),
            typeof(SkillButton),
            new PropertyMetadata(null));
    
    public ImageSource SkillIcon
    {
        get => (ImageSource)GetValue(SkillIconProperty);
        set => SetValue(SkillIconProperty, value);
    }
    
    // 快捷键文本
    public static readonly DependencyProperty HotkeyTextProperty =
        DependencyProperty.Register(
            nameof(HotkeyText),
            typeof(string),
            typeof(SkillButton),
            new PropertyMetadata(string.Empty));
    
    public string HotkeyText
    {
        get => (string)GetValue(HotkeyTextProperty);
        set => SetValue(HotkeyTextProperty, value);
    }
}
```

### 4. Converters (值转换器)

**HexToColorConverter.cs**
```csharp
public class HexToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string hexString)
        {
            return (Color)ColorConverter.ConvertFromString(hexString);
        }
        return Colors.Transparent;
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            return color.ToString();
        }
        return "#00000000";
    }
}
```

**BoolToVisibilityConverter.cs**
```csharp
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            bool invert = parameter?.ToString() == "Invert";
            bool result = invert ? !boolValue : boolValue;
            return result ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            bool invert = parameter?.ToString() == "Invert";
            bool result = visibility == Visibility.Visible;
            return invert ? !result : result;
        }
        return false;
    }
}
```

**PercentageConverter.cs**
```csharp
public class PercentageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            return $"{doubleValue:P0}";
        }
        return "0%";
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

### 5. Helpers (辅助类)

**设计原则：为每个基础控件创建独立的Helper类**

为了扩展基础控件的功能而不污染原控件，我们为每个需要额外属性的基础控件创建对应的Helper类。这些Helper类通过附加属性（Attached Properties）为控件添加新功能。

**ButtonHelper.cs** - Button附加属性
```csharp
public static class ButtonHelper
{
    // 按钮形状（Rectangle, Rounded, Circle, Hexagon）
    public static readonly DependencyProperty ShapeProperty =
        DependencyProperty.RegisterAttached(
            "Shape",
            typeof(ButtonShape),
            typeof(ButtonHelper),
            new PropertyMetadata(ButtonShape.Rectangle));
    
    public static ButtonShape GetShape(DependencyObject obj)
    {
        return (ButtonShape)obj.GetValue(ShapeProperty);
    }
    
    public static void SetShape(DependencyObject obj, ButtonShape value)
    {
        obj.SetValue(ShapeProperty, value);
    }
    
    // 图标位置（Left, Right, Top, Bottom）
    public static readonly DependencyProperty IconPlacementProperty =
        DependencyProperty.RegisterAttached(
            "IconPlacement",
            typeof(IconPlacement),
            typeof(ButtonHelper),
            new PropertyMetadata(IconPlacement.Left));
    
    public static IconPlacement GetIconPlacement(DependencyObject obj)
    {
        return (IconPlacement)obj.GetValue(IconPlacementProperty);
    }
    
    public static void SetIconPlacement(DependencyObject obj, IconPlacement value)
    {
        obj.SetValue(IconPlacementProperty, value);
    }
    
    // 图标
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.RegisterAttached(
            "Icon",
            typeof(ImageSource),
            typeof(ButtonHelper),
            new PropertyMetadata(null));
    
    public static ImageSource GetIcon(DependencyObject obj)
    {
        return (ImageSource)obj.GetValue(IconProperty);
    }
    
    public static void SetIcon(DependencyObject obj, ImageSource value)
    {
        obj.SetValue(IconProperty, value);
    }
}

public enum ButtonShape
{
    Rectangle,
    Rounded,
    Circle,
    Hexagon
}

public enum IconPlacement
{
    Left,
    Right,
    Top,
    Bottom
}
```

**TextBoxHelper.cs** - TextBox附加属性
```csharp
public static class TextBoxHelper
{
    // 占位符文本
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.RegisterAttached(
            "Placeholder",
            typeof(string),
            typeof(TextBoxHelper),
            new PropertyMetadata(string.Empty));
    
    public static string GetPlaceholder(DependencyObject obj)
    {
        return (string)obj.GetValue(PlaceholderProperty);
    }
    
    public static void SetPlaceholder(DependencyObject obj, string value)
    {
        obj.SetValue(PlaceholderProperty, value);
    }
    
    // 清除按钮可见性
    public static readonly DependencyProperty ShowClearButtonProperty =
        DependencyProperty.RegisterAttached(
            "ShowClearButton",
            typeof(bool),
            typeof(TextBoxHelper),
            new PropertyMetadata(false));
    
    public static bool GetShowClearButton(DependencyObject obj)
    {
        return (bool)obj.GetValue(ShowClearButtonProperty);
    }
    
    public static void SetShowClearButton(DependencyObject obj, bool value)
    {
        obj.SetValue(ShowClearButtonProperty, value);
    }
    
    // 图标
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.RegisterAttached(
            "Icon",
            typeof(ImageSource),
            typeof(TextBoxHelper),
            new PropertyMetadata(null));
    
    public static ImageSource GetIcon(DependencyObject obj)
    {
        return (ImageSource)obj.GetValue(IconProperty);
    }
    
    public static void SetIcon(DependencyObject obj, ImageSource value)
    {
        obj.SetValue(IconProperty, value);
    }
}
```

**CornerRadiusHelper.cs** - 通用圆角辅助类
```csharp
public static class CornerRadiusHelper
{
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.RegisterAttached(
            "CornerRadius",
            typeof(CornerRadius),
            typeof(CornerRadiusHelper),
            new PropertyMetadata(new CornerRadius(0)));
    
    public static CornerRadius GetCornerRadius(DependencyObject obj)
    {
        return (CornerRadius)obj.GetValue(CornerRadiusProperty);
    }
    
    public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
    {
        obj.SetValue(CornerRadiusProperty, value);
    }
}
```

**GlowEffectHelper.cs** - 通用发光效果辅助类
```csharp
public static class GlowEffectHelper
{
    public static readonly DependencyProperty EnableGlowProperty =
        DependencyProperty.RegisterAttached(
            "EnableGlow",
            typeof(bool),
            typeof(GlowEffectHelper),
            new PropertyMetadata(false, OnEnableGlowChanged));
    
    public static bool GetEnableGlow(DependencyObject obj)
    {
        return (bool)obj.GetValue(EnableGlowProperty);
    }
    
    public static void SetEnableGlow(DependencyObject obj, bool value)
    {
        obj.SetValue(EnableGlowProperty, value);
    }
    
    public static readonly DependencyProperty GlowColorProperty =
        DependencyProperty.RegisterAttached(
            "GlowColor",
            typeof(Color),
            typeof(GlowEffectHelper),
            new PropertyMetadata(Colors.Gold, OnGlowColorChanged));
    
    public static Color GetGlowColor(DependencyObject obj)
    {
        return (Color)obj.GetValue(GlowColorProperty);
    }
    
    public static void SetGlowColor(DependencyObject obj, Color value)
    {
        obj.SetValue(GlowColorProperty, value);
    }
    
    private static void OnEnableGlowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element && e.NewValue is bool enable)
        {
            if (enable)
            {
                var color = GetGlowColor(d);
                var dropShadow = new DropShadowEffect
                {
                    Color = color,
                    BlurRadius = 20,
                    ShadowDepth = 0,
                    Opacity = 0.8
                };
                element.Effect = dropShadow;
            }
            else
            {
                element.Effect = null;
            }
        }
    }
    
    private static void OnGlowColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (GetEnableGlow(d) && d is UIElement element && element.Effect is DropShadowEffect effect)
        {
            effect.Color = (Color)e.NewValue;
        }
    }
}
```

**其他控件Helper示例**

每个需要扩展功能的基础控件都应该有对应的Helper类：

- **ComboBoxHelper** - 下拉框图标、占位符等
- **ListBoxHelper** - 选中样式、悬停效果等
- **ProgressBarHelper** - 进度文本、动画速度等
- **SliderHelper** - 刻度显示、值标签等
- **TabControlHelper** - 选项卡位置、关闭按钮等
- **ScrollBarHelper** - 滚动条宽度、自动隐藏等
- **WindowHelper** - 窗口阴影、标题栏按钮等

## Data Models

### Sample Application ViewModels

**MainViewModel.cs** - 主窗口视图模型
```csharp
public class MainViewModel : INotifyPropertyChanged
{
    private string _currentView;
    
    public string CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<NavigationItem> NavigationItems { get; set; }
    
    public ICommand NavigateCommand { get; }
    
    public MainViewModel()
    {
        NavigationItems = new ObservableCollection<NavigationItem>
        {
            new NavigationItem { Name = "Screenshot 1", ViewName = "Screenshot1View" },
            new NavigationItem { Name = "Screenshot 2", ViewName = "Screenshot2View" },
            new NavigationItem { Name = "Screenshot 3", ViewName = "Screenshot3View" },
            new NavigationItem { Name = "Screenshot 4", ViewName = "Screenshot4View" },
            new NavigationItem { Name = "Screenshot 5", ViewName = "Screenshot5View" },
            new NavigationItem { Name = "Screenshot 6", ViewName = "Screenshot6View" },
            new NavigationItem { Name = "Control Showcase", ViewName = "ControlShowcaseView" }
        };
        
        NavigateCommand = new RelayCommand<string>(Navigate);
        CurrentView = "Screenshot1View";
    }
    
    private void Navigate(string viewName)
    {
        CurrentView = viewName;
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class NavigationItem
{
    public string Name { get; set; }
    public string ViewName { get; set; }
}
```

## C
orrectness Properties

*A property is a characteristic or behavior that should hold true across all valid executions of a system-essentially, a formal statement about what the system should do. Properties serve as the bridge between human-readable specifications and machine-verifiable correctness guarantees.*

基于需求分析，以下是可测试的正确性属性：

### Property Reflection

在定义具体属性之前，我们需要识别并消除冗余：

- 属性2.1-2.28涉及所有基础控件样式的定义，可以合并为一个通用属性：验证所有必需的控件样式是否存在于资源字典中
- 属性4.1-4.4涉及特定颜色的定义，可以合并为一个属性：验证所有必需的主题颜色是否正确定义
- 属性5.1-5.3涉及字体资源的定义，可以合并为一个属性：验证所有必需的字体资源是否正确定义
- 属性3.1-3.5涉及自定义控件的存在性，这些是独立的控件，应保持独立验证
- 属性9.1-9.5涉及转换器和辅助类，可以合并为两个属性：验证所有转换器和所有辅助类

经过反思，我们将原本50+个细粒度属性合并为以下核心属性：

### Core Properties

**Property 1: 控件样式完整性**
*For any* 必需的WPF基础控件类型（Button, TextBox, ComboBox等28个控件），资源字典中应该存在对应的样式定义，且样式包含完整的ControlTemplate
**Validates: Requirements 2.1-2.28**

**Property 2: 控件视觉状态完整性**
*For any* 交互式控件样式（Button, TextBox, ComboBox等），其ControlTemplate应该包含所有必需的视觉状态触发器（Normal, MouseOver, Pressed, Disabled, Focused等）
**Validates: Requirements 2.1, 8.1, 8.2, 8.3**

**Property 3: 自定义控件属性完整性**
*For any* 自定义控件类（HexagonButton, GlowButton, ChampionCard, SkillButton, AnimatedBorder），该控件应该定义所有必需的依赖属性
**Validates: Requirements 3.1, 3.2, 3.3, 3.4, 3.5**

**Property 4: 主题颜色定义完整性**
*For any* 必需的主题颜色键（PrimaryGold, PrimaryDarkBlue, PrimaryCyan等），资源字典中应该存在对应的Color和SolidColorBrush资源，且颜色值符合规范
**Validates: Requirements 4.1, 4.2, 4.3, 4.4, 4.5**

**Property 5: 字体资源定义完整性**
*For any* 必需的字体资源类型（字体族、字体大小、字体权重），资源字典中应该存在对应的资源定义
**Validates: Requirements 5.1, 5.2, 5.3, 5.4**

**Property 6: 转换器功能正确性**
*For any* 值转换器（HexToColorConverter, BoolToVisibilityConverter, PercentageConverter），对于任意有效输入值，Convert方法应该返回正确类型的输出，且ConvertBack方法（如果实现）应该能够反向转换
**Validates: Requirements 9.1, 9.2, 9.3**

**Property 7: 附加属性功能正确性**
*For any* 附加属性辅助类（CornerRadiusHelper, GlowEffectHelper），设置附加属性应该正确影响目标元素的视觉表现
**Validates: Requirements 9.4, 9.5**

**Property 8: 导航功能正确性**
*For any* 有效的视图名称，执行导航命令应该正确更新CurrentView属性为该视图名称
**Validates: Requirements 7.5**

**Property 9: 动画触发器存在性**
*For any* 需要动画的控件样式（Button, ListBox, ProgressBar等），其ControlTemplate应该包含动画定义或动画触发器
**Validates: Requirements 8.1, 8.2, 8.3, 8.4, 8.5**

### Example-Based Properties

以下属性基于特定示例或配置验证：

**Example 1: 项目结构验证**
验证LOLThemes.Wpf项目包含Themes、Controls、Converters、Helpers四个目录
**Validates: Requirements 1.2**

**Example 2: 目标框架验证**
验证LOLThemes.Wpf和LOLThemes.Wpf.Samples项目的TargetFramework为net8.0-windows
**Validates: Requirements 1.3, 6.2**

**Example 3: Generic.xaml存在性**
验证Themes/Generic.xaml文件存在且格式正确
**Validates: Requirements 1.4**

**Example 4: 项目依赖验证**
验证LOLThemes.Wpf项目不包含第三方UI库的PackageReference
**Validates: Requirements 1.5**

**Example 5: 项目引用验证**
验证LOLThemes.Wpf.Samples项目包含对LOLThemes.Wpf的ProjectReference
**Validates: Requirements 6.3**

**Example 6: 示例项目结构验证**
验证LOLThemes.Wpf.Samples项目包含Views、ViewModels、Assets目录
**Validates: Requirements 6.4**

**Example 7: 资源字典合并验证**
验证LOLThemes.Wpf.Samples的App.xaml中正确合并了LOLThemes.Wpf的资源字典
**Validates: Requirements 6.5**

**Example 8: 示例页面存在性**
验证为每个截图创建了对应的XAML页面文件（Screenshot1View.xaml到Screenshot6View.xaml）
**Validates: Requirements 7.1**

**Example 9: README内容验证**
验证README.md文件存在，且包含AI完成声明、项目简介、特性列表、使用方法、控件列表、项目结构、贡献指南和许可证信息
**Validates: Requirements 10.1, 10.2, 10.3, 10.4, 10.5, 10.6, 10.7**

## Error Handling

### 资源加载错误处理

1. **资源字典加载失败**
   - 场景：Generic.xaml或其他资源文件损坏或格式错误
   - 处理：在应用启动时捕获XamlParseException，记录详细错误信息，显示友好的错误对话框
   - 恢复：提供默认的最小化样式集，确保应用可以启动

2. **资源键不存在**
   - 场景：XAML中引用了不存在的资源键
   - 处理：WPF会抛出ResourceReferenceKeyNotFoundException
   - 预防：在设计时使用静态资源引用，在构建时验证所有资源键的存在性

### 控件实例化错误处理

1. **自定义控件初始化失败**
   - 场景：自定义控件的静态构造函数或依赖属性注册失败
   - 处理：捕获TypeInitializationException，记录错误，使用基类控件作为降级方案
   - 预防：确保所有依赖属性都有有效的默认值

2. **图片资源加载失败**
   - 场景：ChampionCard或SkillButton引用的图片文件不存在
   - 处理：使用占位符图片，在控件中显示"图片加载失败"提示
   - 实现：在ImageSource属性的PropertyChangedCallback中验证资源

### 转换器错误处理

1. **转换器输入类型错误**
   - 场景：传递给转换器的值类型不符合预期
   - 处理：在Convert方法中进行类型检查，返回合理的默认值
   - 示例：HexToColorConverter收到非字符串输入时返回Transparent

2. **转换器转换失败**
   - 场景：HexToColorConverter收到无效的十六进制颜色字符串
   - 处理：捕获FormatException，返回默认颜色，可选记录警告日志
   - 预防：在转换前进行格式验证

### 动画错误处理

1. **动画目标属性不存在**
   - 场景：Storyboard引用的属性路径在目标元素上不存在
   - 处理：WPF会静默失败，不播放动画
   - 预防：在样式中使用x:Name标记目标元素，确保属性路径正确

2. **动画性能问题**
   - 场景：大量元素同时播放复杂动画导致UI卡顿
   - 处理：使用RenderTransform而非直接修改布局属性，启用硬件加速
   - 优化：限制同时播放的动画数量，使用简化的动画曲线

### 示例应用错误处理

1. **视图导航失败**
   - 场景：导航到不存在的视图名称
   - 处理：在NavigateCommand中验证视图名称，显示错误提示，保持当前视图
   - 实现：维护有效视图名称的白名单

2. **ViewModel数据绑定错误**
   - 场景：XAML中绑定的属性在ViewModel中不存在
   - 处理：WPF会在输出窗口显示绑定错误，但不会崩溃
   - 预防：使用编译时绑定验证工具，确保所有绑定路径正确

## Testing Strategy

本项目采用双重测试策略，结合单元测试和基于属性的测试，以确保控件库的正确性和可靠性。

### 测试框架选择

- **单元测试框架**: xUnit.net
- **基于属性的测试框架**: FsCheck (可与xUnit集成)
- **WPF测试支持**: 使用Dispatcher和STAThread进行UI线程测试
- **断言库**: FluentAssertions (提供更好的可读性)

### 单元测试策略

单元测试专注于验证特定示例、边缘情况和集成点：

1. **资源加载测试**
   - 测试Generic.xaml能够成功加载
   - 测试所有子资源字典（Colors.xaml, Fonts.xaml等）能够正确合并
   - 测试特定资源键的存在性和值的正确性

2. **自定义控件实例化测试**
   - 测试每个自定义控件能够成功实例化
   - 测试依赖属性的默认值是否正确
   - 测试属性变化回调是否正确触发

3. **转换器边缘情况测试**
   - 测试转换器处理null输入
   - 测试转换器处理无效格式输入
   - 测试转换器处理边界值

4. **ViewModel逻辑测试**
   - 测试导航命令的执行
   - 测试属性变化通知
   - 测试命令的CanExecute逻辑

### 基于属性的测试策略

基于属性的测试验证通用属性在所有输入下都成立：

1. **配置要求**
   - 每个属性测试至少运行100次迭代
   - 使用FsCheck的Arbitrary生成器生成随机测试数据
   - 每个属性测试必须使用注释标记对应的设计文档属性

2. **属性测试标记格式**
   ```csharp
   /// <summary>
   /// Feature: lol-wpf-themes, Property 1: 控件样式完整性
   /// </summary>
   [Property(MaxTest = 100)]
   public Property ControlStyleCompleteness_ShouldExistForAllRequiredControls()
   {
       // 测试实现
   }
   ```

3. **核心属性测试**

   **Property 1: 控件样式完整性**
   - 生成器：生成所有必需的控件类型列表
   - 验证：对每个控件类型，检查资源字典中是否存在对应的Style
   - 验证：Style包含有效的ControlTemplate

   **Property 2: 控件视觉状态完整性**
   - 生成器：生成交互式控件类型列表
   - 验证：对每个控件，检查其ControlTemplate中是否包含必需的Trigger
   - 验证：Trigger的Property和Value设置正确

   **Property 3: 自定义控件属性完整性**
   - 生成器：生成自定义控件类型列表
   - 验证：对每个控件类型，使用反射检查是否定义了所有必需的DependencyProperty
   - 验证：每个DependencyProperty都有有效的默认值

   **Property 4: 主题颜色定义完整性**
   - 生成器：生成必需的颜色资源键列表
   - 验证：对每个键，检查资源字典中是否存在Color和对应的SolidColorBrush
   - 验证：颜色值符合十六进制格式规范

   **Property 5: 字体资源定义完整性**
   - 生成器：生成必需的字体资源键列表
   - 验证：对每个键，检查资源字典中是否存在对应的资源
   - 验证：资源类型正确（FontFamily, Double, FontWeight）

   **Property 6: 转换器功能正确性**
   - 生成器：为每个转换器生成随机有效输入值
   - 验证：Convert方法返回正确类型的值
   - 验证：如果实现了ConvertBack，则Convert(ConvertBack(x)) ≈ x（往返一致性）

   **Property 7: 附加属性功能正确性**
   - 生成器：生成随机的UIElement和附加属性值
   - 验证：设置附加属性后，GetValue返回相同的值
   - 验证：附加属性的PropertyChangedCallback正确执行

   **Property 8: 导航功能正确性**
   - 生成器：生成有效的视图名称列表
   - 验证：对任意视图名称，执行导航后CurrentView等于该名称
   - 验证：PropertyChanged事件正确触发

   **Property 9: 动画触发器存在性**
   - 生成器：生成需要动画的控件类型列表
   - 验证：对每个控件，检查其样式中是否包含Storyboard或动画相关的Trigger
   - 验证：动画的Duration和TargetProperty设置正确

4. **示例测试**

   示例测试验证特定配置和文件结构：

   ```csharp
   /// <summary>
   /// Feature: lol-wpf-themes, Example 1: 项目结构验证
   /// </summary>
   [Fact]
   public void ProjectStructure_ShouldContainRequiredDirectories()
   {
       var requiredDirs = new[] { "Themes", "Controls", "Converters", "Helpers" };
       foreach (var dir in requiredDirs)
       {
           Directory.Exists(Path.Combine(projectRoot, dir)).Should().BeTrue();
       }
   }
   
   /// <summary>
   /// Feature: lol-wpf-themes, Example 2: 目标框架验证
   /// </summary>
   [Fact]
   public void TargetFramework_ShouldBeNet80()
   {
       var csproj = XDocument.Load("LOLThemes.Wpf.csproj");
       var targetFramework = csproj.Descendants("TargetFramework").First().Value;
       targetFramework.Should().Be("net8.0-windows");
   }
   ```

5. **测试组织**

   ```
   tests/
   ├── LOLThemes.Wpf.Tests/
   │   ├── Properties/                    # 基于属性的测试
   │   │   ├── ControlStyleProperties.cs
   │   │   ├── CustomControlProperties.cs
   │   │   ├── ThemeResourceProperties.cs
   │   │   ├── ConverterProperties.cs
   │   │   └── AnimationProperties.cs
   │   ├── Examples/                      # 示例测试
   │   │   ├── ProjectStructureTests.cs
   │   │   ├── ResourceDictionaryTests.cs
   │   │   └── DependencyTests.cs
   │   ├── Units/                         # 单元测试
   │   │   ├── CustomControlTests.cs
   │   │   ├── ConverterTests.cs
   │   │   ├── HelperTests.cs
   │   │   └── ViewModelTests.cs
   │   └── Generators/                    # FsCheck生成器
   │       ├── ControlTypeGenerators.cs
   │       ├── ColorGenerators.cs
   │       └── ViewNameGenerators.cs
   └── LOLThemes.Wpf.Tests.csproj
   ```

6. **测试执行要求**

   - 所有测试必须在STA线程上运行（WPF要求）
   - 属性测试配置为至少100次迭代
   - 使用CI/CD管道自动运行所有测试
   - 测试覆盖率目标：核心逻辑80%以上

7. **测试与实现的关系**

   - 实现优先：先实现功能，再编写测试验证
   - 属性测试紧随实现：每完成一个主要组件，立即编写对应的属性测试
   - 测试作为文档：测试代码应清晰展示控件库的使用方法
   - 失败即修复：测试失败时，优先修复实现代码而非修改测试

### 测试工具和辅助类

1. **ResourceDictionaryLoader** - 辅助加载和验证资源字典
2. **ControlStyleInspector** - 检查控件样式的完整性
3. **DependencyPropertyHelper** - 反射辅助类，用于检查依赖属性
4. **XamlValidator** - 验证XAML文件格式和结构

## Implementation Notes

### 样式文件组织原则

**重要：每个基础控件的样式必须创建独立的XAML文件**

不要将所有控件样式放在一个文件中，而是为每个控件创建单独的样式文件：

```
Themes/
├── Generic.xaml                  # 主资源字典，合并所有子资源
├── Colors.xaml                   # 颜色定义
├── Fonts.xaml                    # 字体定义
├── Animations.xaml               # 动画定义
└── Styles/                       # 控件样式目录
    ├── ButtonStyles.xaml         # Button样式（独立文件）
    ├── TextBoxStyles.xaml        # TextBox样式（独立文件）
    ├── PasswordBoxStyles.xaml    # PasswordBox样式（独立文件）
    ├── ComboBoxStyles.xaml       # ComboBox样式（独立文件）
    ├── ListBoxStyles.xaml        # ListBox样式（独立文件）
    ├── ListViewStyles.xaml       # ListView样式（独立文件）
    ├── DataGridStyles.xaml       # DataGrid样式（独立文件）
    ├── CheckBoxStyles.xaml       # CheckBox样式（独立文件）
    ├── RadioButtonStyles.xaml    # RadioButton样式（独立文件）
    ├── ToggleButtonStyles.xaml   # ToggleButton样式（独立文件）
    ├── ProgressBarStyles.xaml    # ProgressBar样式（独立文件）
    ├── SliderStyles.xaml         # Slider样式（独立文件）
    ├── ScrollBarStyles.xaml      # ScrollBar样式（独立文件）
    ├── TabControlStyles.xaml     # TabControl样式（独立文件）
    ├── MenuStyles.xaml           # Menu和MenuItem样式（独立文件）
    ├── ContextMenuStyles.xaml    # ContextMenu样式（独立文件）
    ├── ToolTipStyles.xaml        # ToolTip样式（独立文件）
    ├── GroupBoxStyles.xaml       # GroupBox样式（独立文件）
    ├── ExpanderStyles.xaml       # Expander样式（独立文件）
    ├── LabelStyles.xaml          # Label样式（独立文件）
    ├── TextBlockStyles.xaml      # TextBlock样式（独立文件）
    ├── BorderStyles.xaml         # Border样式（独立文件）
    ├── WindowStyles.xaml         # Window样式（独立文件）
    ├── TreeViewStyles.xaml       # TreeView样式（独立文件）
    ├── StatusBarStyles.xaml      # StatusBar样式（独立文件）
    ├── SeparatorStyles.xaml      # Separator样式（独立文件）
    ├── ImageStyles.xaml          # Image样式（独立文件）
    ├── CalendarStyles.xaml       # Calendar样式（独立文件）
    ├── DatePickerStyles.xaml     # DatePicker样式（独立文件）
    └── RichTextBoxStyles.xaml    # RichTextBox样式（独立文件）
```

**Generic.xaml中的资源合并示例：**

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    
    <!-- 合并基础资源 -->
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="Colors.xaml"/>
        <ResourceDictionary Source="Fonts.xaml"/>
        <ResourceDictionary Source="Animations.xaml"/>
        
        <!-- 合并所有控件样式（每个控件一个文件） -->
        <ResourceDictionary Source="Styles/ButtonStyles.xaml"/>
        <ResourceDictionary Source="Styles/TextBoxStyles.xaml"/>
        <ResourceDictionary Source="Styles/PasswordBoxStyles.xaml"/>
        <ResourceDictionary Source="Styles/ComboBoxStyles.xaml"/>
        <ResourceDictionary Source="Styles/ListBoxStyles.xaml"/>
        <ResourceDictionary Source="Styles/ListViewStyles.xaml"/>
        <ResourceDictionary Source="Styles/DataGridStyles.xaml"/>
        <ResourceDictionary Source="Styles/CheckBoxStyles.xaml"/>
        <ResourceDictionary Source="Styles/RadioButtonStyles.xaml"/>
        <ResourceDictionary Source="Styles/ToggleButtonStyles.xaml"/>
        <ResourceDictionary Source="Styles/ProgressBarStyles.xaml"/>
        <ResourceDictionary Source="Styles/SliderStyles.xaml"/>
        <ResourceDictionary Source="Styles/ScrollBarStyles.xaml"/>
        <ResourceDictionary Source="Styles/TabControlStyles.xaml"/>
        <ResourceDictionary Source="Styles/MenuStyles.xaml"/>
        <ResourceDictionary Source="Styles/ContextMenuStyles.xaml"/>
        <ResourceDictionary Source="Styles/ToolTipStyles.xaml"/>
        <ResourceDictionary Source="Styles/GroupBoxStyles.xaml"/>
        <ResourceDictionary Source="Styles/ExpanderStyles.xaml"/>
        <ResourceDictionary Source="Styles/LabelStyles.xaml"/>
        <ResourceDictionary Source="Styles/TextBlockStyles.xaml"/>
        <ResourceDictionary Source="Styles/BorderStyles.xaml"/>
        <ResourceDictionary Source="Styles/WindowStyles.xaml"/>
        <ResourceDictionary Source="Styles/TreeViewStyles.xaml"/>
        <ResourceDictionary Source="Styles/StatusBarStyles.xaml"/>
        <ResourceDictionary Source="Styles/SeparatorStyles.xaml"/>
        <ResourceDictionary Source="Styles/ImageStyles.xaml"/>
        <ResourceDictionary Source="Styles/CalendarStyles.xaml"/>
        <ResourceDictionary Source="Styles/DatePickerStyles.xaml"/>
        <ResourceDictionary Source="Styles/RichTextBoxStyles.xaml"/>
    </ResourceDictionary.MergedDictionaries>
    
</ResourceDictionary>
```

**优点：**
- 代码组织清晰，易于维护
- 多人协作时减少冲突
- 按需加载特定控件样式
- 便于版本控制和代码审查
- 每个文件职责单一，符合单一职责原则

### 开发顺序建议

1. **阶段1：基础设施**
   - 创建项目结构
   - 设置主题系统（颜色、字体、动画资源）
   - 创建Generic.xaml并配置资源合并框架

2. **阶段2：基础控件样式**
   - 为每个控件创建独立的样式文件
   - 按优先级实现控件样式：Button → TextBox → ComboBox → ListBox → 其他
   - 每个控件样式包含完整的视觉状态
   - 实现交互动画
   - 在Generic.xaml中合并新创建的样式文件

3. **阶段3：自定义控件**
   - 实现5个核心自定义控件
   - 为每个控件创建对应的样式模板
   - 测试控件的依赖属性和事件

4. **阶段4：辅助工具**
   - 实现转换器
   - 实现附加属性辅助类
   - 创建实用工具类

5. **阶段5：示例应用**
   - 创建示例项目结构
   - 实现主窗口和导航系统
   - 创建6个截图还原页面
   - 创建控件展示页面

6. **阶段6：文档和测试**
   - 编写README文档
   - 实现单元测试
   - 实现基于属性的测试
   - 完善代码注释和XML文档

### 性能优化建议

1. **资源字典优化**
   - 使用StaticResource而非DynamicResource（除非需要运行时更改）
   - 避免在资源字典中定义过多未使用的资源
   - 考虑按需加载大型资源字典

2. **动画优化**
   - 使用RenderTransform进行变换动画（GPU加速）
   - 避免动画修改布局属性（Width, Height, Margin）
   - 使用Storyboard.SetSpeedRatio控制动画速度而非修改Duration

3. **控件模板优化**
   - 减少模板中的嵌套层级
   - 使用ContentPresenter而非重复定义内容
   - 避免在模板中使用复杂的数据绑定

4. **自定义控件优化**
   - 在依赖属性变化时避免不必要的重绘
   - 使用CoerceValueCallback限制属性值范围
   - 缓存计算结果，避免重复计算

### 可扩展性设计

1. **主题切换支持**
   - 虽然当前只实现LOL主题，但设计应支持未来添加其他主题
   - 考虑使用ThemeManager类管理主题切换
   - 颜色和字体资源应易于替换

2. **自定义控件扩展**
   - 提供基类或接口供用户创建自己的LOL风格控件
   - 文档化控件创建的最佳实践
   - 提供控件模板的示例和指南

3. **本地化支持**
   - 虽然当前不实现多语言，但字符串资源应集中管理
   - 考虑未来添加资源文件支持

### 已知限制

1. **WPF平台限制**
   - 仅支持Windows平台
   - 需要.NET 8.0运行时
   - 不支持跨平台部署

2. **视觉还原限制**
   - 某些LOL游戏中的特效（粒子效果、复杂3D效果）无法在WPF中完美实现
   - 动画性能受限于WPF渲染引擎
   - 某些字体可能因版权问题无法包含

3. **功能范围限制**
   - 当前版本专注于视觉样式，不包含游戏逻辑
   - 不包含网络通信或数据持久化功能
   - 示例应用仅用于展示，不是完整的应用程序

## Future Enhancements

1. **额外主题**
   - 添加暗色/亮色主题变体
   - 支持用户自定义颜色方案

2. **更多自定义控件**
   - 排行榜控件
   - 聊天消息控件
   - 装备栏控件
   - 小地图控件

3. **动画增强**
   - 添加更多过渡动画
   - 实现页面切换动画
   - 添加加载动画

4. **工具和生成器**
   - 提供主题编辑器工具
   - 提供控件预览工具
   - 提供样式生成器

5. **文档和示例**
   - 添加交互式文档网站
   - 提供更多使用示例
   - 创建视频教程

6. **社区功能**
   - 建立控件库市场
   - 支持社区贡献的控件
   - 提供控件评分和评论系统
