using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Survey.BLL;
using Survey.Models;

namespace Survey.Web.Pages
{
    public class RatingsModel : BaseSurveyPageModel
    {
        private readonly IService _service;
        private readonly ILogger<RatingsModel> _logger;
        public List<LookUp> LikertQuestions { get; set; } = new List<LookUp>();

        [BindProperty]
        public Dictionary<int, int> RatingsAnswers { get; set; } = new Dictionary<int, int>();

        public RatingsModel(IService service, ILogger<RatingsModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            LikertQuestions = await _service.GetLikertQuestionsAsync();
            if (Survey.Ratings != null)
                RatingsAnswers = Survey.Ratings;
            SetProgress(4);
        }

        public IActionResult OnPost()
        {
            Survey.Ratings = RatingsAnswers;
            SaveSurvey();
            // If the chosen length is "TwoMorePages" then this is the final survey page. Otherwise, if "FullSurvey", go to Dislikes.
            return Survey.SurveyLength switch
            {
                "TwoMorePages" => RedirectToPage("/ThankYou"),
                _ => RedirectToPage("/Dislikes")
            };
        }
    }
}
