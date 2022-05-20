using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    internal static class Algorithms
    {
        static Dictionary<string, List<(string, string)>> AdjList;
        static Dictionary<string, bool> Visited = new();
        static Queue<(string, string)> queue = new();
        static List<string> moviesPath = new();
        static List<string> actorsPath = new();

        public static void Prepare(Dictionary<string, List<(string, string)>> adjList)
        {
            AdjList = adjList;
        }

        static (int, int) Bfs(string actor1, string actor2)
        {
            if (!Visited.ContainsKey(actor1)) Visited.Add(actor1, false);
            Visited[actor1] = true;

            queue.Enqueue((actor1, ""));
            while (queue.Count > 0)
            {
                var front = queue.Dequeue();
                string node = front.Item1;
                
                //List<(string, string)> list = AdjList[node];
                foreach(var item in AdjList[node])
                {
                    string child = item.Item1;
                    string childMovieName = item.Item2;

                    if (!Visited.ContainsKey(child)) Visited.Add(child, false);
                    if (Visited[child] == true) continue;
                    else
                    {
                        queue.Enqueue((child, childMovieName));
                        Visited[child] = true;                        
                    }
                }
            }

            return (0, 0);
        }

        private static (List<string>, List<string>) GetPath(string actor1, string actor2)
        {
            return (actorsPath, actorsPath);
        }

        public static (int, int) Query(string actor1, string actor2)
        {
            Visited.Clear();
            return Bfs(actor1, actor2);
        }
    }
}
