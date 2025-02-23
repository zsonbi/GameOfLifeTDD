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

        [Fact]
        public async Task ReadInGliderTest()
        {
            Game game = new Game();

            await game.ImportRLEFile("./TestData/glider.rle");
            Assert.Equal(3, game.Height);
            Assert.Equal(3, game.Width);
            Assert.False(game.GetCell(0, 0));
            Assert.True(game.GetCell(0, 1));
            Assert.False(game.GetCell(0, 2));
            Assert.False(game.GetCell(1, 0));
            Assert.False(game.GetCell(1, 1));
            Assert.True(game.GetCell(1, 2));
            Assert.True(game.GetCell(2, 0));
            Assert.True(game.GetCell(2, 1));
            Assert.True(game.GetCell(2, 2));
        }


        [Fact]
        public async Task ReadInGliderGunTest()
        {
            Game game = new Game();

            await game.ImportRLEFile("./TestData/gosperglidergun.rle");
            Assert.Equal(9, game.Height);
            Assert.Equal(36, game.Width);
            Assert.True(game.GetCell(0, 24));
        }


        [Fact]
        public async Task ReadInGliderAlteredRuleTest()
        {
            Game game = new Game();

            await game.ImportRLEFile("./TestData/glider_altered_rule.rle");
            Assert.Equal(3, game.Height);
            Assert.Equal(3, game.Width);
            Assert.False(game.GetCell(0, 0));
            Assert.True(game.GetCell(0, 1));
            Assert.False(game.GetCell(0, 2));
            Assert.False(game.GetCell(1, 0));
            Assert.False(game.GetCell(1, 1));
            Assert.True(game.GetCell(1, 2));
            Assert.True(game.GetCell(2, 0));
            Assert.True(game.GetCell(2, 1));
            Assert.True(game.GetCell(2, 2));
            Assert.Contains(game.BirthAmount,x=>x==1);
            Assert.Contains(game.BirthAmount,x=>x==3);
            Assert.Contains(game.BirthAmount,x=>x==4);
            Assert.Equal(3,game.BirthAmount.Count);
            Assert.Equal(5,game.SurvivalAmount.Count);
            Assert.Contains(game.SurvivalAmount, x => x == 2);
            Assert.Contains(game.SurvivalAmount, x => x == 3);
            Assert.Contains(game.SurvivalAmount, x => x == 5);
            Assert.Contains(game.SurvivalAmount, x => x == 7);
            Assert.Contains(game.SurvivalAmount, x => x == 8);
        }
    }
}