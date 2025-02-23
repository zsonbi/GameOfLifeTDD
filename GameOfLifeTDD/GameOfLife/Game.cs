﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeTDD.GameOfLife
{
    public class Game
    {
        private bool[,] matrix;
        public List<int> SurvivalAmount = new List<int>() { 2, 3 };
        public List<int> BirthAmount = new List<int>() { 3 };

        public const int DEFAULT_WIDTH = 16;
        public const int DEFAULT_HEIGHT = 16;

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
                        newBoard[i, j] = SurvivalAmount.Contains(neighbourCount);
                    }
                    else
                    {
                        newBoard[i, j] = BirthAmount.Contains(neighbourCount);
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
            this.matrix = new bool[Height, Width];
        }

        public async Task ImportRLEFile(string filePath)
        {
            try
            {
                string[] fileContent = await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
                bool[,]? readInBoard = null;
                string board = "";
                List<int> survivalAmounts = new List<int>();
                List<int> birthAmounts = new List<int>();
                foreach (string line in fileContent)
                {
                    if (line.StartsWith('#'))
                    {
                        continue;
                    }

                    if (line.StartsWith('x'))
                    {
                        string[] splitted = line.Split("=");
                        int width = int.Parse(splitted[1].Split(',')[0]);
                        int height = int.Parse(splitted[2].Split(',')[0]);

                        if (splitted.Length >= 3)
                        {
                            string[] rules = splitted[3].Split("/");
                            for (int i = 1; i < rules[0].Length; i++)
                            {
                                birthAmounts.Add(Convert.ToInt32(rules[0][i]));
                            }
                            for (int i = 1; i < rules[1].Length; i++)
                            {
                                survivalAmounts.Add(Convert.ToInt32(rules[1][i]));
                            }
                        }
                        readInBoard = new bool[height, width];
                    }
                    else
                    {
                        board += line;
                    }
                }
                if (readInBoard is null)
                {
                    throw new Exception("Can't create no boardsize was specified in file");
                }
                string[] rows = board.Trim('!').Split("$");
                string num = "";
                int colIndex = 0;
                int rowIndex = 0;
                foreach (string row in rows)
                {
                    colIndex = 0;

                    foreach (char character in row)
                    {
                        if (character <= '9' && character >= '0')
                        {
                            num += character;
                        }
                        else
                        {
                            int occurence = num == "" ? 1 : int.Parse(num);
                            for (int i = 0; i < occurence; i++)
                            {
                                readInBoard[rowIndex, colIndex] = 'o' == character;
                                colIndex++;
                            }
                            num = "";
                        }
                    }
                    rowIndex++;
                }
                
                this.SurvivalAmount = survivalAmounts.Count == 0 ? this.SurvivalAmount : survivalAmounts;
                this.BirthAmount = birthAmounts.Count == 0 ? this.BirthAmount : birthAmounts;
                this.matrix = readInBoard;
            }
            catch (Exception)
            {
                Console.WriteLine("Can't import file: " + filePath);
            }
        }
    }
}
