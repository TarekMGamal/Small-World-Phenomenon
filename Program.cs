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
        static void Main(string[] args)
        {
            string fileName = "../../../Testcases/Sample/movies1.txt";

            string tempMovieData = ReadMoviesFile(fileName);
            Dictionary<string, List<Edge.Edge>> AdjList = GetAdjList(tempMovieData);

            
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

        
        static Dictionary<string, List<Edge.Edge>> GetAdjList(string moviesData)
        {
            string[] movies = moviesData.Split("\n");
            Dictionary<string, List<Edge.Edge>> AdjList = new();


            for (int i = 0; i < movies.Length; i++)
            {
                string[] movieData = movies[i].Split('/');

                string movieName = movieData[0];

                for (int j = 1; j < movieData.Length; j++)
                {
                    for (int k = j+1; k < movieData.Length; k++)
                    {
                        string u = movieData[j];
                        string v = movieData[k];

                        Edge.Edge edge1 = new(u, v, movieName);
                        Edge.Edge edge2 = new(v, u, movieName);

                        if (!AdjList.ContainsKey(u))
                        {
                            AdjList.Add(u, new List<Edge.Edge>());
                        }
                        if (!AdjList.ContainsKey(v))
                        {
                            AdjList.Add(v, new List<Edge.Edge>());
                        }

                        AdjList[u].Add(edge1);
                        AdjList[v].Add(edge2);
                    }
                }
            }

            return AdjList;
        }
    }
}

