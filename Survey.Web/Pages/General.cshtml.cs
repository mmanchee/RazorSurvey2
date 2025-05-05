using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Survey.BLL;
using Survey.Models;

namespace Survey.Web.Pages
{
    public class GeneralModel : BaseSurveyPageModel
    {
        private readonly IService _service;
        private readonly ILogger<GeneralModel> _logger;
        public List<LookUp> Industries { get; set; } = new List<LookUp>();
        public List<LookUp> Positions { get; set; } = new List<LookUp>();

        [BindProperty]
        public SurveyResp SurveyForm { get; set; } = new SurveyResp();

        public GeneralModel(IService service, ILogger<GeneralModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                Industries = await _service.GetIndustriesAsync();
                Positions = await _service.GetPositionsAsync();
                SurveyForm = Survey;
                // Set today's date if no date has yet been selected.
                if (SurveyForm.Date == default)
                    SurveyForm.Date = DateTime.Today;

                SetProgress(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GET General");
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                // Update the session Survey from form data.
                Survey.Date = SurveyForm.Date;
                Survey.Name = SurveyForm.Name;
                Survey.Age = SurveyForm.Age;
                Survey.IndustryId = SurveyForm.IndustryId;
                Survey.OtherIndustry = SurveyForm.OtherIndustry;
                Survey.PositionId = SurveyForm.PositionId;
                Survey.OtherPosition = SurveyForm.OtherPosition;
                SaveSurvey();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on POST General");
            }
            return RedirectToPage("/Length");
        }
    }
}
