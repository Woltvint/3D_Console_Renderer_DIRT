using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Diagnostics;

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

        static async Task Main(string[] args)
        {
            Console.Title = "dirt";

            Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Black;

            
            cubes.Add(new Cube(new vector(-0.5f, -0.5f, -2.5f), Color.Brown));
            cubes.Add(new Cube(new vector(-0.5f, -0.5f, -1.5f), Color.Red));
            cubes.Add(new Cube(new vector(-0.5f, -0.5f, -0.5f), Color.White));
            cubes.Add(new Cube(new vector(-0.5f, -0.5f, 0.5f), Color.Yellow));
            cubes.Add(new Cube(new vector(-0.5f, -0.5f, 1.5f), Color.Blue));

            //cubes = loadFromImage("house.png");
            //cubes = loadFromImage("knight.png");
            //cubes = loadFromImage("teapot.png");
            //cubes = loadFromImage("castle.png");

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
                }
            }

            List<Cube> tmp = new List<Cube>();

            Console.Title = "checking sides";

            //Task[] checkTasks = new Task[cubes.Count - 1];

            for (int i = 0; i < cubes.Count; i++)
            {
                tmp.Add(cubes[i].checkSides(cubes));
                //checkTasks[i] = Task.Run(() => tmp.Add(cubes[i].checkSides(cubes)));
                if (i % 100 == 0)
                {
                    Console.Title = "checking sides (" + i + "/"+ cubes.Count + ")";
                }
            }

            //await Task.WhenAll(checkTasks);

            cubes = tmp;

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

                /*
                Task[] tasks = new Task[cubes.Count-1];

                for (int i = 0; i < cubes.Count-1; i++)
                {
                    tasks[i] = Task.Run(() => cubes[i].renderCube());
                }

                await Task.WhenAll(tasks);*/

                for (int i = 0; i < cubes.Count; i++)
                {
                    cubes[i].renderCube();
                }

                draw();

                oldScreen = screen;
                

                //rx += 0.005f;
                ry -= 0.005f;
                //rz += 0.001f;

                //Console.WriteLine("tick");
            }

        }


        public static void draw()
        {
            Console.CursorVisible = false;

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
                        ctc.setColor(screen[x, y].c);
                        Console.SetCursorPosition(x, y);
                        Console.Write('▒');
                    }
                }
            }
        }

        private static readonly object screenLock = new object();

        public static void setPixel(int x, int y, pixel p)
        {
            if (x >= 0 && x < settings.width)
            {
                if (y >= 0 && y < settings.height)
                {
                    while (true)
                    {
                        lock (screenLock)
                        {
                            if (screen[x, y].z < p.z)
                            {
                                screen[x, y].c = p.c;
                                screen[x, y].z = p.z;
                            }
                            break;
                        }
                    }
                    
                    

                }
            }
        }

        static List<Cube> loadFromImage(string fileName)
        {
            List<Cube> cub = new List<Cube>();

            Bitmap img = (Bitmap)Bitmap.FromFile(fileName);

            float off = -(img.Width / 2f) - 0.5f;

            for (int y = 0; y < img.Height; y += img.Width)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    for (int z = 0; z < img.Width; z++)
                    {
                        if (img.GetPixel(x, z + y).A > 0)
                        {
                            cub.Add(new Cube(new vector(x + off, (y / img.Width) - ((img.Height / img.Width) / 2), z + off), img.GetPixel(x, z + y)));
                        }
                    }
                }
            }

            settings.offZ = -off;

            return cub;
        }
    }
}