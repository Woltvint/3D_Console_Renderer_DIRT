using System;
using System.Collections.Generic;
using System.Text;

namespace dirt
{
    class matrix4x4
    {
        public float[,] m;

        public matrix4x4()
        {
            m = new float[4, 4];
        }

        public static matrix4x4 operator *(matrix4x4 m1, matrix4x4 m2)
        {
            matrix4x4 matrix = new matrix4x4();
            for (int c = 0; c < 4; c++)
            {
                for (int r = 0; r < 4; r++)
                {
                    matrix.m[r, c] = m1.m[r, 0] * m2.m[0, c] + m1.m[r, 1] * m2.m[1, c] + m1.m[r, 2] * m2.m[2, c] + m1.m[r, 3] * m2.m[3, c];
                }
            }
            return matrix;
        }
    }
}
