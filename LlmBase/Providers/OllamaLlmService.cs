using System;
using Microsoft.Extensions.AI;
using LlmBase.Models;
using LlmBase.Interfaces;

using static LlmBase.Helpers.Common;

namespace LlmBase.Providers;

public class OllamaLlmService : ILlmService
{
    private readonly IChatClient _chatClient;
    public string ModelName { get ; init; }
    public OllamaLlmService()
    {
        ModelName = "gemma4:26b";
        _chatClient = new OllamaChatClient(
            endpoint: new Uri("http://localhost:11434"),
            modelId:"gemma4:26b"
        );

    }


    public Task<string> CompleteChatAsync(IEnumerable<ChatInfo> history, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async IAsyncEnumerable<string> StreamChatAsync(IEnumerable<ChatInfo> history, 
                                [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<ChatMessage> messages = ParseHistoryToMessages(history);

        await foreach(ChatResponseUpdate update in _chatClient.GetStreamingResponseAsync(messages:messages,cancellationToken:cancellationToken))
        {
            if (update.Text !=null)
            {
                yield return update.Text;
            }
        }
    }
}
