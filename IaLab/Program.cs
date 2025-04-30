using DotNetEnv;
using SynapseLab.Services;

Env.Load();

//var openaiService = new WeatherChatServiceOpenAi();
//await openaiService.RunAsync();

//var service = new WeatherChatService();
//await service.RunAsync();

var ollamaService = new WeatherChatServiceOllama();
await ollamaService.RunAsync();