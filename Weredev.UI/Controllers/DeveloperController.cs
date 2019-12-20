using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Models.Developer;
using MarkdownSharp;

namespace Weredev.UI.Controllers
{
    public class DeveloperController : BaseController
    {
        private readonly ICodeRepoService _codeRepoService;
        private readonly Markdown _markdown;

        public DeveloperController(ICodeRepoService codeRepoService)
        {
            _codeRepoService = codeRepoService ?? throw new ArgumentNullException(nameof(codeRepoService));
            _markdown = new Markdown();
        }

        [HttpGet]
        public IActionResult Index()
        {
            SetTitle("developer");
            return View();
        }

        [HttpGet("[controller]/{repoKey}")]
        public async Task<IActionResult> GetReadme(string repoKey)
        {
            SetTitle($"developer | {repoKey}");

            var markdown = await _codeRepoService.GetReadmeMarkdown(repoKey);

            var response = new GetReadmeResponse
            {
                HtmlContent = TransformMarkdown(markdown),
                RepoName = GetRepoName(repoKey),
            };
            return View(response);
        }

        private string GetRepoName(string repoKey)
        {
            return repoKey.ToLower() switch
            {
                "wu10man" => "Wu10Man",
                _ => repoKey?.ToLower(),
            };
        }

        private string TransformMarkdown(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
                return string.Empty;

            var html = _markdown.Transform(markdown);
            html = html.Replace("<h1", "<h4");
            html = html.Replace("<h2", "<h5");
            html = html.Replace("<h3", "<h6");
            return html;
        }
    }
}
