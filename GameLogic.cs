using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SnakeGame
{
    internal class GameLogic
    {
        public int Rows { get; }
        public int Columns { get; }
        public GridValue[,] Grid { get; }
        public Direction Direction { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; } 

        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();

        private readonly Random foodPosition = new Random();

        public GameLogic(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            Grid = new GridValue[Rows, Columns];
            Direction = Direction.Right;

            AddSnake();
            AddFood();

        }

        /// <summary>
        /// I want to add snake at the middle of the Grid 
        /// </summary>
        private void AddSnake()
        {
            int middle = Rows / 2;

            for (int col = 1; col < 3; col++)
            {
                Grid[middle,col] = GridValue.Snake ;
                snakePositions.AddFirst(new Position(middle, col));
            }
        }

        private IEnumerable<Position> emptyPositions()
        {
            for (int row = 0; row < Rows ; row ++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (Grid[row, col] == GridValue.Empty) 
                    {
                        yield return new Position(row, col);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> emptysGridValue = new List<Position>(emptyPositions());

            if (emptysGridValue.Count == 0)
                return;

            Position pos = emptysGridValue[foodPosition.Next(emptysGridValue.Count)];
            Grid[pos.Row, pos.Column] = GridValue.Food;

        }

    }
}
