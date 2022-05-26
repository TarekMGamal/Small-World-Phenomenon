using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Program
{
    public class Program
    {
        static bool optimization = true;
        static Dictionary<string, List<(string, string)>> AdjList;
        static Dictionary<(string, string), int> Strength;

        static void Main(string[] args)
        {
            double timeBefore = 0, timeAfter = 0;
            bool yes = false;
            
            Console.WriteLine("Small-World Phenomenon:\n[1] sample\n[2] small\n[3] medium\n[4] large\n[5] extreme\n");
            Console.Write("Enter your choice [1-2-3-4-5]: ");
            char choice = Console.ReadLine()[0];
            Console.WriteLine("\nDo you want to run with optimization?\nEnter your choice [y-n]: ");
            char opt = Console.ReadLine()[0];
            if (opt == 'n') optimization = false;
            switch (choice)
            {
                case '1':
                    Console.WriteLine("Not implemented yet\n");
                    break;
                case '2':
                    Preproccess("../../../Testcases/Complete/small/Case1/Movies193.txt");
                    timeBefore = System.Environment.TickCount;
                    yes = false;
                    yes |= Check("../../../Testcases/Complete/small/Case1/queries110.txt", "../../../Testcases/Complete/small/Case1/Solution/queries110 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    double time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);

                    Preproccess("../../../Testcases/Complete/small/Case2/Movies187.txt");
                    timeBefore = System.Environment.TickCount;
                    yes = false;
                    yes |= Check("../../../Testcases/Complete/small/Case2/queries50.txt", "../../../Testcases/Complete/small/Case2/Solution/queries50 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);
                    break;
                case '3':
                    Preproccess("../../../Testcases/Complete/medium/Case1/Movies967.txt");
                    timeBefore = System.Environment.TickCount;
                    yes = false;
                    yes |= Check("../../../Testcases/Complete/medium/Case1/queries4000.txt", "../../../Testcases/Complete/medium/Case1/Solutions/queries4000 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);

                    timeBefore = System.Environment.TickCount;
                    yes = false;
                    yes |= Check("../../../Testcases/Complete/medium/Case1/queries85.txt", "../../../Testcases/Complete/medium/Case1/Solutions/queries85 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);

                    Preproccess("../../../Testcases/Complete/medium/Case2/Movies4736.txt");

                    timeBefore = System.Environment.TickCount;
                    yes = false;
                    yes |= Check("../../../Testcases/Complete/medium/Case2/queries2000.txt", "../../../Testcases/Complete/medium/Case2/Solutions/queries2000 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);

                    timeBefore = System.Environment.TickCount;
                    yes = false;
                    yes |= Check("../../../Testcases/Complete/medium/Case2/queries110.txt", "../../../Testcases/Complete/medium/Case2/Solutions/queries110 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);
                    break;
                case '4':
                    Preproccess("../../../Testcases/Complete/large/Movies14129.txt");
                    timeBefore = System.Environment.TickCount;
                    yes = false;
                    yes |= Check("../../../Testcases/Complete/large/queries26.txt", "../../../Testcases/Complete/large/Solutions/queries26 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);

                    timeBefore = System.Environment.TickCount;
                    yes = false;
                    yes |= Check("../../../Testcases/Complete/large/queries600.txt", "../../../Testcases/Complete/large/Solutions/queries600 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);
                    break;
                case '5':
                    Preproccess("../../../Testcases/Complete/extreme/Movies122806.txt");
                    timeBefore = System.Environment.TickCount;
                    yes |= Check("../../../Testcases/Complete/extreme/queries200.txt", "../../../Testcases/Complete/extreme/Solutions/queries200 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);

                    timeBefore = System.Environment.TickCount;
                    yes |= Check("../../../Testcases/Complete/extreme/queries22.txt", "../../../Testcases/Complete/extreme/Solutions/queries22 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    time = (timeAfter - timeBefore) / (60000);
                    Console.WriteLine(time.ToString() + " min");
                    Console.WriteLine(yes);
                    break;
            }

        }

        static string ReadFile(string fileName)
        {
            FileStream file = new(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new(file);
            string moviesData = sr.ReadToEnd();
            sr.Close();
            file.Close();
            return moviesData;
        }

        static void Preproccess(string fileName)
        {
            AdjList = new();
            Strength = new();

            string moviesData = ReadFile(fileName);
            string[] movies = moviesData.Split("\n");

            // O(n * m^2)
            for (int i = 0; i < movies.Length; i++)
            {
                string[] movieData = movies[i].Split('/');
                string m = movieData[0];

                for (int j = 1; j < movieData.Length; j++)
                {
                    string u = movieData[j];

                    for (int k = j + 1; k < movieData.Length; k++)
                    {
                        string v = movieData[k];

                        if (!AdjList.ContainsKey(u)) AdjList.Add(u, new List<(string, string)>());
                        if (!AdjList.ContainsKey(v)) AdjList.Add(v, new List<(string, string)>());

                        AdjList[u].Add((v, m));
                        AdjList[v].Add((u, m));

                        if (!Strength.ContainsKey((u, v))) Strength.Add((u, v), 0);
                        if (!Strength.ContainsKey((v, u))) Strength.Add((v, u), 0);

                        Strength[(u, v)]++;
                        Strength[(v, u)]++;
                    }

                }
            }
        }

        public static bool Check(string queryFile, string solutionFile)
        {
            Algorithms.Algorithms.Prepare(AdjList, Strength);
            
            string tmpQueries = ReadFile(queryFile);
            string tmpSolutions = ReadFile(solutionFile);

            string[] queries = tmpQueries.Split('\n');

            string[] ans = new string[(queries.Length - 1) * 5];
            string[] ANS = tmpSolutions.Split('\n');

            for (int i = 0; i < queries.Length; i++)
            {
                string[] actors = queries[i].Split('/');
                if (actors.Length < 2) continue;

                string actor1 = actors[0];
                string actor2 = actors[1];

                double timeBefore = System.Environment.TickCount;
                (int, int, List<string>, List<string>) answer = Algorithms.Algorithms.Query(actor1, actor2, optimization);
                double timeAfter = System.Environment.TickCount;

                double time = (timeAfter - timeBefore) / (1000.0 * 60.0);
                //Console.WriteLine("Query " + i.ToString() + ": " + time.ToString() + " min");

                int degree = answer.Item1;
                int strength = answer.Item2;
                List<string> actorsPath = answer.Item3;
                List<string> moviesPath = answer.Item4;

                string tmp = actor1 + "/" + actor2;
                ans[i * 5] = tmp;

                tmp = "DoS = " + degree.ToString() + ", RS = " + strength.ToString();
                ans[i * 5 + 1] = tmp;

                tmp = "CHAIN OF ACTORS: ";
                foreach (string actor in actorsPath)
                {
                    tmp = tmp + actor + " -> ";
                }
                tmp = tmp.Remove(tmp.Length - 4);
                ans[i * 5 + 2] = tmp;

                tmp = "CHAIN OF MOVIES:  =>";
                foreach (string movie in moviesPath)
                {
                    tmp = tmp + " " + movie + " =>";
                }
                ans[i * 5 + 3] = tmp;

                ans[i * 5 + 4] = "";
            }
            ans[ans.Length - 1] = "";

            bool yes = true;

            for (int i = 0; i < ans.Length; i++)
            {
                //Console.WriteLine(ans[i]);
                //Console.WriteLine(ANS[i]);
                if (ans[i] != ANS[i])
                {
                    if (i % 5 == 2 || i % 5 == 3) continue;
                    Console.WriteLine("---------------------------------------------------------");
                    Console.WriteLine("\nWA on test " + i.ToString() + "\n");
                    yes = false;
                    //break;
                }

            }
            return yes;
        }

    }
}

