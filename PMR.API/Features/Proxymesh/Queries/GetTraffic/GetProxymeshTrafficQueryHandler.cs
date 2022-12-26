using MediatR;
using PuppeteerSharp;
using System.Net.Http.Headers;

namespace PMR.API.Features.Proxymesh.Queries.GetTraffic;
public class GetProxymeshTrafficQueryHandler : IRequestHandler<GetProxymeshTrafficQuery, string> {
    private readonly IConfiguration conf;
    private readonly ILogger<GetProxymeshTrafficQueryHandler> logger;
    private readonly string webHookURL = "https://localitymedia.webhook.office.com/webhookb2/2c0e2458-c4b2-448d-bb55-4b8c1f86feab@dca4b479-dd94-4d7b-bddb-8594c6160459/IncomingWebhook/b4ac0b4fe1a6421c8bfca67ce4706881/76e6b7af-882d-4e8a-aa8c-52a4c6f98a4a";

    public GetProxymeshTrafficQueryHandler(IConfiguration conf, 
        ILogger<GetProxymeshTrafficQueryHandler> logger) {
        this.conf = conf;
        this.logger = logger;
    }

    public async Task<string> Handle(GetProxymeshTrafficQuery request, CancellationToken cancellationToken) {
        using var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();

        var url = "https://proxymesh.com/auth/login/";

        var launchOptions = new LaunchOptions() { Headless = true };

        try {
            using (var browser = await Puppeteer.LaunchAsync(launchOptions))
            using (var page = await browser.NewPageAsync())
            {
                page.DefaultNavigationTimeout = 0;
                logger.LogInformation("Going to url");
                await page.GoToAsync(url);

                logger.LogInformation("Typing credentials");
                await page.TypeAsync("#id_username", conf.GetValue<string>("ProxyMesh:user"));
                await page.TypeAsync("#id_password", conf.GetValue<string>("ProxyMesh:pass"));
                await page.Keyboard.PressAsync("Enter");

                logger.LogInformation("Redirecting");
                await page.WaitForNavigationAsync();
                await page.WaitForXPathAsync("//td[@data-stathat='8xna']");

                var elementHandler = (await page.XPathAsync("//td[@data-stathat='8xna']"))[0];
                var traffic = await page.EvaluateFunctionAsync<string>("e => e.textContent", elementHandler);
                logger.LogInformation($"Traffic: {traffic}");

                string json = @"{
                    ""@type"": ""MessageCard"",
                    ""@context"": ""http://schema.org/extensions"",
                    ""themeColor"": ""0076D7"",
                    ""summary"": ""ProxyMesh traffic report"",
                    ""sections"": [{
                        ""activityTitle"": ""ProxyMesh traffic report"",
                        ""facts"": [{
                            ""name"": ""Traffic:"",
                            ""value"": """ + traffic + @"""
                        },
                        {
                            ""name"": ""Date:"",
                            ""value"": """ + DateTime.Now + @"""
                        }]
                    }]
                }";

                logger.LogInformation("Sending data to MS Teams");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(webHookURL, content);

                logger.LogInformation("Done!");
                return traffic;
            }
        }
        catch (Exception e) {
            logger.LogError($"Error: {e.Message}");
            return e.Message;
        }
    }
}