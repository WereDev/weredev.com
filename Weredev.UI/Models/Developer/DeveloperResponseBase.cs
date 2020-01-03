namespace Weredev.UI.Models.Developer
{
    public abstract class DeveloperResponseBase
    {
        public const string RepoNameWu10Man = "Wu10Man";

        public string RepoName { get; set; }
        
        public bool HasReleaseNotes { get; set; }
    }
}
