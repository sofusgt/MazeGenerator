using System.Collections.Generic;

namespace MazeGenerator {
    public static class GraphOperations {
        public static bool Adjacent (Vertex vertex1, Vertex vertex2) {
            return vertex1.IsConnected(vertex2);
        }


        public static List<Vertex> Neighbours (Vertex vertex) {
            return vertex.Neighbours;
        }


        public static void Connect(Vertex vertex1, Vertex vertex2){
            vertex1.Connect(vertex2);
            vertex2.Connect(vertex1);
        }


        public static void AddVertex (Graph graph, Vertex vertex) {
            if (!graph.Contains(vertex)) {
                graph.AddVertex(vertex);
            }
        }


        public static void RemoveVertex (Graph graph, Vertex vertex) {
            if (graph.Contains(vertex)) {
                graph.RemoveVertex(vertex);
            }
        }


        public static void AddEdge (Vertex vertex1, Vertex vertex2) {
            vertex1.AddEdge(vertex2);
            vertex2.AddEdge(vertex1);
        }


        public static void RemoveEdge (Vertex vertex1, Vertex vertex2) {
            vertex1.RemoveEdge(vertex2);
            vertex2.RemoveEdge(vertex1);
        }


        public static int GetVertexValue (Vertex vertex) {
            return vertex.Value;
        }


        public static void SetVertexValue (Vertex vertex, int value) {
            vertex.SetValue(value);
        }
    }
}