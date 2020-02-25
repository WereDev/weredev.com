using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weredev.Domain.Interfaces;
using Weredev.UI.Models.Developer;
using MarkdownSharp;
using System.Linq;
using AutoMapper;

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
            return View();
        }

        [HttpGet("[controller]/{repoKey}")]
        public async Task<IActionResult> Readme(string repoKey)
        {
            SetTitle($"developer | {repoKey}");

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

        private async Task<T> CreateResponse<T>(string repoKey)
            where T : DeveloperResponseBase
        {
            var instance = Activator.CreateInstance<T>();
            instance.RepoName = repoKey;
            instance.HasReleaseNotes = (await _codeRepoService.GetReleaseHistory(repoKey))?.Any() == true;
            return instance;
        }
    }
}
