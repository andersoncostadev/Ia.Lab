# WeatherChat.AI - Assistente Meteorológico com IA Local (LLaMA + .NET)

Este projeto demonstra como integrar inteligência artificial generativa local (modelo **LLaMA 3 via Ollama**) com dados em tempo real utilizando **.NET 8**, **Microsoft.Extensions.AI** e a **API OpenWeatherMap**.

---

## ✨ Funcionalidades

- Consulta a **temperatura atual** de qualquer cidade via OpenWeatherMap
- Gera **respostas inteligentes** com base em perguntas meteorológicas predefinidas ou personalizadas
- Integração com **modelo local LLaMA 3** via **Ollama**
- Uso da nova biblioteca **Microsoft.Extensions.AI** para padronização do uso de IChatClient
- Interface de console interativa
- Permite realizar **múltiplas perguntas sem reiniciar o app**

---

## 💡 Tecnologias e Pacotes Utilizados

- [.NET 8](https://dotnet.microsoft.com/en-us/download)
- [Microsoft.Extensions.AI](https://learn.microsoft.com/dotnet/core/extensions/artificial-intelligence)
- [Ollama](https://ollama.com/) (para executar modelos LLM localmente)
- Modelo: `llama3`
- [OpenWeatherMap API](https://openweathermap.org/api)
- `Microsoft.Extensions.AI.Ollama`
- `System.Text.Json`

---

## 🚀 Como Executar Localmente

### 1. Instale o modelo LLaMA 3 via Ollama
```bash
ollama run llama3
```

### 2. Configure as variáveis de ambiente

Crie um arquivo `.env` com as chaves:
```bash
OPENWEATHERMAP_API_KEY=SuaChaveAqui
```

> (Não é necessário chave para o Ollama rodando localmente)

### 3. Execute a aplicação
```bash
dotnet run
```

---

## 📄 Estrutura do Projeto

```
SynapseLab
├── Models
│   └── WeatherPrompt.cs
├── Prompts
│   └── WeatherQuestionGenerator.cs
├── Services
│   ├── WeatherChatServiceOllama.cs
│   └── WeatherChatServiceOpenAi.cs (exemplo com OpenAI)
├── Program.cs
└── .env
```

---

## ⚠️ Importância de Prompts Claros

Durante o desenvolvimento, reforçamos o uso de **instruções claras e objetivas nos prompts** para reduzir alucinações e garantir respostas coerentes da IA. Um bom prompt define o comportamento esperado do modelo com mais assertividade.

---

## 📖 Exemplo de Uso
```
Digite o nome da cidade: Campinas
Pergunta: Vou precisar de guarda-chuva hoje?

Resposta da IA:
"Com base na temperatura atual de 25°C em Campinas, não há indícios de chuva hoje."
```

---

## 📅 Autor

Anderson Costa  
[LinkedIn](www.linkedin.com/in/anderson-correia-da-costa)  

---

## 🔗 Link da Publicação

> O projeto também foi demonstrado no LinkedIn. [Veja a publicação aqui.](#)

---

**Este é um projeto de estudo com foco prático em IA generativa local usando .NET e prompts dinâmicos baseados em dados reais.**

