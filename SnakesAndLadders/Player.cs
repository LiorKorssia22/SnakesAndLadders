using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    public class Player
    {
        public string Name { get; private set; }
        public int Position { get; set; }

        public Player(string name)
        {
            Name = name;
            Position = 0;
        }

        public void Move(int steps)
        {
            Position += steps;
            Console.WriteLine($"{Name} moved to position {Position}.");

            // Check if the player lands on a snake or ladder position
            Position = GameBoard.CheckPosition(Position);
        }
    }
}
