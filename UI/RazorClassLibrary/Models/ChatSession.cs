using LlmBase.Models;
namespace RazorClassLibrary.Models;

public class ChatSession
{
    public string Id {get;} = Guid.NewGuid().ToString();
    public string Title{get;set;} = "New Chat";
    public DateTime CreatedAt {get;} = DateTime.UtcNow;
    public DateTime LastModified {get; private set;} = DateTime.UtcNow;

    public List<ChatInfo> Messages {get;} = [];


    public void Touch()
    {
        LastModified = DateTime.UtcNow;
    }

}
