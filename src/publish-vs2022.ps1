param (
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    [string]
    $personalAccessToken,
    
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    [string]
    $version
)

$VisualStudioVersion = "15.0";
$VSINSTALLDIR =  $(Get-ItemProperty "Registry::HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\VisualStudio\SxS\VS7").$VisualStudioVersion;
$VSIXPublisherPath = $VSINSTALLDIR + "VSSDK\VisualStudioIntegration\Tools\Bin\"
$VSIXFileName = (Get-ChildItem -Path . -Filter "ApiClientCodeGenerator-VS2022-$version.vsix" | Select-Object -First 1).Name
$env:GITHUB_PATH += ";$VSIXPublisherPath"

Write-Host $VSIXPublisherPath
$VsixPublisher = $VSIXPublisherPath + "VsixPublisher.exe"

& $VsixPublisher login `
    -personalAccessToken $personalAccessToken `
    -publisherName ChristianResmaHelle

& $VsixPublisher publish `
    -payload $VSIXFileName `
    -publishManifest publish-manifest-vs2022.json `
    -ignoreWarnings 'VSIXValidatorWarning01,VSIXValidatorWarning02'