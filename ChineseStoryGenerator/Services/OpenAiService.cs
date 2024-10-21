using Azure.AI.OpenAI;
using Azure;
using OpenAI.Chat;
using System.ClientModel;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using ChineseStoryGenerator.Models;

namespace ChineseStoryGenerator.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly HttpClient _httpClient;

        public OpenAiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateTitleAsync(string prompt)
        {
            var response = await _httpClient.PostAsJsonAsync("api/generate-title", prompt);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GenerateStoryAsync(MyChatMessage prompt)
        {
            try
            {
                string promptString = JsonSerializer.Serialize(prompt);
                //var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsJsonAsync("api/generate-story", promptString);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

                throw;
            }
           
        }
    }
}
