using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace SnakeGame
{
    internal class InitalGameStatus
    {
        public int Rows { get; }
        public int Columns { get; }
        public GridValue[,] Grid { get; }
        public Direction Direction { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();

        private readonly Random foodPosition = new Random();

        public InitalGameStatus(int rows, int cols)
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

            for (int col = 1; col <= 3; col++)
            {
                Grid[middle, col] = GridValue.Snake;
                snakePositions.AddFirst(new Position(middle, col));
            }
        }

        private IEnumerable<Position> emptyPositions()
        {
            for (int row = 0; row < Rows; row++)
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

        public Position headPosition()
        {
            return snakePositions.First.Value;
        }

        public Position TailPosition()
        {
            return snakePositions.Last.Value;
        }

        public IEnumerable<Position> SnakePositions()
        {
            return snakePositions;
        }

        /// <summary>
        /// This method is for move the snake 
        /// </summary>
        /// <param name="pos"></param>
        private void AddHead(Position pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Column] = GridValue.Snake;
        }

        /// <summary>
        /// This method is for move the snake 
        /// </summary>
        /// <param name="pos"></param>
        private void RemoveTail()
        {
            Position tail = TailPosition();
            Grid[tail.Row, tail.Column] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        /// <summary>
        /// Need to control some uses cases
        /// we have to evaluate several use cases 
        /// 1-the movement can be performed, in this case we remove the tail and move the snake's tail one more position.
        /// 2-the next movement is food, in this case we only move the head and do not eliminate the tail.
        /// 3-the next movement causes a collision against the wall or the snake itself, in this case the game is over.
        /// </summary>
        /// <param name="newDirection"></param>
        public void ChangeDirection(Direction newDirection)
        {
            Direction = newDirection;
        }

        /// <summary>
        /// check if position is outSideGrid
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool OutsideGrid (Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns;
        }

        /// <summary>
        /// Check what is store in the grid for the next position
        /// </summary>
        /// <returns></returns>
        private GridValue WillHit (Position newHeadPosition)
        {
            if (OutsideGrid(newHeadPosition))
                return GridValue.Outside;

            if (newHeadPosition == TailPosition())
                return GridValue.Empty;

            return Grid[newHeadPosition.Row, newHeadPosition.Column];
        }

        /// <summary>
        /// Need to control some uses cases
        /// we have to evaluate several use cases 
        /// 1-the movement can be performed, in this case we remove the tail and move the snake's tail one more position.
        /// 2-the next movement is food, in this case we only move the head and do not eliminate the tail.
        /// 3-the next movement causes a collision against the wall or the snake itself, in this case the game is over.
        /// </summary>
        public void Move()
        {
            Position newHeadPosition = headPosition().Translate(Direction);
            GridValue hit = WillHit(newHeadPosition);
            if (hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPosition);
            }else if (hit == GridValue.Food)
            {
                AddHead(newHeadPosition);
                Score++;
                AddFood();
            }else if(hit == GridValue.Outside || hit == GridValue.Snake)
                GameOver = true;
        }

    }
}
