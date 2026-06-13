namespace LlmBase.Interfaces;

public interface ILlmServiceFactory
{
    public IEnumerable<ILlmService> Services{get;}

    public ILlmService GetLlmServiceByName(string ModelName);
    public IEnumerable<string> GetModelNames();

}