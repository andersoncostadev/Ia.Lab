namespace SynapseLab.Models
{
    public class WeatherPrompt(string city, double temperature, string userQuestion)
    {
        public string City { get; set; } = city;
        public double Temperature { get; set; } = temperature;
        public string UserQuestion { get; set; } = userQuestion;
        public string WeatherInfo { get; set; } = $"Com base na temperatura atual de {temperature}°C em {city}.";

        public string GeneratePrompt()
        {
            return $"Você é um assistente meteorológico. {WeatherInfo}, responda apenas a esta pergunta: \"{UserQuestion}\". Seja direto e objetivo.";
        }
    }
}