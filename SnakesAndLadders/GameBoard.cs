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
        public const int Size = 100;

        private static Random _random = new Random();

        public int NumSnakes;
        public int NumLadders;
        private static int[] _snakes;
        private static int[] _ladders;

        private static int[] _goldSquareArray;

        public GameBoard(int numSnakes, int numLadders)
        {
            this.NumSnakes = numSnakes;
            this.NumLadders = numLadders;
            _snakes = new int[numSnakes];
            _ladders = new int[numLadders];
            _goldSquareArray = new int[2];
            RandomNumSnakes();
            RandomNumLadders();
            RandomGoldenSquares();
        }
        //each run get random board
        // Snakes and Ladders positions
        public void RandomNumSnakes()
        {
            int counter = 0;
            while (counter < NumSnakes)
            {
                int randomNum = _random.Next(11, 100);
                if (_snakes[counter] == default)
                {
                    _snakes[counter] = randomNum;
                    Console.WriteLine($"num of snakes {_snakes[counter]}");
                    counter++;
                }
            }
        }

        public void RandomNumLadders()
        {
            int counter = 0;
            while (counter < NumLadders)
            {
                int randomNum = _random.Next(11, 100);
                if (_ladders[counter] == default && Array.IndexOf(_snakes, randomNum) == -1)
                {
                    _ladders[counter] = randomNum;
                    Console.WriteLine($"num of ladders {_ladders[counter]}");
                    counter++;
                }
            }
        }
        public void RandomGoldenSquares()
        {
            int counter = 0;
            while (counter < _goldSquareArray.Length)
            {
                int randomNum = _random.Next(11, 100);
                if (_goldSquareArray[counter] == default && Array.IndexOf(_snakes, randomNum) == -1 && Array.IndexOf(_ladders, randomNum) == -1)
                {
                    _goldSquareArray[counter] = randomNum;
                    counter++;
                }
            }
        }

        public void SwapPosition(Player position1,Player position2)
        {
            if (position1.Position > position2.Position)
            {
                int tempswap = position1.Position;
                position1.Position = position2.Position;
                position2.Position = tempswap;
            }
        }

        public int RollDice()
        {
            return _random.Next(1, 7);
        }

        public static int CheckPosition(int position)
        {
            // Check if the player lands on a snake or ladder position
            if (Array.IndexOf(_snakes, position) != -1)
            {
                Console.WriteLine($"Oops! You landed on a snake! Go back to position {position - 10}.");
                position -= 10;
            }
            else if (Array.IndexOf(_ladders, position) != -1)
            {
                Console.WriteLine($"Congratulations! You landed on a ladder! Climb up to position {position + 10}.");
                position += 10;
            }
            else if (Array.IndexOf(_goldSquareArray, position) != -1)
            {
                Console.WriteLine($"Congratulations! you landed on golden square you switch places with the leading player {position}");
            }

            return position;
        }
    }
}
