using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task Download_For_VS2019()
    {
        const string url = "https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator";
        const string output = "vs2019.vsix";
        await Download(url, output);
    }

    [Test]
    public async Task Download_For_VS2022()
    {
        const string url = "https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator2022";
        const string output = "vs2022.vsix";
        await Download(url, output);
    }

    private async Task Download(string url, string output)
    {
        var browser = await Playwright.Firefox.LaunchAsync();
        var context = await browser.NewContextAsync(new BrowserNewContextOptions { AcceptDownloads = true });
        var page = await context.NewPageAsync();
        await page.GotoAsync(url);
        var waitForDownloadTask = page.WaitForDownloadAsync();
        var button = page.Locator("button:has-text('Download')");
        await button.ClickAsync();
        var download = await waitForDownloadTask;
        await download.SaveAsAsync(output);
        Assert.True(File.Exists(output));
    }
}