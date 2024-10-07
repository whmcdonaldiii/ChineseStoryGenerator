
using OpenAI.Chat;

namespace ChineseStoryGenerator.Services
{
    public interface IOpenAiService
    {
        Task<string> GenerateStoryAsync(ChatMessage[] prompt);
        Task<string> GenerateTitleAsync(string prompt);
    }
}