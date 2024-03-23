#read levels
#extract for each the letters
#execute solver
#update line

<#
param(
[string]$solverPath,
[string]$lang
)

if ($lang==null) {
	write-host "Please provide the lang param (en|gr|es)"
return
}

if ($solverPath==null) {
	Write-Host "Please provide the solverPath param (path to solver CLI executable)"
	return
}
#>

$OutputEncoding = [Console]::InputEncoding = [Console]::OutputEncoding = New-Object System.Text.UTF8Encoding

$solverPath="c:\Users\spara\source\repos\LexeisSolver\SolverCLI\bin\Debug\SolverCLI.exe"
$lang="gr"
$levelsPath="c:\Users\spara\Desktop\pod\gr\3\levels.txt"
$outPath="c:\Users\spara\Desktop\pod\gr\3\levels.txt1"
$output="var daily_levels_$($lang)=[]`n"
$lines=Get-Content $levelsPath -Encoding UTF8 



$pattern = '"BoardSetup":"([^"]+)"'
Write-Host "Loading..."
$counter=0
foreach ($row in $lines) {    
    $pos=$row.IndexOf("{")
    if ($pos -ne -1) {
        $counter ++    
        $json=$row.Substring($pos,$row.Length-$pos-2)    
        $obj=$json | ConvertFrom-Json
        $wordCount=$obj.Words.Count
        $points=$obj.Points
        Write-Host("Level: $counter Words: $wordCount Points:$points")
        if ($obj.BoardSetup -ne $null) {
            #if ($obj.Words.Contains("ΗΧΟ")) {
			    $newWords = & $solverPath -l $lang -s $obj.BoardSetup -v
                #$newWords = c:\Users\parasp\source\repos\LexeisSolver\SolverCLI\bin\Debug\SolverCLI.exe -l gr -s $obj.BoardSetup -v
                $obj.Words=$newWords
                Write-Host("New words count: $($newWords.Length)")
            #}
            $outJson=$obj | ConvertTo-Json -Compress
            $modifiedRow = "levels_$($lang).push($outJson);`n"
            $output += "$modifiedRow"
            write-output $output | out-file  -encoding utf8 $outPath 
        }
    }
}



#$output = c:\Users\parasp\source\repos\LexeisSolver\SolverCLI\bin\Debug\SolverCLI.exe -l gr -s "ΑΕΕΚΣΜΡΟΙΟΑΓΜΟΗΩ" -v 
write-output $output | out-file  -encoding utf8 $outPath 

# Start the process and capture the output
#$output = Start-Process -FilePath $toolPath -ArgumentList $arguments 
#-NoNewWindow -PassThru -Wait 
#-RedirectStandardOutput (New-TemporaryFile).FullName -ErrorAction Stop


# Read the output file as an array of strings
#$outputStrings = Get-Content $output.Path

# Output the list of strings
#$outputStrings