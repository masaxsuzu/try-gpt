using System.Linq;
using TryGpt;
using Xunit;

namespace TryGpt.Tests
{
    public class TestGraph
    {
        private Graph graph;

        public TestGraph()
        {
            graph = new Graph();
        }

        [Fact]
        public void TestAddVertex()
        {
            graph.AddVertex(1);
            Assert.Empty(graph.GetAdjacentVertices(1));
        }

        [Fact]
        public void TestAddEdge()
        {
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddEdge(1, 2);
            Assert.Equal(new int[] { 2 }, graph.GetAdjacentVertices(1).ToArray());
        }

        [Fact]
        public void TestGetAdjacentVertices()
        {
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            Assert.Equal(new int[] { 2, 3 }, graph.GetAdjacentVertices(1).ToArray());
        }

        [Fact]
        public void TestIsCyclicWithCyclicGraph()
        {
            // Create a cyclic graph
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 2);

            Assert.True(graph.IsCyclic());
        }

        [Fact]
        public void TestIsCyclicWithAcyclicGraph()
        {
            // Create an acyclic graph
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);

            Assert.False(graph.IsCyclic());
        }

        [Fact]
        public void TestFindLongestPath()
        {
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddVertex(6);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 6);
            Assert.Equal(4, graph.FindLongestPath());
        }

        [Fact]
        public void TestToStringWithAcyclicGraph()
        {
            var graph = new Graph();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 5);

            var expected = @"digraph G {
    1 -> 2;
    2 -> 3;
    2 -> 4;
    3 -> 5;
}";

            Assert.Equal(expected.Trim(), graph.ToString().Trim());
        }

        [Fact]
        public void TestToStringWithCyclicGraph()
        {
            var graph = new Graph();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 2);

            var expected = @"digraph G {
    1 -> 2;
    2 -> 3;
    3 -> 4;
    4 -> 2;
}";

            Assert.Equal(expected.Trim(), graph.ToString().Trim());
        }

    }
}
