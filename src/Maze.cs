using System.IO;
using System.Collections.Generic;
using System;
using System.Drawing;

namespace MazeGenerator {
    public class Maze {
        private int mazeSize;
        private Vertex[,] mazeGrid;
        private Graph maze;
        private Random rand = new Random();

        public Vertex[,] MazeGrid {
            get {
                return mazeGrid;
            }
        }

        public Maze (int mazeSize) {
            this.mazeSize = mazeSize;
            maze = new Graph();
            mazeGrid = new Vertex[mazeSize, mazeSize];

            // Initialize maze grid.
            for (int i = 0; i < mazeSize; i++) {
                for (int j = 0; j < mazeSize; j++) {
                    mazeGrid[i,j] = new Vertex(i, j);
                }
            }
        }


        public void LoopErasedRandomWalk () {
            maze.AddVertex(mazeGrid[0, 0]);
            mazeGrid[0, 0].Visit();

            for (int i = 0; i < mazeSize; i++) {
                for (int j = 0; j < mazeSize; j++) {
                    if (!mazeGrid[i, j].IsVisited) {
                        LoopErasedRandomWalk(mazeGrid[i, j]);
                    }  
                }
            }
        }


        private void LoopErasedRandomWalk (Vertex vertx) {
            List<Vertex> path = new List<Vertex>() {vertx};
            List<Position> directions = GetDirections(vertx);
            Position randomDirection = directions[rand.Next(directions.Count)];
            Vertex randomVertex = mazeGrid[randomDirection.X, randomDirection.Y];
            bool pathNotFound = true;

            while (pathNotFound) {
                if (randomVertex.IsVisited) { // Vertex part of maze, add vertices in path to maze then visit all vertices in path.
                    path.Add(randomVertex);
                    GraphOperations.AddVertex(maze, path[0]);
                    for (int i = 0; i < path.Count - 1; i++) {
                        GraphOperations.AddVertex(maze, path[i + 1]);
                        GraphOperations.Connect(path[i], path[i + 1]);
                    }

                    foreach (Vertex v in path) {
                        v.Visit();
                    }

                    pathNotFound = false;
                } else if (path.Contains(randomVertex)) { // Vertex part of path, remove loop and start over at loop base.
                    int loopBase = path.FindIndex(x => x.ID == randomVertex.ID);
                    path.RemoveRange(loopBase + 1, path.Count - (loopBase + 1));

                    randomVertex = path[loopBase];
                    directions = GetDirections(randomVertex);
                    randomDirection = directions[rand.Next(directions.Count)];
                    randomVertex = mazeGrid[randomDirection.X, randomDirection.Y];
                } else { // Vertex not part of maze or path, so add to path.
                    path.Add(randomVertex);
                    directions = GetDirections(randomVertex);
                    randomDirection = directions[rand.Next(directions.Count)];
                    randomVertex = mazeGrid[randomDirection.X, randomDirection.Y];
                }
            }
        }


        private List<Position> GetDirections (Vertex vertex) 
        {
            List<int> xDir = new List<int>();
            List<int> yDir = new List<int>();
            List<Position> directions = new List<Position>();

            if (vertex.Position.X == 0) 
            {
                xDir.Add(1);
            } else if (vertex.Position.X == mazeSize - 1) 
            {
                xDir.Add(-1);
            } else 
            {
                xDir.Add(1);
                xDir.Add(-1);
            }

            if (vertex.Position.Y == 0) 
            {
                yDir.Add(1);
            } else if (vertex.Position.Y == mazeSize - 1) 
            {
                yDir.Add(-1);
            } else 
            {
                yDir.Add(1);
                yDir.Add(-1);
            }

            foreach (int x in xDir) 
            {
                directions.Add(new Position(x, 0));
            }
            foreach (int y in yDir) 
            {
                directions.Add(new Position(0, y));
            }

            for (int i = 0; i < directions.Count; i++)
            {
                directions[i] = new Position(Position.AddPositions(directions[i], vertex.Position));
            }
            
            return directions;
        }


        public override String ToString() 
        {
            String result = "";
            for (int i = 0; i < mazeSize; i++)
            {
                for (int j = 0; j < mazeSize; j++)
                {
                    if (mazeGrid[i,j].IsVisited) 
                    {
                        result += "V ";
                    } else
                    {
                        result += "X ";
                    }
                }
                result += "\n";
            }

            return result;
        }


        public void GenerateImage () 
        {
            int imageSize = mazeSize * 2 + 1;
            using (Bitmap bmp = new Bitmap(imageSize, imageSize)) {
                using (Graphics g = Graphics.FromImage(bmp)) {
                    g.Clear(Color.Black);
                }

                for (int x = 1; x < imageSize - 1; x++) {
                    for (int y = 1; y < imageSize - 1; y++) {
                        if ((x - 1) % 2 == 0 && (y - 1) % 2 == 0) {
                            if (mazeGrid[(x-1) / 2, (y-1) / 2].IsVisited) {
                                bmp.SetPixel(x, y, Color.White);
                            }
                        } else if ((x - 1) % 2 == 0) {
                            if (mazeGrid[(x-1) / 2, (y-2) / 2].IsConnected(mazeGrid[(x-1) / 2, y / 2])) {
                                bmp.SetPixel(x, y, Color.White);
                            }
                        } else if ((y - 1) % 2 == 0) {
                            if (mazeGrid[(x-2) / 2, (y-1) / 2].IsConnected(mazeGrid[x / 2, (y-1) / 2])) {
                                bmp.SetPixel(x, y, Color.White);
                            }
                        }
                    }
                }
                
                bmp.SetPixel(1, 1, Color.Green);
                bmp.SetPixel(imageSize - 2, imageSize - 2, Color.Red);
                bmp.Save("./maze.png");
            }
        }
    }
}