using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var player1 = new Player("Player1", "O");
            var player2 = new Player("Player2", "X");
            var gameBoard = new GameBoard(new List<Player> { player1, player2 });

            var gameActive = true;

            while (gameActive)
            {
                gameBoard.ResetBoard();

                while (!gameBoard.IsGameDone)
                {
                    gameBoard.Print();
                    PlayersInput(gameBoard);
                    gameBoard.Play();
                }
                gameActive = PlayAgain();
            }
        }

        private static void PlayersInput(GameBoard gameBoard)
        {
            var ValidInput = false;

            while (!ValidInput)
            {
                Console.WriteLine($"{gameBoard.CurrentPlayersName()}, Enter a number between 1 to 9: ");

                if (userInputCheck(Console.ReadLine(), gameBoard))
                {
                    if (!gameBoard.IsSpaceAvaliable())
                    {
                        gameBoard.Print();
                        Console.WriteLine($"{gameBoard.LatestNumber} Space is already taken! Try again!");
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static bool userInputCheck(string userInput, GameBoard gameBoard)
        {
            var userInputParse = 0;

            try
            {
                userInputParse = int.Parse(userInput);
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong... try again!");
            }

            if (userInputParse >= 1 && userInputParse < 10)
            {
                gameBoard.SetLatestNumber(userInputParse);
                return true;
            }
            return false;
        }

        private static bool PlayAgain()
        {
            bool gameDone;
            while (true)
            {
                Console.WriteLine("Play again? y/n");
                var playerInput = Console.ReadLine();

                if (playerInput == "y" || playerInput == "n")
                {
                    gameDone = playerInput == "y";
                    break;
                }
                Console.WriteLine("Please answer 'y' or 'n'");
            }
            return gameDone;
        }
    }
}