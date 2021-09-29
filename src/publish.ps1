$VisualStudioVersion = "15.0";
$VSINSTALLDIR =  $(Get-ItemProperty "Registry::HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\VisualStudio\SxS\VS7").$VisualStudioVersion;
$VSIXPublisherPath = $VSINSTALLDIR + "VSSDK\VisualStudioIntegration\Tools\Bin\"
$DropFolder = "$(System.DefaultWorkingDirectory)/CI Build/drop"
$VSIXFileName = $DropFolder + "/" + (Get-ChildItem -Path $DropFolder -Filter "ApiClientCodeGenerator-*.vsix" | Select-Object -First 1).Name
$env:Path += ";$VSIXPublisherPath"

VsixPublisher.exe login `
    -personalAccessToken '$(PersonalAccessToken)' `
    -publisherName 'ChristianResmaHelle'

VsixPublisher.exe publish `
    -payload $VSIXFileName `
    -publishManifest $DropFolder/publish-manifest.json `
    -ignoreWarnings 'VSIXValidatorWarning01,VSIXValidatorWarning02'