using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Snakes and Ladders Game!");
            
            int numSnakes, numLadders;
            do
            {
                Console.Write("enter number of snakes: ");
            } while (!int.TryParse(Console.ReadLine(), out numSnakes) || numSnakes <= 0);

            do
            {
                Console.Write("enter number of Ladders: ");
            } while (!int.TryParse(Console.ReadLine(), out numLadders) || numLadders <= 0);

            // Create a new game board
            GameBoard board = new GameBoard(numSnakes, numLadders);

            //start the game loop
            board.StartGame();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
