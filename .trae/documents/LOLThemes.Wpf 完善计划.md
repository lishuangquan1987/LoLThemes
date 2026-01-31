# LOLThemes.Wpf 完善计划（最终版）

## 1. 树节点图标优化（Material.Icons）

**目标**：为树节点添加Material.Icons图标支持

**实现方案**：

* 修改 Samples 项目中的 `TreeItemTemplate`，添加图标显示区域

* 集成 `Material.Icons.WPF` NuGet包到 Samples 项目

* 在 TreeItemTemplate 中使用 `MaterialIcon` 控件显示图标

* 支持通过数据绑定设置图标

**涉及文件**：

* `e:/Project2025/LoLThemes/src/LOLThemes.Wpf.Samples/Views/ControlShowcaseView.xaml`（或包含TreeItemTemplate的文件）

## 2. ShowMeTheXAML 优化

**目标**：优化 ShowMeTheXAML 控件，达到 MaterialDesignInXamlToolkit 效果

**实现方案**：

* 改进代码高亮显示，使用 AvalonEdit 实现语法高亮

* 添加复制按钮和更好的视觉反馈

* 优化折叠/展开效果

* 改进代码格式化算法

* 确保与Dark主题适配

**涉及文件**：

* `e:/Project2025/LoLThemes/src/LOLThemes.Wpf.Samples/Styles/XamlDisplayStyles.xaml`

* 可能需要更新 ShowMeTheXAML 相关代码

## 3. 新Dark配色方案开发

**目标**：创建一个新的Dark配色方案，与现有方案兼容，支持运行时切换

**实现方案**：

* 创建新的配色文件 `Colors.Dark.V2.xaml`

* 保持与现有配色方案相同的资源名称，确保兼容性

* 调整颜色值，创建不同风格的Dark主题（如更鲜艳的游戏风格或更现代的设计）

* 实现主题切换功能，支持在运行时动态切换

**涉及文件**：

* `e:/Project2025/LoLThemes/src/LOLThemes.Wpf/Themes/Colors.xaml`（作为占位符）

* 新创建 `Colors.Dark.V2.xaml`

* ThemeManager 相关代码

## 4. 英雄联盟主页面实现

**目标**：实现如截图所示的英雄联盟主页面

**实现方案**：

* 创建新的主页面视图和视图模型

* 实现顶部导航栏、侧边栏菜单、主内容区域和右侧好友列表

* 使用现有控件库构建界面元素

* 添加图片轮播、按钮、进度条等交互元素

* 实现响应式布局，适配不同屏幕尺寸

**涉及文件**：

* 新创建主页面 XAML 文件

* 新创建主页面 ViewModel 文件

* 可能需要添加相关资源和图片

## 5. 已有控件样式优化

**目标**：优化已有控件样式，包括滑块、进度条等

**实现方案**：

* 优化 `Slider` 控件样式，使其更符合英雄联盟主题

* 改进 `ProgressBar` 控件样式

* 优化其他已有控件的视觉效果和交互体验

* 确保所有控件样式统一，符合主题风格

**涉及文件**：

* `e:/Project2025/LoLThemes/src/LOLThemes.Wpf/Themes/Styles/SliderStyles.xaml`

* `e:/Project2025/LoLThemes/src/LOLThemes.Wpf/Themes/Styles/ProgressBarStyles.xaml`

* 其他相关控件样式文件

## 6. 日期和日历控件优化

**目标**：优化 DatePicker 和 Calendar 控件的样式和交互体验

**实现方案**：

* 优化 `DatePicker` 控件样式，使其与Dark主题适配

* 改进 `Calendar` 控件样式，添加英雄联盟主题风格

* 确保与其他控件风格统一

* 优化日期选择体验

**涉及文件**：

* 新创建或修改 `DatePickerStyles.xaml`

* 新创建或修改 `CalendarStyles.xaml`

## 7. 其他优化建议

**目标**：列出其他可能的优化点，供用户审核

**优化点**：

1. **性能优化**：优化复杂控件的渲染性能
2. **文档完善**：更新控件使用文档和示例
3. **测试用例**：添加单元测试和UI测试
4. **代码重构**：优化现有代码结构，提高可维护性

## 实施顺序

1. 首先实现树节点图标优化（修改Samples中的TreeItemTemplate）
2. 然后优化 ShowMeTheXAML 控件
3. 接着创建新的Dark配色方案
4. 优化已有控件样式（滑块、进度条等）
5. 优化日期和日历控件样式
6. 实现英雄联盟主页面
7. 最后进行其他优化

## 预期效果

* 树节点将显示 Material.Icons 图标，增强视觉效果

* ShowMeTheXAML 控件将具有更好的代码显示和交互体验

* 用户可以在运行时切换不同的Dark配色方案

* 已有控件样式将更加统一和美观

* 日期和日历控件将具有更好的视觉效果和交互体验

* 实现完整的英雄联盟主页面，接近截图效果

* 整体控件库更加完善，具有更好的视觉效果和用户体验

