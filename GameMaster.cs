using System;
using System.Threading.Tasks;

namespace Snake_Game
{
    class GameMaster
    {
        bool gameOver;

        Random fruitRandomizer = new Random();

        // The width and height of the game board. These settings are constant and will not change unless changed here.
        const int width = 20;
        const int height = 20;

        // These global variables work in all methods, it's not recomended to use global variables in big projects. But as this is a small game, it works here.
        int x, y, fruitX, fruitY, score;

        // Used to make sure that the tail works as intended.
        int[] tailX = new int[100];
        int[] tailY = new int[100];

        // nTail is the total length of the tail. It gets +1 bigger every time the snake eats a fruit.
        int nTail;

        // Using enum to make sure the directions are always set to up, down, left or right. Stop is only to (re)start the game.
        enum Directions { Stop = 0, Left, Right, Up, Down };
        Directions dir;

        // Used to get the key that the player is pressing.
        ConsoleKeyInfo uInput;
        public bool Setup()
        {
            gameOver = false;

            // The start position of the player.
            x = width / 2;
            y = height / 2;

            // Randomizes the fruit location.
            fruitX = fruitRandomizer.Next() % width;
            fruitY = fruitRandomizer.Next() % height;
            score = 0;

            // Returns gameOver to main so that it can be used there to keep the game going.
            return gameOver;
        }
        public void Draw()
        {
            Console.Clear();
            for (int i = 0; i < height; i++)
            {
                Console.Write("#");
                for (int j = 0; j < width; j++)
                {
                    if(i == 0 || i == height-1)
                    {
                        Console.Write("#");
                    }
                    else if(i == y && j == x)
                    {
                        Console.Write("O");
                    }
                    else if (i == fruitY && j == fruitX)
                    {
                        Console.Write("F");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("#");
                Console.WriteLine("");
            }
        }

        public void Input()
        {
            // This if statement is used to check for player key input. The reason for why it's needed is because without it the Console.ReadKey()
            // method would pause the game constantly, which would not be good for gameplay.
            if (Console.KeyAvailable) {
                uInput = Console.ReadKey();
            }

            // This switch case is used to decide what direction the player has chosen, if they click A they move left, if they click D they move right and so on.
            switch (uInput.Key)
            {
                case ConsoleKey.A:
                    dir = Directions.Left;
                    break;
                case ConsoleKey.D:
                    dir = Directions.Right;
                    break;
                case ConsoleKey.W:
                    dir = Directions.Up;
                    break;
                case ConsoleKey.S:
                    dir = Directions.Down;
                    break;
                default:
                    dir = Directions.Stop;
                    break;
            }
        }

        public bool Logic()
        {
            // The logic parts of the tail.
            int prevX = tailX[0];
            int prevY = tailY[0];
            int prev2X, prev2Y;
            tailX[0] = x;
            tailY[0] = y;

            for (int i = 1; i < nTail; i++)
            {
                prev2X = tailX[i];
                prev2Y = tailY[i];
                tailX[i] = prevX;
                tailY[i] = prevY;
                prevX = prev2X;
                prevY = prev2Y;
            }

            // Tells the computer what to do when the player presses a movement key.
            switch (dir)
            {
                case Directions.Left:
                    x--;
                    break;
                case Directions.Right:
                    x++;
                    break;
                case Directions.Up:
                    y--;
                    break;
                case Directions.Down:
                    y++;
                    break;
                default:
                    break;

            }

            // If the snake hits a wall, the game should end.
            if (x > width - 1 || x < 0 || y > height - 2 || y < 0)
            {
                gameOver = true;
            }

            // If the snake hits the tail, the game should end.
            for (int i = 0; i < nTail; i++)
            {
                if (tailX[i] == x && tailY[i] == y)
                {
                   gameOver = true;
                }
            }

            // If the snake moves over a fruit, the position of the fruit changes and the snake gets 1 extra tail length.
            if (x == fruitX && y == fruitY)
            {
                score += 10;
                fruitX = fruitRandomizer.Next() % width;
                fruitY = fruitRandomizer.Next() % height;
                nTail++;
            }

            return gameOver;
        }
    }
}
