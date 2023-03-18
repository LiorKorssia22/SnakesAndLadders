using System;

namespace SnakesAndLadders
{
    public class FixedBoard
    {
        private const int _boardSize = 100;
        private static readonly int[] _ladderStarts = { 1, 4, 8, 21, 28, 50, 71, 80 };
        private static readonly int[] _ladderEnds = { 38, 14, 30, 42, 76, 67, 92, 99 };
        private static readonly int[] _snakeHeads = { 32, 36, 48, 62, 88, 95, 97 };
        private static readonly int[] _snakeTails = { 10, 6, 26, 18, 24, 56, 78 };
        private Player[] _players;
        private int _currentPlayerIndex;
        private static Random _random = new Random();

        public FixedBoard(string player1Name, string player2Name)
        {
            _players = new Player[2];
            _players[0] = new Player(player1Name);
            _players[1] = new Player(player2Name);
            _currentPlayerIndex = 0;
        }
        private void PrintLadders()
        {
            int counter = 0;
            while (counter < _ladderStarts.Length)
            {
                Console.WriteLine($"ladders start from {_ladderStarts[counter]} to {_ladderEnds[counter]}");
                counter++;
            }
        }
        private void PrintSnakes()
        {
            int counter = 0;
            while (counter < _snakeHeads.Length)
            {
                Console.WriteLine($"snakes start from {_snakeHeads[counter]} to {_snakeTails[counter]}");
                counter++;
            }
        }

        public void RunFixedBoard()
        {
            PrintLadders();
            PrintSnakes();
            while (true)
            {
                Console.WriteLine($"{_players[_currentPlayerIndex].Name}'s turn (position: {_players[_currentPlayerIndex].Position}):");
                Console.Write("press any key to roll the dice: ");
                Console.ReadKey();

                int rollValue1 = RollDice();
                int rollValue2 = RollDice();
                Console.WriteLine($"{_players[_currentPlayerIndex].Name} rolled a {rollValue1} and {rollValue2}.");
                int steps = rollValue1 + rollValue2;

                _players[_currentPlayerIndex].Move(steps);
                if (_players[_currentPlayerIndex].Position > _boardSize)
                {
                    Console.WriteLine($"{_players[_currentPlayerIndex].Name} has won!");
                    break;
                }

                CheckLadderOrSnake();
                Console.WriteLine();

                _currentPlayerIndex = (_currentPlayerIndex + 1) % 2;
            }
        }
        private static int RollDice()
        {
            return _random.Next(1, 7);
        }

        private void CheckLadderOrSnake()
        {
            int index = Array.IndexOf(_ladderStarts, _players[_currentPlayerIndex].Position);
            if (index != -1)
            {
                Console.WriteLine($"Ladder found! {_players[_currentPlayerIndex].Name} climbs from {_players[_currentPlayerIndex].Position} to {_ladderEnds[index]}");
                _players[_currentPlayerIndex].JumpToDestination(_ladderEnds[index]);
            }

            index = Array.IndexOf(_snakeHeads, _players[_currentPlayerIndex].Position);
            if (index != -1)
            {
                Console.WriteLine($"Snake found! {_players[_currentPlayerIndex].Name} slides from {_players[_currentPlayerIndex].Position} to {_snakeTails[index]}");
                _players[_currentPlayerIndex].JumpToDestination(_snakeTails[index]);
            }
        }
    }
}
