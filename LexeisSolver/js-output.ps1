param(
[string]$path,
[string]$lang
)


if ($path -eq "") {
Write-Host "Please set the path of the game levels"
return
}

if ($lang -eq "") {
Write-Host "Please set the language (gr|es|en)"
return
}


$levelFiles=Get-ChildItem -Path $path\*.json
Write-Host "Reading $($levelFiles.length) files"

$indexContents=@()
foreach ($lf in $levelFiles) {
    $lvl=Get-Content $lf -Encoding utf8 -raw | convertfrom-json
    $dict = @{}
    $dict.add("Filename",$lf.name)
    $dict.add("Points",$lvl.Points)
    $dict.add("Words",$lvl.Words.Length)

    $existing = ($indexContents  | where {  $_.Points -eq $lvl.Points  -and  $_.Words -eq $lvl.Words.Length })

    if ( ($existing.Length -eq 0) -and ($indexContents.Length -lt 366))  {
        $indexContents += $dict
    }
}

$indexContents | ConvertTo-Json  | Out-File $path\index.txt -Encoding utf8 

$myLevels = Get-Content $path\index.txt -Encoding UTF8 -Raw | ConvertFrom-Json | Sort-Object -Property Filename -Descending
#$sortedLevels = $myLevels | Sort-Object -Property Words -Descending
$sortedLevels = $myLevels

Write-Host "Reading unique indexed files"
Write-Host "$($myLevels.length) files read"

Write-Host "Outputting js"
$result="var daily_levels_$($lang)=[];"
$i=0
foreach ($f in $sortedLevels ) {
    #Write-Host "$($f.Filename) has $($f.Words) words and $($f.Points) points"
    $file=Get-Content -Path $path\$($f.Filename) -Encoding UTF8
    $result += "daily_levels_$($lang).push($($file));`r`n"
}

# $files = Get-Childitem -Path $path -Filter *.json -File -Recurse -ErrorAction SilentlyContinue
# $result = foreach($file in $files)
# {
# Write-Host $file
# }


Write-Host "$path\levels.txt written on disk"
$result | Out-File $path\levels.txt -Encoding utf8