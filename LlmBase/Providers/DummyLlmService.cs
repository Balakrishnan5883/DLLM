

using System.Runtime.CompilerServices;
using LlmBase.Interfaces;
using LlmBase.Models;
using Microsoft.Extensions.AI;
using static LlmBase.Helpers.Common;

namespace LlmBase.Providers;

public class DummyLlmService : ILlmService
{
    public string ModelName { get ; init ; }
    public DummyLlmService()
    {
        ModelName="Dummy";
    }

    public async Task<string> CompleteChatAsync(IEnumerable<ChatInfo> history, CancellationToken cancellationToken = default)
    {
        List<ChatMessage> messages = ParseHistoryToMessages(history);
        string userMessage = "";
        await Task.Delay(1000, cancellationToken);
        for(int i = messages.Count-1;i>=0 ;i--)
        {
            if (messages[i].Role == ChatRole.User)
            {
                userMessage = messages[i].Text;
                break;
            }
        }
        return $"Echo(No Stream): {userMessage}";
    }

    public async IAsyncEnumerable<string> StreamChatAsync(IEnumerable<ChatInfo> history, 
                    [EnumeratorCancellation]CancellationToken cancellationToken = default)
    {
        List<ChatMessage> messages = ParseHistoryToMessages(history);
        string userMessage = "";
        for(int i = messages.Count-1;i>=0 ;i--)
        {
            if (messages[i].Role == ChatRole.User)
            {
                userMessage = messages[i].Text;
                break;
            }
        }
        string[] words = $"Echo(stream): {userMessage}".Split(' ');
        
        foreach(string word in words)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(100,cancellationToken);
            yield return word + ' ';    
        } 
        
    }   
}