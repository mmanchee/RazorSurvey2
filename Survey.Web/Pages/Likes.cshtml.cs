using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Survey.BLL;
using Survey.Models;

namespace Survey.Web.Pages
{
    public class LikesModel : BaseSurveyPageModel
    {
        private readonly IService _service;
        private readonly ILogger<LikesModel> _logger;
        public List<LookUp> Colors { get; set; } = new List<LookUp>();
        public List<LookUp> MusicGenres { get; set; } = new List<LookUp>();
        public List<LookUp> Seasons { get; set; } = new List<LookUp>();

        [BindProperty]
        public SurveyResp SurveyForm { get; set; } = new SurveyResp();

        public LikesModel(IService service, ILogger<LikesModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                Colors = await _service.GetColorsAsync();
                MusicGenres = await _service.GetMusicGenresAsync();
                Seasons = await _service.GetSeasonsAsync();
                SurveyForm = Survey;
                SetProgress(3);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error on GET Likes");
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                Survey.FavoriteColorId = SurveyForm.FavoriteColorId;
                Survey.OtherFavoriteColor = SurveyForm.OtherFavoriteColor;
                Survey.FavoriteMusicGenreIds = SurveyForm.FavoriteMusicGenreIds;
                Survey.OtherFavoriteMusicGenre = SurveyForm.OtherFavoriteMusicGenre;
                Survey.FavoriteSeasonId = SurveyForm.FavoriteSeasonId;
                Survey.OtherFavoriteSeason = SurveyForm.OtherFavoriteSeason;
                Survey.FavoritePlace = SurveyForm.FavoritePlace;
                SaveSurvey();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error on POST Likes");
            }
            // For a one–page survey beyond Length, go directly to Thank You.
            return Survey.SurveyLength switch
            {
                "OneMorePage" => RedirectToPage("/ThankYou"),
                _ => RedirectToPage("/Ratings")
            };
        }
    }
}
