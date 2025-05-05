using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Survey.Models;

namespace Survey.DAL
{
    public class JsonRepository : IRepository
    {
        private readonly ILogger<JsonRepository> _logger;
        private readonly string _dataFile;
        private JsonData? _jsonData;

        public JsonRepository(ILogger<JsonRepository> logger)
        {
            _logger = logger;
            _dataFile = "C:\\Source\\RazorSurvey2\\Survey.Models\\TestData\\testdata.json";
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(_dataFile))
                {
                    string jsonString = File.ReadAllText(_dataFile);
                    _jsonData = JsonSerializer.Deserialize<JsonData>(jsonString);
                }
                else
                {
                    _logger.LogError("Test data file not found: " + _dataFile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading test data");
            }
        }

        public Task<List<LookUp>> GetIndustriesAsync() =>
            Task.FromResult(_jsonData?.Industries ?? new List<LookUp>());

        public Task<List<LookUp>> GetPositionsAsync() =>
            Task.FromResult(_jsonData?.Positions ?? new List<LookUp>());

        public Task<List<LookUp>> GetColorsAsync() =>
            Task.FromResult(_jsonData?.Colors ?? new List<LookUp>());

        public Task<List<LookUp>> GetMusicGenresAsync() =>
            Task.FromResult(_jsonData?.Music ?? new List<LookUp>());

        public Task<List<LookUp>> GetSeasonsAsync() =>
            Task.FromResult(_jsonData?.Seasons ?? new List<LookUp>());

        public Task<List<LookUp>> GetLikertQuestionsAsync() =>
            Task.FromResult(_jsonData?.LikertQuestions ?? new List<LookUp>());

        public Task SaveSurveyAsync(SurveyResp survey)
        {
            // For test purposes, simply log the JSON-saved survey.
            _logger.LogInformation("Survey saved (TEST MODE): " + JsonSerializer.Serialize(survey));
            Console.WriteLine("Survey saved (TEST MODE): " + JsonSerializer.Serialize(survey));
            return Task.CompletedTask;
        }
    }

    public class JsonData
    {
        public List<LookUp> Industries { get; set; }
        public List<LookUp> Positions { get; set; }
        public List<LookUp> Colors { get; set; }
        public List<LookUp> Music { get; set; }
        public List<LookUp> Seasons { get; set; }
        public List<LookUp> LikertQuestions { get; set; }
    }
}
