using System;
using System.Collections.Generic;
using System.Text;

namespace dirt
{
    class vector
    {
        public float x, y, z, w;

        #region constructors
        public vector(float _x, float _y)
        {
            x = _x;
            y = _y;
            z = 0;
            w = 1;
        }

        public vector(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
            w = 1;
        }

        public vector(float _x, float _y, float _z, float _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }
        #endregion

        public float magnitude()
        {
            float m = 0;

            m += MathF.Pow(x, 2);
            m += MathF.Pow(y, 2);
            m += MathF.Pow(z, 2);
            m += MathF.Pow(w, 2);

            m = MathF.Sqrt(m);

            return m;
        }

        public vector normalize()
        {
            vector r = new vector(0, 0, 0);

            float m = magnitude();

            r.x = x / m;
            r.y = y / m;
            r.z = z / m;
            r.w = w / m;

            return r;
        }
        public static float dist(vector v1, vector v2)
        {
            float dist = 0;

            dist += MathF.Sqrt(MathF.Pow(v1.x - v2.x, 2));
            dist += MathF.Sqrt(MathF.Pow(v1.y - v2.y, 2));
            dist += MathF.Sqrt(MathF.Pow(v1.z - v2.z, 2));
            dist += MathF.Sqrt(MathF.Pow(v1.w - v2.w, 2));

            return dist;
        }

        public static vector cross(vector v1, vector v2)
        {
            vector v = new vector(0, 0, 0);
            v.x = v1.y * v2.z - v1.z * v2.y;
            v.y = v1.z * v2.x - v1.x * v2.z;
            v.z = v1.x * v2.y - v1.y * v2.x;
            return v;
        }

        public float dot(vector v)
        {
            return (v.x * x) + (v.y * y) + (v.z * z) + (v.w * w);
        }



        #region operators
        public static vector operator +(vector v1, vector v2)
        {
            return new vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
        }

        public static vector operator -(vector v1, vector v2)
        {
            return new vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        }

        public static vector operator *(vector v, float m)
        {
            return new vector(v.x * m, v.y * m, v.z * m, v.w * m);
        }

        public static vector operator /(vector v, float m)
        {
            return new vector(v.x / m, v.y / m, v.z / m, v.w / m);
        }

        public static vector operator *(vector i, matrix4x4 m)
        {
            vector o = new vector(0, 0, 0, 0);

            o.x = i.x * m.m[0, 0] + i.y * m.m[1, 0] + i.z * m.m[2, 0] + i.w * m.m[3, 0];
            o.y = i.x * m.m[0, 1] + i.y * m.m[1, 1] + i.z * m.m[2, 1] + i.w * m.m[3, 1];
            o.z = i.x * m.m[0, 2] + i.y * m.m[1, 2] + i.z * m.m[2, 2] + i.w * m.m[3, 2];
            o.w = i.x * m.m[0, 3] + i.y * m.m[1, 3] + i.z * m.m[2, 3] + i.w * m.m[3, 3];

            return o;
        }

        /*public bool Equals(vector v)
        {
            return (x == v.x && y == v.y && z == v.z && w == v.w);
        }*/

        public static bool operator ==(vector v1, vector v2)
        {
            return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w);
        }

        public static bool operator !=(vector v1, vector v2)
        {
            return (v1.x != v2.x || v1.y != v2.y || v1.z != v2.z || v1.w != v2.w);
        }
        #endregion

        public static vector front = new vector(0, 0, 1);
        public static vector back = new vector(0, 0, -1);
        public static vector left = new vector(-1, 0, 0);
        public static vector right = new vector(1, 0, 0);
        public static vector up = new vector(0, 1, 0);
        public static vector down = new vector(0, -1, 0);
    }
}