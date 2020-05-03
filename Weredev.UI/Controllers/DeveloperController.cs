using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weredev.Domain.Interfaces;
using Weredev.UI.Models.Developer;
using MarkdownSharp;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace Weredev.UI.Controllers
{
    public class DeveloperController : BaseController
    {
        private readonly ICodeRepoService _codeRepoService;
        private readonly Markdown _markdown;
        private readonly Mapper _mapper;

        public DeveloperController(ICodeRepoService codeRepoService)
        {
            _codeRepoService = codeRepoService ?? throw new ArgumentNullException(nameof(codeRepoService));
            _markdown = new Markdown();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DeveloperMapperProfile>();
            });
            _mapper = new Mapper(mapperConfig);
        }

        [HttpGet]
        public IActionResult Index()
        {
            SetTitle("developer");
            SetKeywords("wu10man", "weredev.com");
            SetDescription("Showcasing some of the stuff I've tinkered with.");
            return View();
        }

        [HttpGet("[controller]/{repoKey}")]
        public async Task<IActionResult> Readme(string repoKey)
        {
            SetTitle($"developer | {repoKey}");
            SetKeywords(repoKey);
            SetDescription(repoKey);

            var markdown = await _codeRepoService.GetReadmeMarkdown(repoKey);

            if (string.IsNullOrWhiteSpace(markdown))
                return NotFound();

            var response = await CreateResponse<ReadmeResponse>(repoKey);
            response.HtmlContent = TransformMarkdown(markdown);

            return View(response);
        }

        [HttpGet("[controller]/{repoKey}/releases")]
        public async Task<IActionResult> ReleaseNotes(string repoKey)
        {
            SetTitle($"developer | {repoKey} | releases");
            SetKeywords(repoKey, "Releases", "Release History");
            SetDescription(repoKey);

            if (string.IsNullOrWhiteSpace(repoKey))
                return NotFound();

            var response = await CreateResponse<ReleaseNotesResponse>(repoKey);

            if (!response.HasReleaseNotes)
                return NotFound();

            var notes = await _codeRepoService.GetReleaseHistory(repoKey);
            response.Releases = notes.Select(x => _mapper.Map<ReleaseNotesResponse.Release>(x)).ToArray();
            foreach (var release in response.Releases)
            {
                release.Body = TransformMarkdown(release.Body);
            }

            return View(response);
        }

        protected override void SetDescription(string repoKey)
        {
            var description = repoKey.ToLower() switch
            {
                "wu10man" => "Windows 10 has decided that users are no longer smart enough to control their own updates, so I wrote this to grant that control back.",
                "weredev.com" => "Information and code on the very site that you're looking at.",
                _ => string.Empty,
            };

            base.SetDescription(description);
        }

        private string TransformMarkdown(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
                return string.Empty;

            var html = _markdown.Transform(markdown);
            html = html.Replace("<h1", "<h4").Replace("h1>", "h4>");
            html = html.Replace("<h2", "<h5").Replace("h2>", "h5>");
            html = html.Replace("<h3", "<h6").Replace("h3>", "h6>");
            return html;
        }

        private async Task<T> CreateResponse<T>(string repoKey)
            where T : DeveloperResponseBase
        {
            var instance = Activator.CreateInstance<T>();
            instance.RepoName = repoKey;
            instance.HasReleaseNotes = (await _codeRepoService.GetReleaseHistory(repoKey))?.Any() == true;
            return instance;
        }

        private void SetKeywords(string repoKey, params string[] additionalKeywords)
        {
            var keywords = repoKey.ToLower() switch
            {
                "wu10man" => new List<string>
                {
                    "wu10man",
                    "Windows 10",
                    "Windows Updates",
                    "Windows Update Manager",
                    "Windows Update Service",
                    "declutter",
                    "WPF",
                },
                "weredev.com" => new List<string>
                {
                    "weredev.com",
                    "mvc.net",
                    "dotnet core",
                    "scss",
                    "stylecop",
                    "bootstrap",
                    "github",
                    "azure pipelines",
                    "automapper",
                    "markdownsharp",
                },
                _ => new List<string>(),
            };

            if (additionalKeywords?.Length > 0)
                keywords.AddRange(additionalKeywords);

            SetKeywords(keywords.ToArray());
        }
    }
}
