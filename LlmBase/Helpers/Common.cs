namespace LlmBase.Helpers;
using Microsoft.Extensions.AI;
using LlmBase.Models;


public static class Common
{
    public static List<ChatMessage> ParseHistoryToMessages(IEnumerable<ChatInfo> history)
    {
        return[.. history.Select(item=>
        {
            var role= item.Role switch
            {
                MessageRole.User => ChatRole.User,
                MessageRole.Assistant=>ChatRole.Assistant,
                MessageRole.System =>ChatRole.System,
                _ =>ChatRole.User
            };
            return new ChatMessage(role,item.Content);
        }
        )];
    }
}