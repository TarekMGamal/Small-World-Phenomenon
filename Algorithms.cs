using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    internal static class Algorithms
    {
        static int VisIndex;
        static Dictionary<string, List<string>> AdjList;
        static Dictionary<string, bool> IsActor;
        static Dictionary<string, int> Visited;
        static Dictionary<string, int> Distance;
        static Dictionary<string, string> Prev;

        public static void Prepare(Dictionary<string, List<string>> adjList,
                                   Dictionary<string, bool> isActor,
                                   Dictionary<string, int> visited,
                                   Dictionary<string, int> dis,
                                   Dictionary<string, string> pre)
        {
            VisIndex = 0;
            AdjList = adjList;
            IsActor = isActor;
            Distance = dis;
            Prev = pre;
            Visited = visited;
        }

        private static int Bfs(string node, string dest)
        {
            Visited[node] = VisIndex;
            Distance[node] = 0;

            Queue<string> queue = new();
            queue.Enqueue(node);
            
            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                  
                if (node == dest)
                {
                    return Distance[dest];
                }

                foreach(var child in AdjList[node])
                {
                    if (Visited[child] == VisIndex) continue;

                    Visited[child] = VisIndex;

                    Distance[child] = Distance[node];
                    if (!IsActor[node]) Distance[child]++;

                    Prev[child] = node;

                    queue.Enqueue(child);
                }
            }

            return 0;
        }

        private static List<string> GetPath(string actor1, string actor2)
        {
            List<string> path = new();
            string node = actor2;

            do
            {
                if (!IsActor[node])
                {
                    path.Add(node);
                }
                node = Prev[node];
            }
            while (node != actor1);
            
            return path;
        }

        private static int GetStrength()
        {
            // Todo
            return 0;
        }

        public static (int, int, List<string>) Query(string actor1, string actor2)
        {
            VisIndex++;
            int degree = Bfs(actor1, actor2);
            int strength = GetStrength();
            List<string> path = GetPath(actor1, actor2);

            return (degree, strength, path);
        }
    }
}
