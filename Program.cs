using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace q2_map_randomizer
{
    class Program
    {
        static char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        static void Main(string[] args)
        {
            var matches = Bracket.GetMatches(@"e:\temp\bracket.json");

            string file = string.Format(@"e:\temp\maps-{0}.txt", DateTime.Now.ToString("yyyyMMddhhmmss"));

            using (StreamWriter output = new StreamWriter(file, append: true))
            {
                foreach (var match in matches.OrderBy(a => a.Identifier))
                {
                    string matchMap = string.Format("Jogo {0}: {1} - {2}",
                        match.Identifier, string.Format("{0} vs {1}", match.Players[0].Nick, match.Players[1].Nick),
                        GetRandomMap());

                    Console.WriteLine(matchMap);
                    output.WriteLine(matchMap);
                }
            }
        }

        static List<string> GetMaps()
        {
            return new List<string>() {"q2dm1","ztn2dm2","ztn2dm3"};
        }

        static string GetRandomMap()
        {
            return GetMaps().OrderBy(a => Guid.NewGuid()).First();
        }

        static string GetMatch(int index)
        {
            string matchName = string.Empty;
            
            string preffix = string.Empty;
            string suffix = string.Empty;

            int max = alpha.Length;
            int leftover = (int)(Math.Floor((double)index / (double)max));
            
            if (leftover > 0)
            {
                preffix = alpha[leftover -1].ToString();
            }

            index = index - (max * leftover);
            suffix = alpha[index].ToString();

            matchName = preffix + suffix;

            return matchName;
        }

        static int GetMatchCount(string latestRound)
        {
            int matchCount = 0;
            
            if (latestRound.Length == 1)
            {
                matchCount = Array.IndexOf(alpha, latestRound[0]) + 1;
            }
            else
            {
                int aux = (Array.IndexOf(alpha, latestRound[0]) + 1) * 26;
                matchCount = Array.IndexOf(alpha, latestRound[1]) + 1 + aux;
            }

            return matchCount;
        }
    }
}
