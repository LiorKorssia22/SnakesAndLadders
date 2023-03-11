using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    public class GameBoard
    {
        private const int _size = 100;

        private static Random _random = new Random();

        public int NumSnakes;
        public int NumLadders;

        private static int[] _snakes;
        private static int[] _ladders;
        private static int[] _goldSquareArray;

        // Create two players
        public static Player player1 = new Player("Player 1");
        public static Player player2 = new Player("Player 2");

        public GameBoard(int numSnakes, int numLadders)
        {
            this.NumSnakes = numSnakes;     // get numSnakes parameter
            this.NumLadders = numLadders;   // get numLadders parameter
            _snakes = new int[numSnakes];   // set numSnakes length
            _ladders = new int[numLadders]; // set numLadders length
            _goldSquareArray = new int[2];  // set goldSquare length
            RandomNumSnakes();              //create random snakes
            RandomNumLadders();             //create random ladders
            RandomGoldenSquares();          //create random Golden Squares
        }
        //each run get random board
        // Snakes, Ladders and goldSquare positions
        private void RandomNumSnakes()
        {
            int counter = 0;
            while (counter < NumSnakes)
            {
                int randomNum = _random.Next(30, 100);
                //check position is empty and not the same (as snakes)
                if (_snakes[counter] == default && Array.IndexOf(_snakes, randomNum) == -1)
                {
                    _snakes[counter] = randomNum;
                    Console.WriteLine($"num of snakes {_snakes[counter]}");
                    counter++;
                }
            }
        }
        private void RandomNumLadders()
        {
            int counter = 0;
            while (counter < NumLadders)
            {
                int randomNum = _random.Next(11, 71);
                //check position is empty and not the same (as ladder and snake)
                if (_ladders[counter] == default && Array.IndexOf(_ladders, randomNum) == -1 && Array.IndexOf(_snakes, randomNum) == -1)
                {
                    _ladders[counter] = randomNum;
                    Console.WriteLine($"num of ladders {_ladders[counter]}");
                    counter++;
                }
            }
        }
        private void RandomGoldenSquares()
        {
            int counter = 0;
            while (counter < _goldSquareArray.Length)
            {
                int randomNum = _random.Next(11, 100);
                //check position is empty and not the same (as ladder and snake and gold squre)
                if (_goldSquareArray[counter] == default && Array.IndexOf(_goldSquareArray, randomNum) == -1 && Array.IndexOf(_snakes, randomNum) == -1 && Array.IndexOf(_ladders, randomNum) == -1)
                {
                    _goldSquareArray[counter] = randomNum;
                    Console.WriteLine($"num of golden square {_goldSquareArray[counter]}");
                    counter++;
                }
            }
        }
        public void StartGame()
        {
            // Start the game loop
            while (true)
            {
                // Player 1 turn
                Console.WriteLine($"{player1.Name}, press any key to roll the dice.");
                Console.ReadKey();

                int rollValue1 = RollDice();
                int rollValue2 = RollDice();
                Console.WriteLine($"{player1.Name} rolled a {rollValue1} and {rollValue2}.");
                int sum = rollValue1 + rollValue2;
                player1.Move(sum);
                //check gold square player1 and swap
                if (player1.Position == _goldSquareArray[0] || player1.Position == _goldSquareArray[1])
                {
                    if (player2.Position > player1.Position)
                    {
                        Console.WriteLine("*You landed on a goldSquare!  You switch places with Player 2.*");
                        SwapPositions(player1, player2);
                    }
                    else
                    {
                        Console.WriteLine("You landed on a goldSquare! but NOT SWITCH(YOU BIGGER)");
                    }
                }
                //check win player1
                if (player1.Position >= _size)
                {
                    Console.WriteLine($"{player1.Name} won the game!");
                    break;
                }

                // Player 2 turn
                Console.WriteLine($"{player2.Name}, press any key to roll the dice.");
                Console.ReadKey();

                rollValue1 = RollDice();
                rollValue2 = RollDice();
                Console.WriteLine($"{player2.Name} rolled a {rollValue1} and {rollValue2}.");
                sum = rollValue1 + rollValue2;
                player2.Move(sum);
                //check gold square player2 and swap
                if (player2.Position == _goldSquareArray[0] || player2.Position == _goldSquareArray[1])
                {
                    if (player1.Position > player2.Position)
                    {
                        Console.WriteLine("*You landed on a goldSquare!  You switch places with Player 1.*");
                        SwapPositions(player2, player1);
                    }
                    else
                    {
                        Console.WriteLine("You landed on a goldSquare! but NOT SWITCH(YOU BIGGER)");
                    }
                }
                //check win player2
                if (player2.Position >= _size)
                {
                    Console.WriteLine($"{player2.Name} won the game!");
                    break;
                }
            }
        }

        private static void SwapPositions(Player currentPlayer, Player otherPlayer)
        {
            int temp = currentPlayer.Position;
            currentPlayer.Position = otherPlayer.Position;
            otherPlayer.Position = temp;
        }

        private int RollDice()
        {
            return _random.Next(1, 7);
        }

        public static int CheckPosition(int position)
        {
            int rndNum = _random.Next(10, 21);
            // Check if the player lands on a snake or ladder position
            if (Array.IndexOf(_snakes, position) != -1)
            {
                Console.WriteLine($"Oops! You landed on a snake! Go back to position {position - rndNum}.");
                position -= rndNum;
            }
            else if (Array.IndexOf(_ladders, position) != -1)
            {
                Console.WriteLine($"Congratulations! You landed on a ladder! Climb up to position {position + rndNum}.");
                position += rndNum;
            }
            return position;
        }
    }
}
