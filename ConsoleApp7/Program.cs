using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> dict = new List<string>();

            string[] lines = File.ReadAllLines("input.txt");
            int lineNumber = 0;

            while (lineNumber < lines.Length && !lines[lineNumber].Contains("="))
            {
                dict.AddRange(lines[lineNumber].Replace(" ", " ").ToLower().Trim().Split());
                lineNumber++;
            }

            string res = "";
            lineNumber++;
            while (lineNumber < lines.Length && !lines[lineNumber].Contains("="))
            {
                string[] mass = lines[lineNumber].Replace(" ", " ").Trim().Split();
                for (int j = 0; j < mass.Length; j++)
                {
                    string lowerLineWord = mass[j].ToLower();

                    if (dict.Select(x => x).Where(x => x == lowerLineWord).Count() > 0)
                        res += mass[j] + " ";
                    else
                    {
                        IEnumerable<string> temp = dict.Select(x => x).Where(x => LevenshteinDistance(x, lowerLineWord) <= 1);
                        if (temp.Count() > 1)
                        {
                            res += "{";
                            foreach (string t in temp)
                            {
                                res += t + " ";
                            }
                            res = res.Substring(0, res.Length - 1);
                            res += "} ";
                        }
                        else if (temp.Count() == 1)
                            res += temp.First() + " ";
                        else
                            res += "{" + mass[j] + "?} ";
                    }
                }
                res = res.Substring(0, res.Length - 1);
                res += Environment.NewLine;
                lineNumber++;
            }

            File.WriteAllText("output.txt", res);

        }


        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}
