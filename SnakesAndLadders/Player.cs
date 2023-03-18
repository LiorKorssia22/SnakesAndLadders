using System;

namespace SnakesAndLadders
{
    public class Player
    {
        public string Name { get; set; }
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
        }

        public void JumpToDestination(int destination)
        {
            Position = destination;
        }
    }
}
