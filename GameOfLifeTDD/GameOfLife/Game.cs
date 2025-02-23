using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeTDD.GameOfLife
{
    public class Game
    {
        public const int DEFAULT_WIDTH = 16;
        public const int DEFAULT_HEIGHT = 16;
        private bool[,] matrix;

        public int Height => matrix.GetLength(0);
        public int Width => matrix.GetLength(1);
        public bool GetCell(int row, int col) => matrix[row, col];

        public Game(int height = DEFAULT_HEIGHT, int width = DEFAULT_WIDTH)
        {
            if (height <= 0)
            {
                throw new ArgumentException("Can't have non positive height", nameof(height));
            }
            if (width <= 0)
            {
                throw new ArgumentException("Can't have non positive width", nameof(width));
            }
            this.matrix = new bool[height, width];
        }

        public void SetCells(List<(int, int)> cells)
        {
            foreach (var cell in cells)
            {
                if (cell.Item1 < 0 || cell.Item1 >= Height || cell.Item2 < 0 || cell.Item2 >= Width)
                {
                    throw new ArgumentException("Cell was out of bounds", nameof(cells));
                }

                this.matrix[cell.Item1, cell.Item2] = true;
            }
        }


        /// <summary>
        /// Run the simulation
        /// </summary>
        /// <param name="generationCount">If negative number was given run infinitely</param>
        /// <param name="animate">Should it wait between generations</param>
        /// <returns></returns>
        public async Task Run(int generationCount, bool animate = false)
        {

            while (generationCount != 0)
            {
                if (generationCount > 0)
                {
                    generationCount--;
                }



                if (animate)
                {
                    await Task.Delay(100);
                }
            }
        }

        private void Tick()
        {
            bool[,] newBoard = new bool[Height, Width];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int neighbourCount = NeighbourCount(i, j);
                    if (matrix[i, j])
                    {
                        newBoard[i,j]= neighbourCount ==2 || neighbourCount ==3;
                    }
                    else
                    {
                        newBoard[i, j] = neighbourCount == 3;
                    }
                }
            }

            this.matrix = newBoard;
        }

        private int NeighbourCount(int row, int col)
        {
            int counter = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                if (i >= Height || i < 0)
                {
                    continue;
                }
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (j >= Width || j < 0 || (i == row && j == col))
                    {
                        continue;
                    }
                    if (matrix[i, j])
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public void ClearBoard()
        {
            this.matrix = new bool[Height,Width];
        }

        public void ImportRLEFile(string filePath)
        {
            string fileContent= File.ReadAllText(filePath, Encoding.UTF8);




        }
    }
}
