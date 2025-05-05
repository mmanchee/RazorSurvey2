using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Survey.Models;

namespace Survey.DAL
{
    public interface IRepository
    {
        Task<List<LookUp>> GetIndustriesAsync();
        Task<List<LookUp>> GetPositionsAsync();
        Task<List<LookUp>> GetColorsAsync();
        Task<List<LookUp>> GetMusicGenresAsync();
        Task<List<LookUp>> GetSeasonsAsync();
        Task<List<LookUp>> GetLikertQuestionsAsync();
        Task SaveSurveyAsync(SurveyResp survey);
    }
}
