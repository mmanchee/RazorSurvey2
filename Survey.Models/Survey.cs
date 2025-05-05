using System;
using System.Collections.Generic;

namespace Survey.Models
{
    public class SurveyResp
    {
        // General page
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public int? IndustryId { get; set; }
        public string? OtherIndustry { get; set; }
        public int? PositionId { get; set; }
        public string? OtherPosition { get; set; }

        // Length page – possible values: "EndHere", "OneMorePage", "TwoMorePages", "FullSurvey"
        public string? SurveyLength { get; set; }

        // Likes page
        public int? FavoriteColorId { get; set; }
        public string? OtherFavoriteColor { get; set; }
        public List<int>? FavoriteMusicGenreIds { get; set; }
        public string? OtherFavoriteMusicGenre { get; set; }
        public int? FavoriteSeasonId { get; set; }
        public string? OtherFavoriteSeason { get; set; }
        public string? FavoritePlace { get; set; }

        // Ratings page – key: Likert question id, value: rating (1–5)
        public Dictionary<int, int>? Ratings { get; set; }

        // Dislikes page
        public int? WorstColorId { get; set; }
        public string? OtherWorstColor { get; set; }
        public List<int>? WorstMusicGenreIds { get; set; }
        public string? OtherWorstMusicGenre { get; set; }
        public int? WorstSeasonId { get; set; }
        public string? OtherWorstSeason { get; set; }
        public string? WorstPlace { get; set; }
    }
}
