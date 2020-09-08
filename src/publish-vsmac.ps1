[xml]$latestRoot = Get-Content root.mrep
$resultRoot = '<?xml version="1.0" encoding="utf-8"?>'
$resultRoot += '<Repository xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">'
$resultRoot += (Invoke-RestMethod 'https://christianhelle.com/vsmac/root.mrep').Repository.InnerXml + $latestRoot.Repository.InnerXml
$resultRoot += '</Repository>'
([xml]$resultRoot).Save("$pwd\root.mrep")

$main = Invoke-RestMethod 'https://christianhelle.com/vsmac/main.mrep'
$main.Repository.Repository.LastModified = (Get-Date).ToString('o')
([xml]$main).Save("$pwd\main.mrep")