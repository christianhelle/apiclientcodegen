[xml]$latestRoot = Get-Content root.mrep
$resultRoot = '<?xml version="1.0" encoding="utf-8"?>'
$resultRoot += '<Repository xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">'
$resultRoot += (Invoke-RestMethod 'https://christianhelle.com/vsmac/root.mrep').Repository.InnerXml + $latestRoot.Repository.InnerXml
$resultRoot += '</Repository>'
([xml]$resultRoot).Save("$pwd\root.mrep")

$main = '<?xml version="1.0" encoding="utf-8"?>'
$main += '<Repository xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">'
$main += '<Repository>'
$main += '<Url>root.mrep</Url>'
$main += '<LastModified>'
$main += (Get-Date).ToString('o')
$main += '</LastModified>'
$main += '</Repository>'
$main += '</Repository>'
([xml]$main).Save("$pwd\main.mrep")