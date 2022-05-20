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
        static double totalTime = 0;
        static Dictionary<string, List<(string, string)>> AdjList;
        static Dictionary<(string, string) , int> Strength;

        static void Main(string[] args)
        {
            double timeBefore = 0, timeAfter = 0;
            bool yes = false;

            timeBefore = System.Environment.TickCount;
            int cnt = 0;
            for (long i = 0; i < 2e5; i++)
            {
                for (long j = 0; j < 1e7; j++)
                {
                    cnt++;
                }
            }
            Console.WriteLine(cnt);
            timeAfter = System.Environment.TickCount;
            Console.WriteLine((timeAfter - timeBefore) / (60000));


            return;
            Console.WriteLine("Small-World Phenomenon:\n[1] sample\n[2] small\n[3] medium\n[4] large\n[5] extreme\n");
            Console.Write("Enter your choice [1-2-3-4-5]: ");
            char choice = Console.ReadLine()[0];
            switch (choice)
            {
                case '1':
                    break;
                case '2':
                    timeBefore = System.Environment.TickCount;
                    Preproccess("../../../Testcases/Complete/small/Case1/Movies193.txt");
                    yes |= Check("../../../Testcases/Complete/small/Case1/queries110.txt", "../../../Testcases/Complete/small/Case1/Solution/queries110 - Solution.txt");
                    Preproccess("../../../Testcases/Complete/small/Case2/Movies187.txt");
                    yes |= Check("../../../Testcases/Complete/small/Case2/queries50.txt", "../../../Testcases/Complete/small/Case2/Solution/queries50 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    break;
                case '3':
                    
                    Preproccess("../../../Testcases/Complete/medium/Case1/Movies967.txt");
                    //timeBefore = System.Environment.TickCount;
                    yes |= Check("../../../Testcases/Complete/medium/Case1/queries4000.txt", "../../../Testcases/Complete/medium/Case1/Solutions/queries4000 - Solution.txt");
                    //timeAfter = System.Environment.TickCount;
                    //Console.WriteLine((timeAfter - timeBefore) / (1000 * 60));

                    //timeBefore = System.Environment.TickCount;
                    yes |= Check("../../../Testcases/Complete/medium/Case1/queries85.txt", "../../../Testcases/Complete/medium/Case1/Solutions/queries85 - Solution.txt");
                    //timeAfter = System.Environment.TickCount;
                    //Console.WriteLine((timeAfter - timeBefore) / (1000 * 60));

                    Preproccess("../../../Testcases/Complete/medium/Case2/Movies4736.txt");
                    
                    //timeBefore = System.Environment.TickCount;
                    yes |= Check("../../../Testcases/Complete/medium/Case2/queries2000.txt", "../../../Testcases/Complete/medium/Case2/Solutions/queries2000 - Solution.txt");
                    //timeAfter = System.Environment.TickCount;
                    //Console.WriteLine((timeAfter - timeBefore) / (1000 * 60));

                    //timeBefore = System.Environment.TickCount;
                    yes |= Check("../../../Testcases/Complete/medium/Case2/queries110.txt", "../../../Testcases/Complete/medium/Case2/Solutions/queries110 - Solution.txt");
                    //timeAfter = System.Environment.TickCount;
                    //Console.WriteLine((timeAfter - timeBefore) / (1000 * 60));

                    break;
                case '4':
                    timeBefore = System.Environment.TickCount;
                    Preproccess("../../../Testcases/Complete/large/Movies14129.txt");
                    yes |= Check("../../../Testcases/Complete/large/queries26.txt", "../../../Testcases/Complete/large/Solutions/queries26 - Solution.txt");
                    yes |= Check("../../../Testcases/Complete/large/queries600.txt", "../../../Testcases/Complete/large/Solutions/queries600 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    break;
                case '5':
                    timeBefore = System.Environment.TickCount;
                    Preproccess("../../../Testcases/Complete/extreme/Movies122806.txt");
                    //Count();
                    yes |= Check("../../../Testcases/Complete/extreme/queries200.txt", "../../../Testcases/Complete/extreme/Solutions/queries200 - Solution.txt");
                    yes |= Check("../../../Testcases/Complete/extreme/queries22.txt", "../../../Testcases/Complete/extreme/Solutions/queries22 - Solution.txt");
                    timeAfter = System.Environment.TickCount;
                    break;
            }
            Console.WriteLine(yes);
            double time = (timeAfter - timeBefore) / (1000.0 * 60.0);
            Console.WriteLine(time.ToString() , "min");

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

        static void Count()
        {
            int mx = 0;
            foreach(var entry in AdjList)
            {
                List<(string, string)> list = entry.Value;
                mx = Math.Max(mx, list.Count());
                //Console.WriteLine(list.Count);
            }
            Console.WriteLine(mx);
        }

        static void Preproccess(string fileName)
        {
            AdjList = new();
            Strength = new();

            string moviesData = ReadFile(fileName);
            string[] movies = moviesData.Split("\n");
            
            for (int i = 0; i < movies.Length; i++)
            {
                string[] movieData = movies[i].Split('/');
                string m = movieData[0];

                for (int j = 1; j < movieData.Length; j++)
                {
                    string u = movieData[j];

                    for (int k = j+1; k < movieData.Length; k++)
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

            //double timeBefore = System.Environment.TickCount;
            Algorithms.Algorithms.Prepare(AdjList);
            //double timeAfter = System.Environment.TickCount;

            string tmpQueries = ReadFile(queryFile);
            string tmpSolutions = ReadFile(solutionFile);

            string[] queries = tmpQueries.Split('\n');

            totalTime = 0;
            for (int i = 0; i < queries.Length ; i++)
            {
                string[] actors = queries[i].Split('/');
                if (actors.Length < 2) continue;

                string actor1 = actors[0];
                string actor2 = actors[1];

                double timeBefore = System.Environment.TickCount;
                (int, int) answer = Algorithms.Algorithms.Query(actor1, actor2);
                double timeAfter = System.Environment.TickCount;

                double time = (timeAfter - timeBefore) / (1000.0 * 60.0);
                totalTime += time;
            }
            Console.WriteLine(totalTime);
            
            return true;
        }

    }
}

