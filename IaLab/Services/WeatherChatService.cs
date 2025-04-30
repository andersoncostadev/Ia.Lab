using SynapseLab.Models;
using SynapseLab.Prompts;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SynapseLab.Services
{
    public class WeatherChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _modelUrl;

        public WeatherChatService()
        {
            _httpClient = new HttpClient();
            _apiKey = Environment.GetEnvironmentVariable("HUGGINGFACE_API_KEY")
                     ?? throw new InvalidOperationException("HUGGINGFACE_API_KEY não encontrado.");

            _modelUrl = "https://api-inference.huggingface.co/models/HuggingFaceH4/zephyr-7b-beta";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
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

            Console.WriteLine("\nEscolha uma pergunta da lista ou pressione ENTER para digitar sua própria pergunta:");
            var questions = WeatherQuestionGenerator.GetDefaultQuestion();

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
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Número da pergunta inválido.");
                    return;
                }
            }

            Console.WriteLine($"\nPergunta selecionada: {userQuestion}");

            var promptModel = new WeatherPrompt(city, temperature, userQuestion);

            var prompt = WeatherQuestionGenerator.BuildPrompt(promptModel);

            var data = new
            {
                inputs = prompt,
                parameters = new
                {
                    max_new_tokens = 200
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_modelUrl, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
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
