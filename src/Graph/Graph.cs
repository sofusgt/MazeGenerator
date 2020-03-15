using System.Collections.Generic;

namespace MazeGenerator {
    public class Graph {
        private List<Vertex> vertices;
        
        public List<Vertex> Vertices {
            get {
                return vertices;
            }
        }


        public Graph () {
            vertices = new List<Vertex>();
        }


        public bool Contains (Vertex x) {
            return vertices.Contains(x);
        } 

        
        public void AddVertex (Vertex x) {
            vertices.Add(x);
        }


        public void RemoveVertex (Vertex x) {
            vertices.Remove(x);
        }
    }
}