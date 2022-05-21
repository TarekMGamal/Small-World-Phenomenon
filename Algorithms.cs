using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    internal static class Algorithms
    {
        static string finalMovie;
        static int inf = (int)1e9;
        static Dictionary<string, List<(string, string)>> AdjList;
        static Dictionary<string, string> PrevNode = new();
        static Dictionary<string, string> PrevMovie = new();
        static Dictionary<string, int> Distance = new();
        static Dictionary<string, int> Strength = new();
        static List<string> moviesPath = new();
        static List<string> actorsPath = new();
        static Dictionary<(string, string), int> DirectStrength;


        public static void Prepare(Dictionary<string, List<(string, string)>> adjList, Dictionary<(string, string), int> strength)
        {
            AdjList = adjList;
            DirectStrength = strength;
        }

        private static (int, int) Dijk(string source, string destination)
        {
            var Pq = new C5.IntervalHeap<(int, int, string, string)>();
            Pq.Add((0, 0, source, ""));

            Distance.Add(source, 0);
            Distance.Add(destination, inf);
            Strength.Add(source, 0);

            while (Pq.Count > 0)
            {
                var tmp = Pq.DeleteMin();

                string node = tmp.Item3;
                string nodeMovieName = tmp.Item4;
                int curDegree = tmp.Item1;
                int curStrength = -tmp.Item2;

                if (node == destination)
                {
                    finalMovie = nodeMovieName;
                    break;
                }
                if (curDegree > Distance[node] || curDegree > Distance[destination]) continue;

                foreach (var entry in AdjList[node])
                {
                    string child = entry.Item1;
                    string childMovieName = entry.Item2;

                    int newDegree = curDegree + 1;
                    int newStrength = curStrength + DirectStrength[(node, child)];

                    if (!Distance.ContainsKey(child)) Distance.Add(child, inf);
                    if (!Strength.ContainsKey(child)) Strength.Add(child, 0);

                    if (newDegree < Distance[child] || (newDegree == Distance[child] && newStrength > Strength[child]))
                    {
                        var temp = (newDegree, -newStrength, child, childMovieName);
                        Pq.Add(temp);

                        Distance[child] = newDegree;
                        Strength[child] = newStrength;

                        if (!PrevNode.ContainsKey(child)) PrevNode.Add(child, "");
                        PrevNode[child] = node;

                        if (!PrevMovie.ContainsKey(childMovieName)) PrevMovie.Add(childMovieName, "");
                        PrevMovie[childMovieName] = nodeMovieName;
                    }
                }
            }

            return (Distance[destination], Strength[destination]);
        }

        private static (List<string>, List<string>) GetPath(string actor1, string actor2)
        {
            PrevNode.Clear();
            PrevMovie.Clear();

            string node = actor2;
            string movie = finalMovie;

            while (node != "") {
                actorsPath.Add(node);

                if (!PrevNode.ContainsKey(node)) PrevNode.Add(node, "");
                node = PrevNode[node];
            }

            while (movie != "")
            {
                actorsPath.Add(movie);

                if (!PrevMovie.ContainsKey(movie)) PrevMovie.Add(movie, "");
                movie = PrevMovie[movie];
            }

            actorsPath.Reverse();
            moviesPath.Reverse();

            return (actorsPath, moviesPath);
        }

        public static (int, int, List<string>, List<string>) Query(string actor1, string actor2)
        {
            Distance.Clear();
            Strength.Clear();
            PrevNode.Clear();
            PrevMovie.Clear();

            (int, int) ans = Dijk(actor1, actor2);
            int degree = ans.Item1;
            int strength = ans.Item2;

            (List<string>, List<string>) paths = GetPath(actor1, actor2);
            
            return (degree, strength, paths.Item1, paths.Item2);
        }
    }
}
