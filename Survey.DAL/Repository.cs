using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Survey.Models;

namespace Survey.DAL
{
    public class Repository : IRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<Repository> _logger;

        public Repository(IConfiguration configuration, ILogger<Repository> logger)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _logger = logger;
        }

        public async Task<List<LookUp>> GetIndustriesAsync()
        {
            var list = new List<LookUp>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Industries", conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new LookUp
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting industries");
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        public async Task<List<LookUp>> GetPositionsAsync()
        {
            var list = new List<LookUp>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Positions", conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new LookUp
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting positions");
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        public async Task<List<LookUp>> GetColorsAsync()
        {
            var list = new List<LookUp>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Colors", conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new LookUp
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting colors");
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        public async Task<List<LookUp>> GetMusicGenresAsync()
        {
            var list = new List<LookUp>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Music", conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new LookUp
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting music genres");
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        public async Task<List<LookUp>> GetSeasonsAsync()
        {
            var list = new List<LookUp>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Seasons", conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new LookUp
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting seasons");
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        public async Task<List<LookUp>> GetLikertQuestionsAsync()
        {
            var list = new List<LookUp>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM LikertQuestions", conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new LookUp
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Likert questions");
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        public async Task SaveSurveyAsync(SurveyResp survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_SaveSurvey", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Supply parameters as needed. (Adjust the parameter names and types per your stored procedure.)
                        cmd.Parameters.AddWithValue("@Date", survey.Date);
                        cmd.Parameters.AddWithValue("@Name", (object)survey.Name ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Age", (object)survey.Age ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IndustryId", (object)survey.IndustryId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@OtherIndustry", (object)survey.OtherIndustry ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PositionId", (object)survey.PositionId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@OtherPosition", (object)survey.OtherPosition ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SurveyLength", (object)survey.SurveyLength ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FavoriteColorId", (object)survey.FavoriteColorId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@OtherFavoriteColor", (object)survey.OtherFavoriteColor ?? DBNull.Value);
                        // For multi-select fields, pass a comma–separated list.
                        cmd.Parameters.AddWithValue("@FavoriteMusicGenreIds", survey.FavoriteMusicGenreIds != null ? string.Join(",", survey.FavoriteMusicGenreIds) : DBNull.Value);
                        cmd.Parameters.AddWithValue("@OtherFavoriteMusicGenre", (object)survey.OtherFavoriteMusicGenre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FavoriteSeasonId", (object)survey.FavoriteSeasonId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@OtherFavoriteSeason", (object)survey.OtherFavoriteSeason ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FavoritePlace", (object)survey.FavoritePlace ?? DBNull.Value);
                        // Serialize the ratings dictionary to JSON.
                        cmd.Parameters.AddWithValue("@Ratings", survey.Ratings != null ? System.Text.Json.JsonSerializer.Serialize(survey.Ratings) : DBNull.Value);
                        cmd.Parameters.AddWithValue("@WorstColorId", (object)survey.WorstColorId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@OtherWorstColor", (object)survey.OtherWorstColor ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@WorstMusicGenreIds", survey.WorstMusicGenreIds != null ? string.Join(",", survey.WorstMusicGenreIds) : DBNull.Value);
                        cmd.Parameters.AddWithValue("@OtherWorstMusicGenre", (object)survey.OtherWorstMusicGenre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@WorstSeasonId", (object)survey.WorstSeasonId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@OtherWorstSeason", (object)survey.OtherWorstSeason ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@WorstPlace", (object)survey.WorstPlace ?? DBNull.Value);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving survey");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
