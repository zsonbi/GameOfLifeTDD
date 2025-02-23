using GameOfLifeTDD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class ProgramTests
    {

        public async Task TestNotEnoughParameters()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            await GameOfLifeTDD.Program.Main(new string[] { "./TestData/bheptomino-t148.rle" });
            Console.SetOut(stringWriter);
            await GameOfLifeTDD.Program.Main(new string[] { "100" });
            string output = stringWriter.ToString().Trim();
            Assert.Equal("Please give path and generation count", output);
        }

        public async Task TestNotEnoughParameters2()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter); 
            await GameOfLifeTDD.Program.Main(new string[] { "100" });
            string output = stringWriter.ToString().Trim();
            Assert.Equal("Please give path and generation count",output);
        }

        public async Task TestInvalidFilePath()
        {
          await Assert.ThrowsAsync<ArgumentException>( async ()=> await GameOfLifeTDD.Program.Main(new string[] { "./TestData/bheptomino-t148.rle1","100" }));
        }
    }
}
