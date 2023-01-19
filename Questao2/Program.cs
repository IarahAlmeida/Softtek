using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net;

namespace Questao2
{
    public class Program
    {
        public static void Main()
        {
            string teamName = "Paris Saint-Germain";
            int year = 2013;
            int totalGoals = getTotalScoredGoals(teamName, year);

            Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

            teamName = "Chelsea";
            year = 2014;
            totalGoals = getTotalScoredGoals(teamName, year);

            Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

            // Output expected:
            // Team Paris Saint - Germain scored 109 goals in 2013
            // Team Chelsea scored 92 goals in 2014
        }

        public static int getTotalScoredGoals(string team, int year)
        {
            int total = 0;
            string team1Key = "team1";
            string team2Key = "team2";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonmock.hackerrank.com/api/");

                HashSet<string> teamKeys = new HashSet<string> { team1Key, team2Key };
                foreach (string teamKey in teamKeys)
                {
                    int currentPage = 1;
                    bool lastPage = false;
                    do
                    {
                        string query = $"football_matches?page={currentPage.ToString()}&{teamKey}={team}&year={year.ToString()}";

                        HttpResponseMessage response = GetResponse(client, query).Result.EnsureSuccessStatusCode();

                        string result = response.Content.ReadAsStringAsync().Result;

                        var footballMatches = JsonConvert.DeserializeObject<FootballMatches>(result);
                        if (footballMatches != null)
                        {
                            total += footballMatches.FootballMatchesList.Sum(footballMatch => teamKey == team1Key ? footballMatch.Team1Goals : footballMatch.Team2Goals);

                            lastPage = ++currentPage > footballMatches.TotalPages;
                        }
                        else
                        {
                            throw new Exception("Failed to deserialize JSON object.");
                        }
                    } while (!lastPage);
                }
            }

            return total;
        }

        private static async Task<HttpResponseMessage> GetResponse(HttpClient client, string query)
        {
            return await client.GetAsync(query);
        }
    }
}