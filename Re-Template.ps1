param(
[string]$currentName,
[string]$newName
)

function Get-FileEncoding
{
    [CmdletBinding()] Param (
     [Parameter(Mandatory = $True, ValueFromPipelineByPropertyName = $True)] [string]$Path
    )
 
    [byte[]]$byte = Get-Content -Encoding byte -ReadCount 4 -TotalCount 4 -Path $Path
 
    If ( $byte[0] -eq 0xef -and $byte[1] -eq 0xbb -and $byte[2] -eq 0xbf )
    { return [System.Text.Encoding]::UTF8 }
    ElseIf ($byte[0] -eq 0xfe -and $byte[1] -eq 0xff)
    { return [System.Text.Encoding]::Unicode }
    ElseIf ($byte[0] -eq 0 -and $byte[1] -eq 0 -and $byte[2] -eq 0xfe -and $byte[3] -eq 0xff)
    { return [System.Text.Encoding]::UTF32 }
    ElseIf ($byte[0] -eq 0x2b -and $byte[1] -eq 0x2f -and $byte[2] -eq 0x76)
    { return [System.Text.Encoding]::UTF7}
    else
    { return [System.Text.Encoding]::ASCII }
}

If ($newName -eq "" -and $currentName -ne "") {
	# If only one parameter passed in treat first parameter as new name
	$newName = $currentName
	$currentName = "App.Template"
}

If ($newName -eq "") {
	$newName = Read-Host "Enter new name"
	$currentName = "App.Template"
}

If ($currentName -eq $newName) {
	throw "Must use new name"
}

Write-Host "Clonar Solucao Template de $currentName para $newName"

Write-Host "Copiando $(Get-Location) para $newName"

Copy-Item -Recurse '.\' "..\$newName"

Set-Location "..\$newName"

# Remove any build/ignored files
git reset --hard
git clean -f -d -x
git remote rm origin
	
Set-Content "README.md" "## $newName"

$gitPath = Join-Path (Resolve-Path (Get-Location)) ".git\*"

$hasMoreDirectories = $true

While ($hasMoreDirectories) {
	$hasMoreDirectories = $false
	Get-ChildItem ".\" -Recurse | ?{ $_.PSIsContainer -and $_.FullName -notlike $gitPath } | Where-Object Name -Match $currentName | Sort-Object FullName.Length | % {
		$hasMoreDirectories = $true

		# May have already been moved
		If (Test-Path $_.FullName) {
			
			$destination = Join-Path $_.Parent.FullName ($_.Name -replace $currentName, $newName)

			Write-Host "Copiando diretorio $($_.FullName) para $destination"
		
			If (Test-Path $destination) {			
				Remove-Item $destination -Recurse -Force
			}

			[System.IO.Directory]::Move($_.FullName, $destination)
		}	
	}
}

Get-ChildItem ".\" -Recurse -Exclude 'Re-Template.ps1', 'packages', 'bin' | ?{ !$_.PSIsContainer -and $_.FullName -notlike $gitPath} | % {
	
	Set-ItemProperty $_.FullName -name IsReadOnly -value $false

	$encoding = Get-FileEncoding $_.FullName
	
	$content = $encoding.GetString(([System.IO.File]::ReadAllBytes($_.FullName))) 	

	If ($content -match $currentName) {		
		Write-Host "Reescrevendo arquivo $($_.FullName)"

		$content = $content -replace $currentName, $newName	
		$bytes = $encoding.GetBytes($content)	
		[System.IO.File]::WriteAllBytes($_.FullName, $bytes)
	}
			
	$destination =Join-Path $_.Directory.FullName $_.Name.Replace($currentName, $newName)
	
	If ($_.FullName -ne $destination) {		
		Write-Host "Copiando arquivo $($_.FullName) para $destination"	

		$_.MoveTo($destination)
	}	
}