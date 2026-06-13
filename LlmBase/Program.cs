using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

using System.Collections.Concurrent;



OllamaChatClient chatClient = new(
    endpoint:new Uri("http://localhost:11434"),
    modelId:"gemma4:26b"
);



ChatClientAgent agent = new(
    chatClient:chatClient,
    instructions:"Chats are prompted by free user. Be very careful about token consumption, generate responses as minimum as possible.");


AgentSession session = await agent.CreateSessionAsync();
string? userInput;
do
{
    Console.Write("User:> ");
    userInput=Console.ReadLine();
    if ( userInput == string.Empty || userInput is null )
    {
        break;
    }

    var result = await agent.RunAsync(userInput,session);

    Console.WriteLine("Assistant:> " + result);

}
while (userInput is not null);