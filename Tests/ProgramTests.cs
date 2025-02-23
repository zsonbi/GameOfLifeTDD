using GameOfLifeTDD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public async Task TestNotEnoughParameters()
        {
            var stringWriter = new StringWriter();
            await stringWriter.FlushAsync();
            Console.SetOut(stringWriter);
            await GameOfLifeTDD.Program.Main(new string[] { "./TestData/bheptomino-t148.rle" });
            string output = stringWriter.ToString().Trim();
            Assert.Equal("Please give path and generation count", output);
        }

        [Fact]
        public async Task TestNotEnoughParameters2()
        {
            var stringWriter = new StringWriter();
            await stringWriter.FlushAsync();
            Console.SetOut(stringWriter); 
            await GameOfLifeTDD.Program.Main(new string[] { "100" });
            string output = stringWriter.ToString().Trim();
            Assert.Equal("Please give path and generation count",output);
        }
        [Fact]
        public async Task TestInvalidFilePath()
        {
            var stringWriter = new StringWriter();
            await stringWriter.FlushAsync();
            Console.SetOut(stringWriter);
            await GameOfLifeTDD.Program.Main(new string[] { "./TestData/bheptomino-t148.rle_notexisting","100" });
            string output = stringWriter.ToString().Trim();
            Assert.Contains("Can't import file:", output);
        }
    }
}
