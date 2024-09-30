using Azure.AI.OpenAI;
using Azure;
using OpenAI.Chat;

namespace ChineseStoryGenerator.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly AzureKeyCredential _credential;
        private readonly AzureOpenAIClient _azureClient;
        private readonly ChatClient _chatClient;
        public OpenAiService()
        {
            string key = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
            string endPoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
            string deployment = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT");

            _credential = new AzureKeyCredential(key);
            _azureClient = new(new Uri(endPoint), _credential);
            _chatClient = _azureClient.GetChatClient(deployment);
        }

        public async Task<string> GenerateTitleAsync(string prompt)
        {
            ChatCompletion completion = await _chatClient.CompleteChatAsync(prompt);
            return completion.Content[0].Text;
        }

        public async Task<string> GenerateStoryAsync(string prompt)
        {
            ChatCompletion completion = await _chatClient.CompleteChatAsync(prompt);
            return completion.Content[0].Text;
        }
    }
}
