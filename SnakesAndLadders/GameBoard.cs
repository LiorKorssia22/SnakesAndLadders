using System;
using System.Collections.Generic;

namespace SnakesAndLadders
{
    public class GameBoard
    {
        private const int _size = 100;

        private static Random _random = new Random();

        public int NumSnakes;
        public int NumLadders;

        private static int[] _snakes;
        private static int[] _snakesEnd;
        private static int[] _ladders;
        private static int[] _laddersEnd;
        private static int[] _goldSquareArray;

        //Checks in each slot I have only one element (startSnake/endSnake/startLadder/endLadder/goldSquare)
        private static List<int> _validationList = new List<int>();

        // Create two players
        public static Player player1 = new Player("Player 1");
        public static Player player2 = new Player("Player 2");

        public GameBoard(int numSnakes, int numLadders)
        {
            this.NumSnakes = numSnakes;     // get numSnakes parameter
            this.NumLadders = numLadders;   // get numLadders parameter
            _snakes = new int[numSnakes];   // set numSnakes length and start point of snake
            _snakesEnd = new int[numSnakes];// set end point of snake
            _ladders = new int[numLadders]; // set numLadders length and start point of ledder
            _laddersEnd = new int[numLadders];// set end point of ladder
            _goldSquareArray = new int[2];  // set goldSquare length
            RandomNumSnakes();              //create random snakes
            RandomNumLadders();             //create random ladders
            RandomGoldenSquares();          //create random Golden Squares
        }
        //each run get random board
        // Snakes, Ladders and goldSquare positions
        static int randPositionOfSnakes = _random.Next(10, 21);
        private void RandomNumSnakes()
        {
            int counter = 0;
            while (counter < NumSnakes)
            {
                int randomNum = _random.Next(30, 100);
                int endSnake = randomNum - randPositionOfSnakes;
                //check position is empty and not the same (as snakes)
                if (_snakes[counter] == default && _snakesEnd[counter] == default &&
                    _validationList.IndexOf(randomNum) == -1 && _validationList.IndexOf(endSnake) == -1)
                {
                    //add index of randomnum and endsnake for validation
                    _validationList.AddRange(new List<int>() { randomNum, endSnake });
                    _snakes[counter] = randomNum;
                    _snakesEnd[counter] = endSnake;
                    Console.WriteLine($"num of snakes for {_snakes[counter]} to {_snakesEnd[counter]}");
                    counter++;
                }
            }
        }
        static int randPositionOfLadders = _random.Next(10, 21);
        private void RandomNumLadders()
        {
            int counter = 0;
            while (counter < NumLadders)
            {
                int randomNum = _random.Next(11, 71);
                int endLadders = randomNum + randPositionOfLadders;
                //check position is empty and not the same (as ladder and snake)
                if (_ladders[counter] == default && _laddersEnd[counter] == default &&
                    _validationList.IndexOf(randomNum) == -1 && _validationList.IndexOf(endLadders) == -1)
                {
                    //add index of randomnum and endLadders for validation
                    _validationList.AddRange(new List<int>() { randomNum, endLadders });
                    _ladders[counter] = randomNum;
                    _laddersEnd[counter] = endLadders;
                    Console.WriteLine($"num of ladders from {_ladders[counter]} to {_laddersEnd[counter]}");
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
                if (_goldSquareArray[counter] == default && _validationList.IndexOf(randomNum) == -1)
                {
                    //add index of randomnum for validation
                    _validationList.AddRange(new List<int>() { randomNum });
                    _goldSquareArray[counter] = randomNum;
                    Console.WriteLine($"num of golden square {_goldSquareArray[counter]}");
                    counter++;
                }
            }
        }
        //I debated whether to put the StartGame in the gameboard, or in the program, but I want the board to hold the game, in my opinion, this is a more correct approach.
        public void StartGame()
        {
            // Start the game loop
            while (true)
            {
                // Player 1 turn
                RunPlayer(player1);
                CheckSwapPlayer(player1, player2);
                //check win player1
                if (player1.Position >= _size)
                {
                    Console.WriteLine($"{player1.Name} won the game!");
                    break;
                }
                CheckPosition(player1.Position, player1);
                // Player 2 turn
                RunPlayer(player2);
                CheckSwapPlayer(player2, player1);
                //check win player2
                if (player2.Position >= _size)
                {
                    Console.WriteLine($"{player2.Name} won the game!");
                    break;
                }
                CheckPosition(player2.Position, player2);
            }
        }

        private void CheckSwapPlayer(Player currentPlayer, Player otherPlayer)
        {
            if (currentPlayer.Position == _goldSquareArray[0] || currentPlayer.Position == _goldSquareArray[1])
            {
                if (otherPlayer.Position > currentPlayer.Position)
                {
                    Console.WriteLine($"*You landed on a goldSquare!  You switch places with {otherPlayer.Name}.*");
                    SwapPositions(currentPlayer, otherPlayer);
                }
                else
                {
                    Console.WriteLine("You landed on a goldSquare! but NOT SWITCH(YOU BIGGER)");
                }
            }
        }

        private void RunPlayer(Player player)
        {
            Console.WriteLine($"{player.Name}, press any key to roll the dice.");
            Console.ReadKey();

            int rollValue1 = RollDice();
            int rollValue2 = RollDice();
            Console.WriteLine($"{player.Name} rolled a {rollValue1} and {rollValue2}.");
            int sum = rollValue1 + rollValue2;
            player.Move(sum);
        }

        private static void SwapPositions(Player currentPlayer, Player otherPlayer)
        {
            int temp = currentPlayer.Position;
            currentPlayer.Position = otherPlayer.Position;
            otherPlayer.Position = temp;
        }

        private static int RollDice()
        {
            return _random.Next(1, 7);
        }

        private static void CheckPosition(int position, Player currentPlayer)
        {
            // Check if the player lands on a snake or ladder position
            int index = Array.IndexOf(_snakes, position);
            if (index != -1)
            {
                Console.WriteLine($"Oops! You landed on a snake! Go back from {position} to position {_snakesEnd[index]} .");
                currentPlayer.JumpToDestination(_snakesEnd[index]);
            }
            index = Array.IndexOf(_ladders, position);
            if (index != -1)
            {
                Console.WriteLine($"Congratulations! You landed on a ladder! Climb up from {position} to position {_laddersEnd[index]} .");
                currentPlayer.JumpToDestination(_laddersEnd[index]);
            }
        }
    }
}
