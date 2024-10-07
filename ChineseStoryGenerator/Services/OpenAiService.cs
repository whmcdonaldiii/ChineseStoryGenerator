using Azure.AI.OpenAI;
using Azure;
using OpenAI.Chat;
using System.ClientModel;

namespace ChineseStoryGenerator.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly ApiKeyCredential _credential;
        private readonly AzureOpenAIClient _azureClient;
        private readonly ChatClient _chatClient;
        public OpenAiService(IConfiguration configuration)
        {
            string key = configuration["AZURE_OPENAI_API_KEY"];
            string endPoint = configuration["AZURE_OPENAI_ENDPOINT"];
            string deployment = configuration["AZURE_OPENAI_DEPLOYMENT"];

            _credential = new(key);
            _azureClient = new(new Uri(endPoint), _credential);
            _chatClient = _azureClient.GetChatClient(deployment);
        }

        public async Task<string> GenerateTitleAsync(string prompt)
        {
            ChatCompletion completion = await _chatClient.CompleteChatAsync(prompt);
            return completion.Content[0].Text;
        }

        public async Task<string> GenerateStoryAsync(ChatMessage[] prompt)
        {
            ChatCompletion completion = await _chatClient.CompleteChatAsync(prompt);
            return completion.Content[0].Text;
        }
    }
}
