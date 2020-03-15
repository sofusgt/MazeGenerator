using System.IO;
using System.Collections.Generic;
using System;

namespace MazeGenerator {
    public class Maze {
        private int length;
        private Vertex[,] mazeGrid;
        private Graph maze;
        private Random rand = new Random();

        public Maze (int length) {
            this.length = length;
            maze = new Graph();
            mazeGrid = new Vertex[length, length];

            // Initialize maze grid.
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < length; j++) {
                    mazeGrid[i,j] = new Vertex(i, j);
                }
            }
        }


        public void LoopErasedRandomWalk () 
        {
            maze.AddVertex(mazeGrid[0, 0]);
            mazeGrid[0, 0].Visit();

            for (int i = 0; i < length; i++) 
            {
                for (int j = 0; j < length; j++) 
                {
                    if (!mazeGrid[i, j].IsVisited()) 
                    {
                        LoopErasedRandomWalk(mazeGrid[i, j]);
                    }  
                }
            }
        }


        private void LoopErasedRandomWalk (Vertex vertex) 
        {
            List<Vertex> path = new List<Vertex>() {vertex};
            List<Position> directions = GetDirections(vertex);
            Position randomDirection = directions[rand.Next(directions.Count)];
            Vertex randomVertex = mazeGrid[randomDirection.X, randomDirection.Y];
            bool pathNotFound = true;

            while (pathNotFound) 
            {
                if (randomVertex.IsVisited()) 
                { // Vertex part of maze, add vertices in path to maze then visit all vertices in path.
                    path.Add(randomVertex);
                    GraphOperations.AddVertex(maze, path[0]);
                    for (int i = 0; i < path.Count - 2; i++) 
                    {
                        GraphOperations.AddVertex(maze, path[i + 1]);
                        GraphOperations.Connect(path[i], path[i + 1]);
                    }

                    foreach (Vertex v in path) 
                    {
                        v.Visit();
                    }

                    pathNotFound = false;
                } else if (randomVertex.IsInPath()) 
                { // Vertex part of path, remove loop and start over at loop base.
                    int loopBase = path.FindIndex(x => x.ID == randomVertex.ID);
                    for (int i = loopBase + 1; i < path.Count; i++)
                    {
                        path[i].RemoveFromPath();
                    }
                    path.RemoveRange(loopBase + 1, path.Count - (loopBase + 1));

                    randomVertex = path[loopBase];
                    directions = GetDirections(randomVertex);
                    randomDirection = directions[rand.Next(directions.Count)];
                    randomVertex = mazeGrid[randomDirection.X, randomDirection.Y];
                } else 
                { // Vertex not part of maze or path, so add to path.
                    path.Add(randomVertex);
                    randomVertex.MakePath();
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
            } else if (vertex.Position.X == length - 1) 
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
            } else if (vertex.Position.Y == length - 1) 
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
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (mazeGrid[i,j].IsVisited()) 
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
    }
}