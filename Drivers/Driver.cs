using Microsoft.Playwright;

namespace GLMSys.SpecFlow.Playwright.Drivers
{
    public class Driver : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;
        private IBrowserContext? _browserContext;

        public IPage Page => _page.Result;

        public Driver()
        {
            _page = InitializePlaywright();
        }

        public async Task<IPage> InitializePlaywright()
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = Utilities.Parameters.Headless,
            });

            _browserContext = await _browser.NewContextAsync();

            ////set default timeout
            _browserContext.SetDefaultTimeout((float)TimeSpan.FromMinutes(2).TotalMilliseconds);

            ////trace option
            //await _browserContext.Tracing.StartAsync(new()
            //{
            //    Screenshots = true,
            //    Snapshots = true
            //});

            return await _browserContext.NewPageAsync();
        }

        public void Dispose()
        {
            _browser?.CloseAsync();

            //var task = Task.Run(() => _browserContext?.Tracing.StopAsync(new TracingStopOptions() { Path = "testtrace.zip" }));
        }
    }
}