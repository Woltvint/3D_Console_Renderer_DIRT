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

        public static int[,,] screen;
        public static int[,,] oldScreen;

        static ConsoleToColor ctc = new ConsoleToColor();

        static void Main(string[] args)
        {
            Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Black;

            vector s = new vector(10, 10);
            vector e = new vector(20, 20);

            vector a = new vector(10, 15);

            vector b = new vector(10, 10);

            vector c = new vector(20, 15);

            vector d = new vector(20, 20);

            float area = triangleArea(a, b, c) + triangleArea(a, c, d);

            Console.ForegroundColor = ConsoleColor.White;

            for (int x = (int)s.x; x <= (int)e.x; x++)
            {
                for (int y = (int)s.y; y <= (int)e.y; y++)
                {
                    vector p = new vector(x, y);
                    float ar = triangleArea(a,p,d) + triangleArea(d,p,c) + triangleArea(c,p,b) + triangleArea(p,b,a);

                    Console.SetCursorPosition(x, y);

                    if (ar <= area + 0.1f)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write('#');
                    }
                    
                }
            }

            /*
            //render loop
            while (true)
            {



            }*/

        }

        public static float triangleArea(vector A, vector B, vector C)
        {
            float a = vector.dist(C, B);
            float b = vector.dist(A, C);
            float c = vector.dist(A, B);

            float s = (a + b + c) / 2f;

            return MathF.Sqrt(s * (s - a) * (s - b) * (s - c));
        }


    }
}
