using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace dirt
{
    class ConsoleToColor
    {
        static Color[] colors = new Color[256];
        static int[,] cc = new int[256, 2];
        public ConsoleToColor()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    colors[j + (i * 16)] = Color.FromArgb((baseConsoleColors[i].R + baseConsoleColors[j].R) / 2, (baseConsoleColors[i].G + baseConsoleColors[j].G) / 2, (baseConsoleColors[i].B + baseConsoleColors[j].B) / 2);
                    cc[j + (i * 16), 0] = i;
                    cc[j + (i * 16), 1] = j;
                }
            }
        }

        private Color[] baseConsoleColors = {
            Color.FromArgb(0x000000), Color.FromArgb(0x00008B),
            Color.FromArgb(0x006400), Color.FromArgb(0x008B8B),
            Color.FromArgb(0x8B0000), Color.FromArgb(0x8B008B),
            Color.FromArgb(0x000000), Color.FromArgb(0x808080),
            Color.FromArgb(0xA9A9A9), Color.FromArgb(0x0000FF),
            Color.FromArgb(0x008000), Color.FromArgb(0x00FFFF),
            Color.FromArgb(0xFF0000), Color.FromArgb(0xFF00FF),
            Color.FromArgb(0xFFFF00), Color.FromArgb(0xFFFFFF)
        };

        public void setColor(Color c)
        {
            int close = 0;
            int dist = 1000;

            for (int i = 0; i < 256; i++)
            {
                int d = colorDist(c, colors[i]);

                if (d < dist)
                {
                    dist = d;
                    close = i;
                }
            }


            ConsoleColor bg = Enum.Parse<ConsoleColor>(cc[close, 0].ToString());
            ConsoleColor fg = Enum.Parse<ConsoleColor>(cc[close, 1].ToString());

            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
        }

        private int colorDist(Color c1, Color c2)
        {
            int r = (int)MathF.Abs(c1.R - c2.R);
            int g = (int)MathF.Abs(c1.G - c2.G);
            int b = (int)MathF.Abs(c1.B - c2.B);

            return r + g + b;
        }
    }
}

