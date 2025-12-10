param()
Set-Location -Path "C:\Users\sebas\source\repos\LeadGenerator"
$patterns = @("*.cs","*.csproj","*.cshtml","*.json","*.config","*.props","*.targets","*.md","*.yml","*.yaml","*.sln")
Get-ChildItem -Recurse -File -Include $patterns | Where-Object { $_.FullName -notmatch "\\obj\\" -and $_.FullName -notmatch "\\bin\\" } | ForEach-Object {
    $content = Get-Content -Raw -Path $_.FullName
    $updated = $content -replace 'BitMouse\.LeadGenerator', 'GLeadGenerator'
    if ($updated -ne $content) {
        Set-Content -Path $_.FullName -Value $updated -Encoding UTF8
    }
}
