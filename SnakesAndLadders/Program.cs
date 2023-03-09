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

            Console.WriteLine("enter number of snakes:");
            int numSnakes = int.Parse(Console.ReadLine());
            Console.WriteLine("enter number of Ladders:");
            int numLadders = int.Parse(Console.ReadLine());
            // Create a new game board
            GameBoard board = new GameBoard(numSnakes, numLadders);


            // Create two players
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            // Start the game loop
            while (true)
            {
                // Player 1 turn
                Console.WriteLine($"{player1.Name}, press any key to roll the dice.");
                Console.ReadKey();

                int rollValue1 = board.RollDice();
                int rollValue2 = board.RollDice();
                Console.WriteLine($"{player1.Name} rolled a {rollValue1} and {rollValue2}.");
                int sum = rollValue1 + rollValue2;

                player1.Move(sum);
                if (player1.Position == 70 || player1.Position == 30)
                {
                    board.SwapNum(player1.Position, player2.Position);
                    Console.WriteLine($"swap beetween the players: player1 {player1.Position} and platyer2 {player2.Position}");
                }

                if (player1.Position >= GameBoard.Size)
                {
                    Console.WriteLine($"{player1.Name} won the game!");
                    break;
                }

                // Player 2 turn
                Console.WriteLine($"{player2.Name}, press any key to roll the dice.");
                Console.ReadKey();

                rollValue1 = board.RollDice();
                rollValue2 = board.RollDice();
                Console.WriteLine($"{player2.Name} rolled a {rollValue1} and {rollValue2}.");
                sum = rollValue1 + rollValue2;

                player2.Move(sum);

                //if (player2.Position == 70 || player2.Position == 30)
                //{
                //    //board.SwapNum( player1.Position, player2.Position);
                //    board.SwapNum(player1.Position, player2.Position);
                //    Console.WriteLine();
                //}

                if (player2.Position >= GameBoard.Size)
                {
                    Console.WriteLine($"{player2.Name} won the game!");
                    break;
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
