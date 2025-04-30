using Microsoft.Extensions.AI;

namespace SynapseLab.Services
{
    public class WeatherChatServiceOpenAi
    {
        private readonly IChatClient _chatClient;

        public WeatherChatServiceOpenAi()
        {
            _chatClient = new OpenAI.Chat.ChatClient("gpt-4o-mini", Environment.GetEnvironmentVariable("OPENAI_API_KEY")).AsIChatClient();
        }

        public async Task RunAsync()
        {
            var messages = new List<ChatMessage>
            {
                new(ChatRole.System, "Você é um assistente meteorológico."),
                new(ChatRole.User, "Vou precisar de guarda-chuva amanhã?")
            };

            var response = await _chatClient.GetResponseAsync(messages);
            Console.WriteLine(response);
        }
    }
}
