using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    public class PlayerFixed
    {
        public string Name { get; set; }
        public int Position { get; set; }

        public PlayerFixed(string name)
        {
            Name = name;
            Position = 0;
        }

        public void Move(int steps)
        {
            Position += steps;
            Console.WriteLine($"{Name} moved to position {Position}.");
        }

        public void Jump(int destination)
        {
            Position = destination;
        }
    }
}
