using System.Net.Mime;
using System.Collections.Generic;
using System;
using System.Drawing;

namespace MazeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int mazeSize = 100;

            Maze maze = new Maze(mazeSize);
            maze.LoopErasedRandomWalk();

            maze.GenerateImage();
        }
    }
}
