using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace dirt
{
    class Side
    {
        private vector[] points;
        private Color c;
        private vector dir;

        public bool visible = true;
        public Side(vector[] ps,Color _c,vector direction)
        {
            points = ps;
            c = _c;
            dir = direction;
        }

        public void renderSide()
        {
            if (!visible)
            {
                return;
            }

            vector[] proj = new vector[4];
            for (int i = 0; i < 4; i++)
            {
                proj[i] = Projector.projectVector(points[i]);
            }

            fillRect(proj);
        }

        public void fillRect(vector[] ps)
        {
            vector s = new vector(0, 0);
            vector e = new vector(0, 0);

            s = new vector(1000,1000);
            e = new vector(-1,-1);

            for (int i = 0; i < 4; i++)
            {
                if (ps[i].x < s.x)
                {
                    s.x = ps[i].x;
                }
                if (ps[i].y < s.y)
                {
                    s.y = ps[i].y;
                }

                if (ps[i].x > e.x)
                {
                    e.x = ps[i].x;
                }
                if (ps[i].y > e.y)
                {
                    e.y = ps[i].y;
                }
            }

            vector A = new vector(ps[0].x, ps[0].y);
            vector B = new vector(ps[1].x, ps[1].y);
            vector C = new vector(ps[2].x, ps[2].y);
            vector D = new vector(ps[3].x, ps[3].y);

            float area = triangleArea(A, B, C) + triangleArea(A, C, D);

            for (int x = (int)s.x; x <= (int)e.x; x++)
            {
                for (int y = (int)s.y; y <= (int)e.y; y++)
                {
                    vector p = new vector(x, y);
                    float ar = triangleArea(A, p, D) + triangleArea(D, p, C) + triangleArea(C, p, B) + triangleArea(p, B, A);

                    if (ar <= area + 0.1f)
                    {
                        Program.setPixel(x, y, new pixel(ps[0].z, c));
                    }
                    
                }
            }
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
