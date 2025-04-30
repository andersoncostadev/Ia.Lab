using SynapseLab.Models;

namespace SynapseLab.Prompts
{
    public static class WeatherQuestionGenerator
    {
        public static List<string> GetDefaultQuestion()
        {
            return
            [
                "Vou precisar de guarda-chuva hoje?",
                "Qual é a previsão do tempo para hoje?",
                "Vai chover amanhã?",
                "Como estará o clima na próxima semana?",
                "Qual é a temperatura atual?",
                "Devo levar um casaco?",
                "Vai fazer calor hoje?",
                "Está ventando muito?",
                "Qual é a umidade do ar?",
                "Vai nevar esta noite?"
            ];
        }

        public static string BuildPrompt(WeatherPrompt prompt)
        {
            return prompt.GeneratePrompt();
        }
    }
}
