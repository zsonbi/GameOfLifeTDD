using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeTDD.GameOfLife;

namespace Tests
{
    public class GameBaseFunctionalityTests
    {

        [Theory]
        [InlineData(10, 10)]
        [InlineData(10, 5)]
        [InlineData(5, 10)]
        public void CreateTest(int height, int width)
        {
            Game game = new Game(height, width);
            Assert.Equal(height, game.Height);
            Assert.Equal(width, game.Width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.False(game.GetCell(i, j));
                }
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-4)]
        public void InvalidHeightTest(int height)
        {
            Assert.Throws<ArgumentException>(() => new Game(height));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-4)]
        public void InvalidWidthTest(int width)
        {
            Assert.Throws<ArgumentException>(() => new Game(width: width));
        }

        [Fact]
        public void DefaultGameSize()
        {
            Game game = new Game();
            Assert.Equal(Game.DEFAULT_HEIGHT, game.Height);
            Assert.Equal(Game.DEFAULT_WIDTH, game.Width);
        }

        [Fact]
        public void SetCellsTest1()
        {
            Game game = new Game();
            List<(int, int)> cells = new List<(int, int)>() { (5, 5), (1, 4), (1, 6), (1, 7), (1, 8), (1, 9), (1, 10) };
            game.SetCells(cells);

            foreach (var cell in cells)
            {
                Assert.True(game.GetCell((int)cell.Item1, (int)cell.Item2));
            }
        }

        [Fact]
        public void SetCellsTestOutOfRangeTest1()
        {
            Game game = new Game();
            List<(int, int)> cells = new List<(int, int)>() { (5, 5), (1, 4), (1, 6), (1, 7), (1, 8), (1, 9), (1, 16) };
            Assert.Throws<ArgumentException>(() => game.SetCells(cells));
        }

        [Fact]
        public void SetCellsTestOutOfRangeTest2()
        {
            Game game = new Game();
            List<(int, int)> cells = new List<(int, int)>() { (5, 5), (1, 4), (1, 6), (1, 7), (1, 8), (1, 9), (16, 1) };
            Assert.Throws<ArgumentException>(() => game.SetCells(cells));
        }

        [Fact]
        public void GetNeighbourCountTest1()
        {
            Game game = new Game();

            List<(int, int)> cells = new List<(int, int)>() { (3, 3), (2, 3), (4, 3), (3, 4), (3, 2) };
            game.SetCells(cells);

            MethodInfo methodInfo = typeof(Game).GetMethod("NeighbourCount", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { 3, 3 };
            int? count = (int?)methodInfo?.Invoke(game, parameters);
            Assert.Equal(4, count);
        }


        [Fact]
        public void GetNeighbourCountTest2()
        {
            Game game = new Game();

            List<(int, int)> cells = new List<(int, int)>() { (3, 0), (3, 1), (2, 0), (4, 0), (4, 2) };
            game.SetCells(cells);

            MethodInfo methodInfo = typeof(Game).GetMethod("NeighbourCount", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { 3, 0 };
            int? count = (int?)methodInfo?.Invoke(game, parameters);
            Assert.Equal(3, count);
        }

        [Fact]
        public void GetNeighbourCountTest3()
        {
            Game game = new Game();

            List<(int, int)> cells = new List<(int, int)>() { (3, 0), (3, 1), (2, 0), (4, 0), (4, 2) };
            game.SetCells(cells);

            MethodInfo methodInfo = typeof(Game).GetMethod("NeighbourCount", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { -1, -1 };
            int? count = (int?)methodInfo?.Invoke(game, parameters);
            Assert.Equal(0, count);
        }

        [Fact]
        public void GameTickTest1()
        {
            Game game = new Game();
            List<(int, int)> cells = new List<(int, int)>() { (3, 3), (3, 2), (3, 4) };
            game.SetCells(cells);

            MethodInfo methodInfo = typeof(Game).GetMethod("Tick", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { };
            methodInfo?.Invoke(game, parameters);

            Assert.True(game.GetCell(3, 3));
            Assert.True(game.GetCell(4, 3));
            Assert.True(game.GetCell(2, 3));
            methodInfo?.Invoke(game, parameters);
            Assert.True(game.GetCell(3, 3));
            Assert.True(game.GetCell(3, 4));
            Assert.True(game.GetCell(3, 2));
        }

        [Fact]
        public void GameTickTest2()
        {
            Game game = new Game();
            List<(int, int)> cells = new List<(int, int)>() { (3, 3) };
            game.SetCells(cells);

            MethodInfo methodInfo = typeof(Game).GetMethod("Tick", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { };
            methodInfo?.Invoke(game, parameters);

            Assert.False(game.GetCell(3, 3));
            methodInfo?.Invoke(game, parameters);
            Assert.False(game.GetCell(3, 3));
        }

        [Fact]
        public async Task GameRunTest1()
        {
            Game game = new Game();
            List<(int, int)> cells = new List<(int, int)>() { (3, 3), (2, 2) };
            game.SetCells(cells);

            await game.Run(10);
            Assert.True(game.GetCell(2, 2));
            Assert.True(game.GetCell(3, 3));
            Assert.False(game.GetCell(2, 3));
            Assert.False(game.GetCell(3, 2));


        }

    }
}
