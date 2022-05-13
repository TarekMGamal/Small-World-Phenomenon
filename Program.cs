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
        static Dictionary<string, List<string>> AdjList;
        static Dictionary<string, bool> isActor;
        static Dictionary<string, int> vis;
        static Dictionary<string, int> dis;
        static Dictionary<string, string> pre;

        static void Main(string[] args)
        {
            AdjList = new();
            isActor = new();
            vis = new();
            dis = new();
            pre = new();

            string fileName = "../../../Testcases/Sample/movies1.txt";
            
            string tempMovieData = ReadMoviesFile(fileName);
            Prepare(tempMovieData);

            Algorithms.Algorithms.Prepare(AdjList, isActor, vis, dis, pre);

            (int, int, List<string>) x = Algorithms.Algorithms.Query("A", "B");
            Console.Write(x.Item1 + " ");
            Console.Write(x.Item2 + " ");
            Console.Write(x.Item3.Count + " ");
            Console.WriteLine();
            for (int i = 0; i < x.Item3.Count; i++)
            {
                Console.WriteLine(x.Item3[i]);
            }
        }

        static string ReadMoviesFile(string fileName)
        {
            FileStream file = new(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new(file);

            string moviesData = sr.ReadToEnd();
            
            sr.Close();
            file.Close();

            return moviesData;
        }

        
        static void Prepare(string moviesData)
        {
            string[] movies = moviesData.Split("\n");
            
            
            for (int i = 0; i < movies.Length; i++)
            {
                string[] movieData = movies[i].Split('/');

                string u = movieData[0];

                if (!isActor.ContainsKey(u)) isActor.Add(u, false);
                else isActor[u] = false;

                if (!vis.ContainsKey(u)) vis.Add(u, 0);
                else vis[u] = 0;

                if (!dis.ContainsKey(u)) dis.Add(u, 0);
                else dis[u] = 0;

                if (!pre.ContainsKey(u)) pre.Add(u, "");
                else pre[u] = "";

                for (int j = 1; j < movieData.Length; j++)
                {
                    string v = movieData[j];

                    if (!isActor.ContainsKey(v)) isActor.Add(v, true);
                    else isActor[v] = true;

                    if (!vis.ContainsKey(v)) vis.Add(v, 0);
                    else vis[v] = 0;

                    if (!dis.ContainsKey(v)) dis.Add(v, 0);
                    else dis[v] = 0;

                    if (!pre.ContainsKey(v)) pre.Add(v, "");
                    else pre[v] = "";

                    if (!AdjList.ContainsKey(u)) AdjList.Add(u, new List<string>());
                    if (!AdjList.ContainsKey(v)) AdjList.Add(v, new List<string>());

                    AdjList[u].Add(v);
                    AdjList[v].Add(u);
                }
            }
        }
    }
}

