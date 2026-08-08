using Dima.Core.Handlers;
using Dima.Web;
using Dima.Web.Handler;
using Dima.Web.Sercurity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var teste = builder.Configuration.GetValue<string>("BackEndUrl") ?? string.Empty;

Configuration.BackEndUrl = builder.Configuration.GetValue<string>("BackEndUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();

builder.Services.AddScoped(serviceProvider => 
    (ICookieAuthenticationStateProvider)serviceProvider.GetRequiredService<AuthenticationStateProvider>()
    );

builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configuration.HttpClientName, options =>
{
    options.BaseAddress = new Uri(teste);
}
).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

await builder.Build().RunAsync();
