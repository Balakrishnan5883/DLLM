
using RazorClassLibrary.Models;
using LlmBase.Interfaces;
using LlmBase.Models;

namespace RazorClassLibrary.Services;

public class ChatService
{
    private readonly string UserName;
    private readonly List<ChatSession> _sessions = [];
    private ChatSession _currentSession;
    private readonly ILlmServiceFactory llmServiceFactory;
    private ILlmService _selectedLlmService;

    public IEnumerable<ChatSession> AllSessions => _sessions;
    public ChatSession CurrentSession
    {
        get => _currentSession ?? throw new InvalidOperationException("No session selected");
        private set => _currentSession = value;
    }
    
    public event Action? OnChange;


    public ChatService(ILlmServiceFactory llmServiceFactory)
    {
        this.llmServiceFactory = llmServiceFactory;
        _currentSession = new();
        _sessions.Add(_currentSession);
        UserName = Environment.UserName;
        _selectedLlmService=llmServiceFactory.Services.First();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
    public void CreateNewSession()
    {
        ChatSession newSession = new();
        _sessions.Add(newSession);
        _currentSession = newSession;
        NotifyStateChanged();
    }

    public void SwitchSession(string sessionId)
    {
        _currentSession = _sessions.First(session => session.Id == sessionId);
        NotifyStateChanged();
    }

    public void DeleteSession(string sessionId)
    {
        _sessions.RemoveAll(session=> session.Id == sessionId);
        if(_currentSession.Id == sessionId)
        {
            ChatSession newSession = _sessions.FirstOrDefault()?? new();
            _currentSession = newSession;
            _sessions.Add(newSession);
        }
        NotifyStateChanged();
    }

    public void ChangeModel(string ModelName)
    {
        _selectedLlmService = llmServiceFactory.GetLlmServiceByName(ModelName);
        NotifyStateChanged();
    }



    public async Task<ChatInfo> SendUserMessageAsync(string content)
    {
        ChatInfo userMessage = new()
        {
          Role = MessageRole.User,
          Content=  content,
          Name=UserName
          
        };
        CurrentSession.Messages.Add(userMessage);

        string response = await _selectedLlmService.CompleteChatAsync( CurrentSession.Messages);
        ChatInfo assistantResponse = new ()
        {
          Role = MessageRole.Assistant  ,
          Content = response,
          Name=_selectedLlmService.ModelName

        };
        CurrentSession.Messages.Add(assistantResponse);
        return assistantResponse;
    }

    public async IAsyncEnumerable<string> StreamResponseForUserMessageAsync(string content)
    {
        ChatInfo userMessage = new()
        {
          Role = MessageRole.User,
          Content=  content,
          Name = UserName
        };
        CurrentSession.Messages.Add(userMessage);
        
        ChatInfo assistantResponse = new()
        {
            Role = MessageRole.Assistant,
            Content = "",
            Name=_selectedLlmService.ModelName
        };
        CurrentSession.Messages.Add(assistantResponse);

        await foreach(string chunk in _selectedLlmService.StreamChatAsync(CurrentSession.Messages))
        {
            assistantResponse.Content += chunk ;
            CurrentSession.Messages[^1] = assistantResponse;
            yield return chunk;
        }
        CurrentSession.Messages[^1] =assistantResponse;

        
    }

    
}


