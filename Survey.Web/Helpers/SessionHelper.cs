using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Survey.Models;


namespace Survey.Web.Helpers
{
    public static class SessionHelper
    {
        private const string SessionKey = "SurveyData";

        public static void SetSurvey(this ISession session, SurveyResp survey)
        {
            string json = JsonSerializer.Serialize(survey);
            session.SetString(SessionKey, json);
        }

        public static SurveyResp GetSurvey(this ISession session)
        {
            string? json = session.GetString(SessionKey);
            return string.IsNullOrEmpty(json) ? new SurveyResp() : JsonSerializer.Deserialize<SurveyResp>(json)!;
        }
    }
}
