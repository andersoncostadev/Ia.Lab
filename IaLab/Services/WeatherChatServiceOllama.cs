using Microsoft.Extensions.AI;
using SynapseLab.Models;
using SynapseLab.Prompts;
using System.Text.Json;

namespace SynapseLab.Services
{
    public class WeatherChatServiceOllama
    {
        private readonly IChatClient _chatClient;

        public WeatherChatServiceOllama()
        {
            _chatClient = new OllamaChatClient(new Uri("http://localhost:11434/"), "llama3");
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Iniciando o serviço de previsão do tempo...");
            Console.WriteLine("Digite o nome da cidade");
            var city = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(city))
            {
                Console.WriteLine("Cidade não pode ser vazia.");
                return;
            }
            var temperature = await GetCurrentTemperatureAsync(city);
            var questions = WeatherQuestionGenerator.GetDefaultQuestion();

            while (true)
            {
                Console.WriteLine("\nEscolha uma pergunta da lista ou pressione ENTER para digitar sua própria pergunta:");
                for (int i = 0; i < questions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {questions[i]}");
                }

                Console.Write("\nDigite o número da pergunta ou pressione ENTER: ");
                var input = Console.ReadLine();
                string userQuestion;

                if (int.TryParse(input, out int questionIndex) && questionIndex > 0 && questionIndex <= questions.Count)
                {
                    userQuestion = questions[questionIndex - 1];
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.Write("Digite sua própria pergunta: ");
                        userQuestion = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(userQuestion))
                        {
                            Console.WriteLine("Pergunta não pode ser vazia.");
                            continue;
                        }
                    }
                    else
                    {
                        userQuestion = input;
                    }
                }
                var prompt = WeatherQuestionGenerator.BuildPrompt(new WeatherPrompt(city, temperature, userQuestion!));
                var response = await _chatClient.GetResponseAsync(prompt);
                Console.WriteLine(response);

                Console.WriteLine("\nDeseja fazer outra pergunta? (s/n):");
                var continueInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(continueInput) || continueInput.ToLower() != "s")
                {
                    Console.WriteLine("Encerrando o serviço de previsão do tempo.");
                    break;
                }
            }
        }

        private static async Task<double> GetCurrentTemperatureAsync(string city)
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENWEATHERMAP_API_KEY")
                         ?? throw new InvalidOperationException("OPENWEATHERMAP_API_KEY não encontrado.");

            using var client = new HttpClient();
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(json);
            var root = jsonDoc.RootElement;

            var temp = root.GetProperty("main").GetProperty("temp").GetDouble();

            return temp;
        }
    }
}
