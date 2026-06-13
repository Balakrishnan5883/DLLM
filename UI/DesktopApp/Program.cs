using Photino.Blazor;
using RazorClassLibrary.Components;
using RazorClassLibrary.Extensions;

namespace DesktopApp;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

        // 3. Register the root UI component
        appBuilder.RootComponents.Add<Routes>("#app");

        appBuilder.Services.AddCommonServices();
        var app = appBuilder.Build();
        
        // 4. Configure your native window
        app.MainWindow
            .SetTitle("DLLM")
            .SetSize(1000, 800);

        app.Run();
    }
}