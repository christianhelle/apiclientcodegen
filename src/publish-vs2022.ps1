$VisualStudioVersion = "15.0";
$VSINSTALLDIR =  $(Get-ItemProperty "Registry::HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\VisualStudio\SxS\VS7").$VisualStudioVersion;
$VSIXPublisherPath = $VSINSTALLDIR + "VSSDK\VisualStudioIntegration\Tools\Bin\"
$env:Path += ";$VSIXPublisherPath"

$DropFolder = "$(System.DefaultWorkingDirectory)/CI Builds/drop"
$VSIXFileName = $DropFolder + "/" + (Get-ChildItem -Path $DropFolder -Filter "ApiClientCodeGenerator-VS2022-*.vsix" | Select-Object -First 1).Name
$ManifestFile = $DropFolder + "/publish-manifest.json"

VsixPublisher.exe login `
    -personalAccessToken '$(PersonalAccessToken)' `
    -publisherName 'ChristianResmaHelle'

VsixPublisher.exe publish `
    -payload $VSIXFileName `
    -publishManifest $ManifestFile `
    -ignoreWarnings 'VSIXValidatorWarning01,VSIXValidatorWarning02'