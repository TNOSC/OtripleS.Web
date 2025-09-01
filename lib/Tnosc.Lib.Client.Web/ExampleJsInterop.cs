using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Tnosc.Lib.Client.Web;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class ExampleJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public ExampleJsInterop(IJSRuntime jsRuntime) =>
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Tnosc.Lib.Client.Web/exampleJsInterop.js").AsTask());

    public async ValueTask<string> Prompt(string message)
    {
        IJSObjectReference module = await moduleTask.Value;
        return await module.InvokeAsync<string>("showPrompt", message);
    }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    public async ValueTask DisposeAsync()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
    {
        if (moduleTask.IsValueCreated)
        {
            IJSObjectReference module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
