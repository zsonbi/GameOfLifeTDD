using GameOfLifeTDD.GameOfLife;

namespace Tests
{
    public class GameFileExportTests
    {
        [Fact]
        public async Task ExportBlinkerTest()
        {
            Game game = new Game();

           await game.ImportRLEFile("./TestData/blinker.rle");
            Assert.Equal(1,game.Height);
            Assert.Equal(3,game.Width);
            Assert.True(game.GetCell(0,0));
            Assert.True(game.GetCell(0,1));
            Assert.True(game.GetCell(0,2));

            string exported= game.ExportRLEBoardState();

            Assert.Equal("x = 3, y = 1, rule = B3/S23\r\n3o!".Replace("\r",""), exported.Replace("\r", ""));
        }

        [Fact]
        public async Task ExportBlockTest()
        {
            Game game = new Game();

            await game.ImportRLEFile("./TestData/block.rle");
            Assert.Equal(2, game.Height);
            Assert.Equal(2, game.Width);

            string exported = game.ExportRLEBoardState();

            Assert.Equal("x = 2, y = 2, rule = B3/S23\r\n2o$2o!".Replace("\r", ""), exported.Replace("\r", ""));
        }


        [Fact]
        public async Task ExportGliderTest()
        {
            Game game = new Game();

            await game.ImportRLEFile("./TestData/glider.rle");
            Assert.Equal(3, game.Height);
            Assert.Equal(3, game.Width);

            string exported = game.ExportRLEBoardState();

            Assert.Equal("x = 3, y = 3, rule = B3/S23\r\nbob$2bo$3o!".Replace("\r", ""), exported.Replace("\r", ""));
        }

        [Fact]
        public async Task ExportGliderGunTest()
        {
            Game game = new Game();

            await game.ImportRLEFile("./TestData/gosperglidergun.rle");
            Assert.Equal(9, game.Height);
            Assert.Equal(36, game.Width);

            string exported = game.ExportRLEBoardState();

            Assert.Equal("x = 36, y = 9, rule = B3/S23\r\n24bo11b$22bobo11b$12b2o6b2o12b2o$11bo3bo4b2o12b2o$2o8bo5bo3b2o14b$2o8b\r\no3bob2o4bobo11b$10bo5bo7bo11b$11bo3bo20b$12b2o!".Replace("\r", ""), exported.Replace("\r", ""));
        }

    }
}