using LlmBase.Interfaces;

namespace LlmBase.Providers;

public class LlmServiceFactory : ILlmServiceFactory
{
    public IEnumerable<ILlmService> Services =>_services;
    private readonly IEnumerable<ILlmService> _services;

    public LlmServiceFactory()
    {
        List<ILlmService> llmServices=[];
        llmServices.Add(new DummyLlmService());
        llmServices.Add(new OllamaLlmService());
        _services = llmServices;

    }


    public ILlmService GetLlmServiceByName(string ModelName)
    {
        return Services.First(llmService=>llmService.ModelName==ModelName);
    }

    public IEnumerable<string> GetModelNames()
    {
        return _services.Select(llmService=>llmService.ModelName);
    }
}