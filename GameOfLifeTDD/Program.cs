using GameOfLifeTDD.GameOfLife;

namespace GameOfLifeTDD
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Please give path and generation count");
                return;
            }
            Game game = new Game();
            await game.ImportRLEFile(args[0]);
            if (Convert.ToInt32(args[1]) < 0)
            {
                Console.Clear();
                _ = Task.Run(() => game.Run(Convert.ToInt32(args[1]), true));
                while (true)
                {
                    for (int i = 0; i < game.Height; i++)
                    {
                        for (int j = 0; j < game.Width; j++)
                        {
                            Console.SetCursorPosition(i, j);
                            Console.Write(game.GetCell(i, j) ? "o" : " ");
                        }
                    }
                    await Task.Delay(1000);
                }

            }
            else
            {
                await game.Run(Convert.ToInt32(args[1]), false);
            }


            Console.WriteLine(game.ExportRLEBoardState());
            return;
        }
    }
}
