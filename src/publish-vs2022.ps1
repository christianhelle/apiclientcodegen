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

$VSIXFileName = (Get-ChildItem -Path . -Filter "ApiClientCodeGenerator-VS2022-$version.vsix" | Select-Object -First 1).Name
$VsixPublisher = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\VSSDK\VisualStudioIntegration\Tools\Bin\VsixPublisher.exe"

& $VsixPublisher login `
    -personalAccessToken $personalAccessToken `
    -publisherName ChristianResmaHelle

& $VsixPublisher publish `
    -payload $VSIXFileName `
    -publishManifest publish-manifest-vs2022.json `
    -ignoreWarnings 'VSIXValidatorWarning01,VSIXValidatorWarning02'