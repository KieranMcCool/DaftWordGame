using DaftWordGame;
using DaftWordGame.Models;
using DaftWordGame.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<WordGameService>();
builder.Services.AddRefitClient<IFreeDictionaryApiService>().ConfigureHttpClient(x =>
{
    x.BaseAddress = new Uri("https://api.dictionaryapi.dev/api/v2");
});
await builder.Build().RunAsync();
