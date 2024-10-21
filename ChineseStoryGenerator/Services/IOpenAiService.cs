
using ChineseStoryGenerator.Models;
using OpenAI.Chat;

namespace ChineseStoryGenerator.Services
{
    public interface IOpenAiService
    {
        Task<string> GenerateStoryAsync(MyChatMessage prompt);
        Task<string> GenerateTitleAsync(string prompt);
    }
}