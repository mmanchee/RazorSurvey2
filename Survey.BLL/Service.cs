using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Survey.DAL;
using Survey.Models;

namespace Survey.BLL
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        private readonly ILogger<Service> _logger;

        public Service(IRepository repository, ILogger<Service> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<List<LookUp>> GetIndustriesAsync() => _repository.GetIndustriesAsync();
        public Task<List<LookUp>> GetPositionsAsync() => _repository.GetPositionsAsync();
        public Task<List<LookUp>> GetColorsAsync() => _repository.GetColorsAsync();
        public Task<List<LookUp>> GetMusicGenresAsync() => _repository.GetMusicGenresAsync();
        public Task<List<LookUp>> GetSeasonsAsync() => _repository.GetSeasonsAsync();
        public Task<List<LookUp>> GetLikertQuestionsAsync() => _repository.GetLikertQuestionsAsync();

        public async Task SaveSurveyAsync(SurveyResp survey)
        {
            try
            {
                await _repository.SaveSurveyAsync(survey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Service SaveSurveyAsync");
                throw;
            }
        }
    }
}
