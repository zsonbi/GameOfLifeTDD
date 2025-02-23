using GameOfLifeTDD.GameOfLife;

namespace Tests
{
    public class GameFileReadInTests
    {
        [Fact]
        public async Task ReadInBlinkerTest()
        {
            Game game = new Game();

           await game.ImportRLEFile("./TestData/blinker.rle");
            Assert.Equal(1,game.Height);
            Assert.Equal(3,game.Width);
            Assert.True(game.GetCell(0,0));
            Assert.True(game.GetCell(0,1));
            Assert.True(game.GetCell(0,2));
        }

        [Fact]
        public async Task ReadInBlockTest()
        {
            Game game = new Game();

            await game.ImportRLEFile("./TestData/block.rle");
            Assert.Equal(2, game.Height);
            Assert.Equal(2, game.Width);
            Assert.True(game.GetCell(0, 0));
            Assert.True(game.GetCell(0, 1));
            Assert.True(game.GetCell(1, 0));
            Assert.True(game.GetCell(1, 1));
        }
    }
}