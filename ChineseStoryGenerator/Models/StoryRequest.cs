namespace ChineseStoryGenerator.Models
{
    public class StoryRequest
    {
        public string StoryTitle { get; set; }
        public string StoryContent = "";
        public string SelectedLevel { get; set; } 
        public string StoryText { get; set; }
    }
}
