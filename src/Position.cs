using Microsoft.VisualBasic.CompilerServices;
using System;


namespace MazeGenerator {
    public class Position {
        private int x;
        private int y;

        public int X {
            get {
                return x; 
            }
        }
        public int Y {
            get {
                return y;
            }
        }

        public Position (int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Position (Position position) {
            this.x = position.X;
            this.y = position.Y;
        }

        public static Position AddPositions (Position pos1, Position pos2) {
            return new Position(pos1.X + pos2.X, pos1.Y + pos2.Y);
        }

        public override String ToString() {
            return String.Format("({0}, {1})", x, y);
        }
    }
}