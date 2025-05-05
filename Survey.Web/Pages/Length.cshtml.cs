using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Survey.Models;

namespace Survey.Web.Pages
{
    public class LengthModel : BaseSurveyPageModel
    {
        private readonly ILogger<LengthModel> _logger;

        [BindProperty]
        public SurveyResp SurveyForm { get; set; } = new SurveyResp();

        public LengthModel(ILogger<LengthModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            SurveyForm = Survey;
            // Default the survey length if not set.
            if (string.IsNullOrEmpty(SurveyForm.SurveyLength))
            {
                SurveyForm.SurveyLength = "EndHere";
            }
            SetProgress(2);
        }

        public IActionResult OnPost()
        {
            try
            {
                Survey.SurveyLength = SurveyForm.SurveyLength;
                SaveSurvey();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on POST Length");
            }
            // If "EndHere" is selected then go directly to Thank You; otherwise launch the next page.
            return Survey.SurveyLength switch
            {
                "EndHere" => RedirectToPage("/ThankYou"),
                _ => RedirectToPage("/Likes")
            };
        }
    }
}
