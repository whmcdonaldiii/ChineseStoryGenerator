using OpenAI.Chat;

namespace ChineseStoryHome.Api.Services
{
    public interface IOpenAiService
    {
        Task<string> GenerateStoryAsync(ChatMessage[] prompt);
        Task<string> GenerateTitleAsync(string prompt);
    }
}
