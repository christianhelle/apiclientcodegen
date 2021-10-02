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
$VSIXFileName = (Get-ChildItem -Path . -Filter "ApiClientCodeGenerator-$version.vsix" | Select-Object -First 1).Name
$env:Path += ";$VSIXPublisherPath"

VsixPublisher.exe login `
    -personalAccessToken $personalAccessToken `
    -publisherName ChristianResmaHelle

VsixPublisher.exe publish `
    -payload $VSIXFileName `
    -publishManifest publish-manifest.json `
    -ignoreWarnings 'VSIXValidatorWarning01,VSIXValidatorWarning02'