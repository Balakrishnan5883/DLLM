
namespace LlmBase.Models;
public class ChatInfo

{
    public string Id {get;init;} = Guid.NewGuid().ToString();
    public required MessageRole Role {get; init;}
    public required string Name;
    public required string Content{get;set;}
    public DateTime TimeStamp {get; init;}= DateTime.UtcNow;

}
