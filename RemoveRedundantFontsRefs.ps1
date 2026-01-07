# 批量移除样式文件中冗余的Fonts.xaml引用
$stylesPath = "e:\Project2025\LoLThemes\src\LOLThemes.Wpf\Themes\Styles"

# 获取所有样式文件
$styleFiles = Get-ChildItem -Path $stylesPath -Filter "*.xaml" -Recurse

foreach ($file in $styleFiles) {
    Write-Host "Processing file: $($file.FullName)"
    
    # 读取文件内容
    $content = Get-Content -Path $file.FullName -Raw
    
    # 检查文件中是否包含Fonts.xaml引用
    if ($content -match "Fonts.xaml") {
        Write-Host "  Found Fonts.xaml reference in $($file.Name)"
        
        # 使用Regex移除Fonts.xaml引用行
        $newContent = $content -replace '<ResourceDictionary Source="pack://application:,,,/LOLThemes.Wpf;component/Themes/Fonts.xaml"/>\r?\n', ''
        
        # 检查是否还有其他资源引用
        if ($newContent -notmatch '<ResourceDictionary Source=') {
            # 如果没有其他资源引用，移除整个ResourceDictionary.MergedDictionaries块
            $newContent = $newContent -replace '<ResourceDictionary.MergedDictionaries>\s*</ResourceDictionary.MergedDictionaries>', ''
            Write-Host "  Removed empty ResourceDictionary.MergedDictionaries block"
        }
        
        # 写入修改后的内容
        Set-Content -Path $file.FullName -Value $newContent
        Write-Host "  Updated file: $($file.Name)"
    }
}

Write-Host "\nAll files processed."