using System;
using System.Threading;

namespace Snake_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fix gameOver bool so that it can run in main as a while loop. Maybe change the methods to take in and output the bool variable?
            GameMaster master = new GameMaster();
            bool gameOver = master.Setup();
            while (!gameOver)
            {
                master.Draw();
                Thread.Sleep(300);
                master.Input();
                gameOver = master.Logic();
            }
            
        }
    }
}
