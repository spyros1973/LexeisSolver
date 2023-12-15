param([string]$path)


$myLevels = Get-Content $path\index.txt -Encoding UTF8 -Raw | ConvertFrom-Json | Sort-Object -Property Filename -Descending
$sortedLevels = $myLevels | Sort-Object -Property Words -Descending

Write-Host "Reading files"
Write-Host "$($myLevels.length) files read"

Write-Host "Outputting js"
$result="var daily_levels_es=[];"
$i=0
foreach ($f in $sortedLevels ) {
    #Write-Host "$($f.Filename) has $($f.Words) words and $($f.Points) points"
    $file=Get-Content -Path $path\$($f.Filename) -Encoding UTF8
    $result += "daily_levels_es.push($($file));`r`n"
}

# $files = Get-Childitem -Path $path -Filter *.json -File -Recurse -ErrorAction SilentlyContinue
# $result = foreach($file in $files)
# {
# Write-Host $file
# }


Write-Host "$path\levels.txt written on disk"
$result | Out-File $path\levels.txt -Encoding utf8