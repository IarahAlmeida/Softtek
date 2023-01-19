using Newtonsoft.Json;

namespace Questao2
{
    public class FootballMatches
    {
        [JsonProperty("page")]
        public int Page;

        [JsonProperty("per_page")]
        public int PerPage;

        [JsonProperty("total")]
        public int Total;

        [JsonProperty("total_pages")]
        public int TotalPages;

        [JsonProperty("data")]
        public List<FootballMatch> FootballMatchesList;

        public FootballMatches()
        {
            FootballMatchesList = new List<FootballMatch>();
        }
    }

    public class FootballMatch
    {
        [JsonProperty("team1")]
        public string Team1;

        [JsonProperty("team2")]
        public string Team2;

        [JsonProperty("team1goals")]
        public int Team1Goals;

        [JsonProperty("team2goals")]
        public int Team2Goals;

        public FootballMatch()
        {
            Team1 = string.Empty;
            Team2 = string.Empty;
        }
    }
}