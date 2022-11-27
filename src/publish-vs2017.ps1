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

$VsixPublisher = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\VSSDK\VisualStudioIntegration\Tools\Bin\VsixPublisher.exe"

& $VsixPublisher login `
    -personalAccessToken $personalAccessToken `
    -publisherName ChristianResmaHelle

& $VsixPublisher publish `
    -payload "ApiClientCodeGenerator-VS2017-$version.vsix" `
    -publishManifest publish-manifest-vs2017.json `
    -ignoreWarnings 'VSIXValidatorWarning01,VSIXValidatorWarning02'