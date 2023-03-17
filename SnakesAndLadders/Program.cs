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
            Console.WriteLine("Welcome to Ladders and Snakes Game!");
            int randomOrFixed;
            do
            {
                Console.Write("for Fixed play enter 1 for random play enter 2: ");
            } while (!int.TryParse(Console.ReadLine(), out randomOrFixed) || randomOrFixed <= 0 || randomOrFixed >= 3);

            if (randomOrFixed == 2)
            {
                //get snake and ladder parameters
                int numSnakes, numLadders;
                do
                {
                    Console.Write("enter number of snakes(1-10): ");
                } while (!int.TryParse(Console.ReadLine(), out numSnakes) || numSnakes <= 0 || numSnakes >= 11);

                do
                {
                    Console.Write("enter number of Ladders(1-10): ");
                } while (!int.TryParse(Console.ReadLine(), out numLadders) || numLadders <= 0 || numLadders >= 11);

                // Create a new game board
                GameBoard board = new GameBoard(numSnakes, numLadders);

                //start the game loop
                board.StartGame();

                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("you choose Fixed play");
                //FixedBoard fixedBoard = new FixedBoard();
                //fixedBoard.StartGameFixed();
                FixedBoard game = new FixedBoard("Player 1", "Player 2");
                game.RunFixedBoard();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }

        }
    }
}
