using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class GameBoard
    {
        private int yBoardSize = 3;
        private int xBoardSize = 3;
        private int gameBoardNumberOfSquares;
        private Dictionary<int, string> gameBoard = new Dictionary<int, string>();
        private int playersTurn = 0;
        internal bool isTurnDone;
        private IList<Player> Players;
        private int gameRound = 1;

        public bool IsGameDone { get; private set; }
        public int LatestNumber { get; private set; }

        public GameBoard(IList<Player> players)
        {
            Players = players;
            gameBoardNumberOfSquares = yBoardSize * xBoardSize;
            for (int k = 1; k < gameBoardNumberOfSquares + 1; k++)
            {
                gameBoard.Add(k, k.ToString());
            }
        }

        public void SetLatestNumber(int latestNumber)
        {
            LatestNumber = latestNumber;
        }

        public void Play()
        {
            isTurnDone = false;
            while (!isTurnDone)
            {
                if (IsSpaceAvaliable())
                {
                    UpdateBoard();
                    bool playerWon = DidPlayerWin();
                    if (playerWon)
                    {
                        Print();
                        Console.WriteLine("{0} WON THE GAME!", CurrentPlayersName());
                        GameIsDone();
                    }

                    TurnDone();
                }
                else
                {
                    Console.WriteLine("Number is already taken, try again!");
                }
            }
        }

        public void TurnDone()
        {
            isTurnDone = true;
            gameRound++;
            if (gameRound > gameBoardNumberOfSquares)
            {
                Console.WriteLine("It is a Draw! Good job?");
                GameIsDone();
            }
            else
            {
                NextPlayersTurn();
            }
        }

        public void GameIsDone()
        {
            isTurnDone = true;
            IsGameDone = true;
        }

        public void NextPlayersTurn()
        {
            if (playersTurn >= Players.Count -1)
            {
                playersTurn = 0;
            }
            else
            {
                playersTurn++;
            }
        }

        public string CurrentPlayersName()
        {
            return Players[playersTurn].Name;
        }

        public string CurrentPlayersSymbol()
        {
            return Players[playersTurn].Symbol;
        }

        public void ResetBoard()
        {
            IsGameDone = false;
            isTurnDone = false;
            gameRound = 1;

            for (int k = 1; k < gameBoardNumberOfSquares +1; k++)
            {
                gameBoard[k] = k.ToString();
            }
        }

        public void Print()
        {
            string dottedLine = "-------------------------";
            string verticleLines = "|       |       |       |";

            Console.Clear();

            for (int i = 1; i < gameBoardNumberOfSquares; i += 3)
            {
                Console.WriteLine(dottedLine);
                Console.WriteLine(verticleLines);
                Console.WriteLine("|   {0}   |   {1}   |   {2}   |", gameBoard[i], gameBoard[i+1], gameBoard[i+2]);
                Console.WriteLine(verticleLines);
            }
            Console.WriteLine(dottedLine);
        }

        public bool IsSpaceAvaliable()
        {
            return int.TryParse(gameBoard[LatestNumber], out _);
        }

        public void UpdateBoard()
        {
            gameBoard[LatestNumber] = CurrentPlayersSymbol();
        }

        public bool DidPlayerWin()
        {
            string symbol = CurrentPlayersSymbol();

            // Check each row if player won
            for (int i = 1; i < gameBoardNumberOfSquares; i+=3)
            {
                if (gameBoard[i] == symbol && gameBoard[i+1] == symbol && gameBoard[i+2] == symbol)
                {
                    return true;
                }
            }

            // Check each column if player won
            for (int i = 1; i < 3; i++)
            {
                if (gameBoard[i] == symbol && gameBoard[i+3] == symbol && gameBoard[i+6] == symbol)
                {
                    return true;
                }
            }

            // Check if player won diagonally
            if (gameBoard[1] == symbol && gameBoard[5] == symbol && gameBoard[9] == symbol)
            {
                return true;
            }
            if (gameBoard[3] == symbol && gameBoard[5] == symbol && gameBoard[7] == symbol)
            {
                return true;
            }

            return false;
        }
    }
}
