using Microsoft.AspNetCore.Mvc.RazorPages;
using Survey.Models;
using System.Text.Json;

namespace Survey.Web.Pages
{
    public class BaseSurveyPageModel : PageModel
    {
        protected const string SessionKey = "SurveyData";

        public SurveyResp Survey { get; set; } = new SurveyResp();
        public ProgressInfo Progress { get; set; } = new ProgressInfo();

        public override void OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);
            LoadSurvey();
        }

        protected void LoadSurvey()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            Survey = string.IsNullOrEmpty(json)
                ? new SurveyResp()
                : JsonSerializer.Deserialize<SurveyResp>(json) ?? new SurveyResp();
        }

        protected void SaveSurvey()
        {
            var json = JsonSerializer.Serialize(Survey);
            HttpContext.Session.SetString(SessionKey, json);
        }

        // Determines the total pages based on the survey length value.
        // For example, if SurveyLength = "EndHere" then the survey consists of:
        //   Page 1: General, Page 2: Length, Page 3: Thank You
        protected int GetTotalPages()
        {
            return Survey.SurveyLength switch
            {
                "EndHere" => 3,
                "OneMorePage" => 4,
                "TwoMorePages" => 5,
                "FullSurvey" => 6,
                _ => 3 // Default if not yet set
            };
        }

        // Set the progress using the current page number.
        protected void SetProgress(int currentPage)
        {
            Progress.CurrentPage = currentPage;
            Progress.TotalPages = GetTotalPages();
        }
    }
}
