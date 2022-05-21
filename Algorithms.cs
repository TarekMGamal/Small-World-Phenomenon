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
        static Dictionary<(string, string), int> DirectStrength;


        public static void Prepare(Dictionary<string, List<(string, string)>> adjList, Dictionary<(string, string), int> strength)
        {
            AdjList = adjList;
            DirectStrength = strength;
        }

        private static (int, int) Dijk(string source, string destination, bool optimization)
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

                // optimization
                if (node == destination)
                {
                    finalMovie = nodeMovieName;
                    if (optimization) break;
                }
                if (optimization)
                {
                    if (curDegree > Distance[node] || curDegree > Distance[destination]) continue;
                }

                // looping through children
                foreach (var entry in AdjList[node])
                {
                    string child = entry.Item1;
                    string childMovieName = entry.Item2;

                    // calculate new degree & strength
                    int newDegree = curDegree + 1;
                    int newStrength = curStrength + DirectStrength[(node, child)];

                    if (!Distance.ContainsKey(child)) Distance.Add(child, inf);
                    if (!Strength.ContainsKey(child)) Strength.Add(child, 0);

                    if (newDegree < Distance[child] || (newDegree == Distance[child] && newStrength > Strength[child]))
                    {
                        // new optimal edge found

                        // push the new optimal edge in priority queue
                        var temp = (newDegree, -newStrength, child, childMovieName);
                        Pq.Add(temp);

                        // update Distance & Strength dictionaries
                        Distance[child] = newDegree;
                        Strength[child] = newStrength;

                        // update PrevNode & PrevMovie dictionaries
                        if (!PrevNode.ContainsKey(child)) PrevNode.Add(child, "");
                        if (!PrevMovie.ContainsKey(childMovieName)) PrevMovie.Add(childMovieName, "");
                        PrevNode[child] = node;
                        PrevMovie[childMovieName] = nodeMovieName;
                    }
                }
            }

            return (Distance[destination], Strength[destination]);
        }

        private static (List<string>, List<string>) GetPath(string actor1, string actor2)
        {
            // PrevNode dictionary contains the previous node of every node in the optimal path
            // PrevMovie dictionary contains the previous movie of every movie in the optimal path
            // starting from the destination we traverse back using these dictionaries till we reach the source

            List<string> moviesPath = new();
            List<string> actorsPath = new();

            string node = actor2;
            string movie = finalMovie;

            do
            {
                // the previous of source is always empty string
                // that means we reached the source
                if (node == "") break;

                // add node to the nodes list
                actorsPath.Add(node);

                // go to previous node
                if (!PrevNode.ContainsKey(node)) PrevNode.Add(node, "");
                node = PrevNode[node];
            }
            while (true);

            do
            {
                // the previous of source is always empty string
                // that means we reached the source
                if (movie == "") break;

                // add movie to the movies list
                moviesPath.Add(movie);

                // go to previous movie
                if (!PrevMovie.ContainsKey(movie)) PrevMovie.Add(movie, "");
                movie = PrevMovie[movie];
            }
            while (true);

            // after we put every node and movie in thier lists we reverse them to be in required order
            actorsPath.Reverse();
            moviesPath.Reverse();

            return (actorsPath, moviesPath);
        }

        public static (int, int, List<string>, List<string>) Query(string actor1, string actor2, bool optimization)
        {
            Distance = new();
            Strength = new();
            PrevMovie = new();
            PrevNode = new();

            (int, int) ans = Dijk(actor1, actor2, optimization);
            int degree = ans.Item1;
            int strength = ans.Item2;

            (List<string>, List<string>) paths = GetPath(actor1, actor2);

            return (degree, strength, paths.Item1, paths.Item2);
        }
    }
}
