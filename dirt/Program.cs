using System;
using System.Collections.Generic;
using System.Drawing;

namespace dirt
{

    /*
        D.I.R.T. - Dynamic Imprecise Rendering Terminal
        by Patrik Němeček
    */
    class Program
    {
        public static float rx = 0f;
        public static float ry = 0f;
        public static float rz = 0f;

        public static pixel[,] screen;
        public static pixel[,] oldScreen;

        public static List<Cube> cubes = new List<Cube>();

        static ConsoleToColor ctc = new ConsoleToColor();

        static void Main(string[] args)
        {
            Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Black;

            cubes.Add(new Cube(new vector(-0.5f,-0.5f,-0.5f), Color.White));

            settings.width = Console.WindowWidth;
            settings.height = Console.WindowHeight;
            settings.offX = settings.width / 2;
            settings.offY = settings.height / 2;

            oldScreen = new pixel[settings.width, settings.height];
            for (int x = 0; x < oldScreen.GetLength(0); x++)
            {
                for (int y = 0; y < oldScreen.GetLength(1); y++)
                {
                    oldScreen[x, y] = new pixel(-1000, Color.Black);
                    oldScreen[x, y].z = -1000;
                }
            }

            Console.WriteLine("starting...");

            

            //render loop
            while (true)
            {
                screen = new pixel[settings.width, settings.height];
                for (int x = 0; x < screen.GetLength(0); x++)
                {
                    for (int y = 0; y < screen.GetLength(1); y++)
                    {
                        screen[x, y] = new pixel(-1000,Color.Black);
                        screen[x, y].z = -1000;
                    }
                }

                foreach (Cube c in cubes)
                {
                    c.renderCube();
                }

                draw();

                oldScreen = screen;
                

                rx += 0.005f;
                ry -= 0.002f;
                rz += 0.001f;

                //Console.WriteLine("tick");
            }

        }


        public static void draw()
        {
            for (int x = 0; x < screen.GetLength(0); x++)
            {
                for (int y = 0; y < screen.GetLength(1); y++)
                {
                    if (screen[x,y] == null)
                    {
                        continue;
                    }

                    if (screen[x,y].c != oldScreen[x,y].c)
                    {
                        Console.SetCursorPosition(x, y);
                        ctc.setColor(screen[x,y].c);
                        Console.Write('░');
                    }
                }
            }
        }

        

        public static void setPixel(int x, int y, pixel p)
        {
            if (x >= 0 && x < settings.width)
            {
                if (y >= 0 && y < settings.height)
                {
                    /*if (screen[x,y].z < p.z)
                    {*/
                        screen[x, y].c = p.c;
                        screen[x, y].z = p.z;
                    //}
                }
            }
        }
    }
}
