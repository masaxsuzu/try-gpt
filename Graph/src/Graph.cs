using System.Collections.Generic;
using System.Text;

namespace TryGpt
{
    public class Graph
    {
        private readonly Dictionary<int, List<int>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<int, List<int>>();
        }

        public void AddVertex(int vertex)
        {
            adjacencyList[vertex] = new List<int>();
        }

        public void AddEdge(int startVertex, int endVertex)
        {
            if (!adjacencyList.ContainsKey(startVertex))
            {
                adjacencyList[startVertex] = new List<int>();
            }
            adjacencyList[startVertex].Add(endVertex);
        }

        public List<int> GetAdjacentVertices(int vertex)
        {
            if (!adjacencyList.ContainsKey(vertex))
            {
                return new List<int>();
            }
            return adjacencyList[vertex];
        }

        public bool IsCyclic()
        {
            var visited = new HashSet<int>();
            var recursionStack = new HashSet<int>();

            foreach (var vertex in adjacencyList.Keys)
            {
                if (IsCyclicUtil(vertex, visited, recursionStack))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsCyclicUtil(int vertex, HashSet<int> visited, HashSet<int> recursionStack)
        {
            if (recursionStack.Contains(vertex))
            {
                return true;
            }

            if (visited.Contains(vertex))
            {
                return false;
            }

            visited.Add(vertex);
            recursionStack.Add(vertex);

            foreach (var adjacentVertex in adjacencyList[vertex])
            {
                if (IsCyclicUtil(adjacentVertex, visited, recursionStack))
                {
                    return true;
                }
            }

            recursionStack.Remove(vertex);

            return false;
        }

        public int FindLongestPath()
        {
            var longestPaths = new Dictionary<int, int>();
            var visited = new HashSet<int>();

            foreach (var vertex in adjacencyList.Keys)
            {
                FindLongestPathUtil(vertex, longestPaths, visited);
            }

            return longestPaths.Values.Max();
        }

        private int FindLongestPathUtil(int vertex, Dictionary<int, int> longestPaths, HashSet<int> visited)
        {
            if (longestPaths.ContainsKey(vertex))
            {
                return longestPaths[vertex];
            }

            visited.Add(vertex);

            int longestPath = 0;

            foreach (var adjacentVertex in adjacencyList[vertex])
            {
                if (!visited.Contains(adjacentVertex))
                {
                    int pathLength = FindLongestPathUtil(adjacentVertex, longestPaths, visited);
                    longestPath = Math.Max(longestPath, pathLength);
                }
            }

            longestPaths[vertex] = longestPath + 1;

            return longestPaths[vertex];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("digraph G {");
            foreach (int v in adjacencyList.Keys)
            {
                foreach (int w in adjacencyList[v])
                {
                    sb.AppendLine($"    {v} -> {w};");
                }
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
        
    }
}
