
namespace ChineseStoryGenerator.Services
{
    public interface IOpenAiService
    {
        Task<string> GenerateStoryAsync(string prompt);
        Task<string> GenerateTitleAsync(string prompt);
    }
}