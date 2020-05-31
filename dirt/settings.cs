using System;
using System.Collections.Generic;
using System.Text;

namespace dirt
{
    class settings
    {
        public static int width = 634;
        public static int height = 211;
        public static float ratio = 1.6f;
        public static float fov = (float)(Math.PI / 2);
        public static float zFar = 1000;
        public static float zNear = 0.01f;
        public static float scale = 10;

        public static float fYaw = 0;

        public static float offX = width / 2;
        public static float offY = height / 2;
        public static float offZ = 0;

        public static vector offset = new vector(width / 2, height / 2, 0);

        public static vector light = new vector(-1, -1, 1);
    }
}
