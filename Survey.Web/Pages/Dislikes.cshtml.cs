using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Survey.BLL;
using Survey.Models;

namespace Survey.Web.Pages
{
    public class DislikesModel : BaseSurveyPageModel
    {
        private readonly IService _service;
        private readonly ILogger<DislikesModel> _logger;
        public List<LookUp> Colors { get; set; } = new List<LookUp>();
        public List<LookUp> MusicGenres { get; set; } = new List<LookUp>();
        public List<LookUp> Seasons { get; set; } = new List<LookUp>();

        [BindProperty]
        public SurveyResp SurveyForm { get; set; } = new SurveyResp();

        public DislikesModel(IService service, ILogger<DislikesModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            Colors = await _service.GetColorsAsync();
            MusicGenres = await _service.GetMusicGenresAsync();
            Seasons = await _service.GetSeasonsAsync();
            SurveyForm = Survey;
            SetProgress(5);
        }

        public IActionResult OnPost()
        {
            try
            {
                Survey.WorstColorId = SurveyForm.WorstColorId;
                Survey.OtherWorstColor = SurveyForm.OtherWorstColor;
                Survey.WorstMusicGenreIds = SurveyForm.WorstMusicGenreIds;
                Survey.OtherWorstMusicGenre = SurveyForm.OtherWorstMusicGenre;
                Survey.WorstSeasonId = SurveyForm.WorstSeasonId;
                Survey.OtherWorstSeason = SurveyForm.OtherWorstSeason;
                Survey.WorstPlace = SurveyForm.WorstPlace;
                SaveSurvey();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error on POST Dislikes");
            }
            return RedirectToPage("/ThankYou");
        }
    }
}
