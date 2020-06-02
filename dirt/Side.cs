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
            vector d = dir;

            d = Projector.projectVector(d);
            /*
            if (MathF.Acos((vector.dot(d, vector.front)) / (MathF.Sqrt(vector.dot(d, d)) * MathF.Sqrt(vector.dot(vector.front, vector.front)))) / MathF.PI > 0.6f)
            {
                return;
            }*/

            vector s = new vector(0, 0);
            vector e = new vector(0, 0);

            s = new vector(100000,100000);
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

            float z = (ps[0].z + ps[1].z + ps[2].z + ps[3].z) / 4;

            float area = triangleArea(A, B, C) + triangleArea(A, C, D);

            //float dist = ((d.normalize() - settings.light.normalize())/4).magnitude();

            float dist = 1/* - ((MathF.Acos((vector.dot(d,settings.light))/(MathF.Sqrt(vector.dot(d,d)) * MathF.Sqrt(vector.dot(settings.light,settings.light)))) / MathF.PI) / 3)*/;

            Color pc = Color.FromArgb((int)(c.R * dist), (int)(c.G * dist), (int)(c.B * dist));

            for (int x = (int)s.x; x <= (int)e.x; x++)
            {
                for (int y = (int)s.y; y <= (int)e.y; y++)
                {
                    vector p = new vector(x, y);
                    float ar = triangleArea(A, p, D) + triangleArea(D, p, C) + triangleArea(C, p, B) + triangleArea(p, B, A);

                    if (ar <= area + 0.01f)
                    {
                        Program.setPixel(x, y, new pixel(z, pc));
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

        public vector center()
        {
            float x = 0;
            float y = 0;
            float z = 0;

            foreach (vector v in points)
            {
                x += v.x;
                y += v.y;
                z += v.z;
            }

            x /= points.Length;
            y /= points.Length;
            z /= points.Length;

            return new vector(x, y, z);
        }

        public bool same(vector c)
        {
            return c == center();
        }
    }
}
