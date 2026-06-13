
using LlmBase.Models;

namespace LlmBase.Interfaces;

public interface ILlmService
{
    string ModelName { get; init; }
    IAsyncEnumerable<string> StreamChatAsync(IEnumerable<ChatInfo> history, CancellationToken cancellationToken = default);

    Task<string> CompleteChatAsync(IEnumerable<ChatInfo> history, CancellationToken cancellationToken = default);


}
