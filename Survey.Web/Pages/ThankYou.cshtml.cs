using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Survey.BLL;
using Survey.Models;

namespace Survey.Web.Pages
{
    public class ThankYouModel : BaseSurveyPageModel
    {
        private readonly IService _service;
        private readonly ILogger<ThankYouModel> _logger;

        // Expose the Survey for use in the view.
        public SurveyResp Survey => base.Survey;

        public ThankYouModel(IService service, ILogger<ThankYouModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        public void OnGet()
        {
            // Set progress as the final page.
            SetProgress(GetTotalPages());
        }

        public IActionResult OnPostBack()
        {
            // Navigate back depending on the survey length selected.
            return Survey.SurveyLength switch
            {
                "EndHere" => RedirectToPage("/Length"),
                "OneMorePage" => RedirectToPage("/Likes"),
                "TwoMorePages" => RedirectToPage("/Ratings"),
                "FullSurvey" => RedirectToPage("/Dislikes"),
                _ => RedirectToPage("/Length")
            };
        }

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            try
            {
                await _service.SaveSurveyAsync(Survey);
                // Clear the survey data after submission.
                HttpContext.Session.Remove(SessionKey);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error submitting survey");
            }
            return RedirectToPage("/Final");
        }
    }
}
