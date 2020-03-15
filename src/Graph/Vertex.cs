using System.Threading;

using System.Collections.Generic;


namespace MazeGenerator {
    public class Vertex{
        private bool visited;
        private int value;
        private List<Vertex> connections;
        private bool inPath;
        private static int staticID = 0;
        private int id;
        private Position position;
        

        public List<Vertex> Neighbours {
            get {
                return connections;
            }
        }
        public int Value {
            get {
                return value;
            }
        }
        public int ID {
            get {
                return id;
            }
        }
        public Position Position {
            get {
                return position;
            }
        }


        public Vertex (int x, int y) {
            connections = new List<Vertex>();
            visited = false;
            inPath = false;
            staticID++;
            id = staticID;
            position = new Position(x, y);
        }


        public bool IsVisited() {
            return visited;
        }


        public void Visit () {
            visited = true;
        }


        public void UnVisit () {
            visited = false;
        }


        public void Connect(Vertex vertex) {
            connections.Add(vertex);
        }


        public bool IsConnected(Vertex vertex) {
            return connections.Contains(vertex);
        }


        public void AddEdge (Vertex vertex) {
            connections.Add(vertex);
        }


        public void RemoveEdge (Vertex vertex) {
            connections.Remove(vertex);
        }


        public void SetValue (int val) {
            value = val;
        }


        public bool IsInPath () {
            return inPath;
        }


        public void MakePath () {
            inPath = true;
        }


        public void RemoveFromPath () {
            inPath = false;
        }
    }
}