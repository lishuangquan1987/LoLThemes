## 改造目标
将所有控件showcase文件中的示例代码，从使用单个XamlDisplay包裹多个控件，改为每个控件示例单独使用XamlDisplay包裹，以便单独展示每个示例的代码实现。

## 需要改造的文件列表
根据查看的结果，以下文件需要进行改造：

1. **CheckBoxShowcaseView.xaml** - 复选框展示
2. **TextBoxShowcaseView.xaml** - 文本框展示
3. **ComboBoxShowcaseView.xaml** - 下拉框展示
4. **ListBoxShowcaseView.xaml** - 列表框展示
5. **ListViewShowcaseView.xaml** - 列表视图展示
6. **ProgressBarShowcaseView.xaml** - 进度条展示
7. **SliderShowcaseView.xaml** - 滑块展示
8. **MenuShowcaseView.xaml** - 菜单展示
9. **TreeViewShowcaseView.xaml** - 树形视图展示
10. **TabControlShowcaseView.xaml** - 标签页展示
11. **ExpanderShowcaseView.xaml** - 展开器展示
12. **GroupBoxShowcaseView.xaml** - 分组框展示
13. **DatePickerShowcaseView.xaml** - 日期选择器展示
14. **CalendarShowcaseView.xaml** - 日历展示
15. **DataGridShowcaseView.xaml** - 数据表格展示
16. **PasswordBoxShowcaseView.xaml** - 密码框展示
17. **RichTextBoxShowcaseView.xaml** - 富文本框展示
18. **ContextMenuShowcaseView.xaml** - 上下文菜单展示
19. **ToolTipShowcaseView.xaml** - 工具提示展示
20. **StatusBarShowcaseView.xaml** - 状态栏展示
21. **ToggleButtonShowcaseView.xaml** - 切换按钮展示
22. **WindowShowcaseView.xaml** - 窗口展示
23. **GlowButtonShowcaseView.xaml** - 发光按钮展示
24. **HexagonButtonShowcaseView.xaml** - 六边形按钮展示
25. **SkillButtonShowcaseView.xaml** - 技能按钮展示
26. **ChampionCardShowcaseView.xaml** - 英雄卡片展示
27. **RankBadgeShowcaseView.xaml** - 段位徽章展示
28. **StatBarShowcaseView.xaml** - 状态条展示
29. **CurrencyDisplayShowcaseView.xaml** - 货币显示展示

## 改造模式
参考ButtonShowcaseView.xaml和已完成的RadioButtonShowcaseView.xaml，改造模式如下：

**改造前：**
```xml
<smtx:XamlDisplay UniqueKey="CheckBoxBasic" Margin="0,0,0,0">
    <StackPanel>
        <CheckBox Content="选项 1" Margin="0,0,0,10"/>
        <CheckBox Content="选项 2" IsChecked="True" Margin="0,0,0,10"/>
        <CheckBox Content="选项 3" Margin="0,0,0,10"/>
    </StackPanel>
</smtx:XamlDisplay>
```

**改造后：**
```xml
<StackPanel>
    <smtx:XamlDisplay Margin="0,0,0,10" UniqueKey="CheckBoxOption1">
        <CheckBox Content="选项 1"/>
    </smtx:XamlDisplay>

    <smtx:XamlDisplay Margin="0,0,0,10" UniqueKey="CheckBoxOption2">
        <CheckBox Content="选项 2" IsChecked="True"/>
    </smtx:XamlDisplay>

    <smtx:XamlDisplay Margin="0,0,0,0" UniqueKey="CheckBoxOption3">
        <CheckBox Content="选项 3"/>
    </smtx:XamlDisplay>
</StackPanel>
```

## 改造原则
1. 每个控件示例（如每个CheckBox、每个RadioButton）都单独使用XamlDisplay包裹
2. 为每个XamlDisplay设置唯一的UniqueKey，命名格式为：控件类型+描述，如"CheckBoxOption1"、"CheckBoxChecked"等
3. 保持原有的布局和样式不变，仅调整XamlDisplay的包裹方式
4. 对于包含多个子控件的复杂示例（如带图标的复选框），将整个复杂控件作为一个示例包裹
5. 对于分组容器（如Border包裹的组），将Border作为整体包裹，或者将组内的每个控件单独包裹（根据具体情况）

## 执行方式
按照用户要求，不使用批量替换，逐个文件进行手动修改，确保每个修改都准确无误。