using RazorClassLibrary.Services;
using LlmBase.Interfaces;
using LlmBase.Providers;
using Microsoft.Extensions.DependencyInjection;
namespace RazorClassLibrary.Extensions;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<ChatService>();
        services.AddSingleton<ILlmServiceFactory,LlmServiceFactory>();
        
        return services;       
    }


}
