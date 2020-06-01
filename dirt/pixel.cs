using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace dirt
{
    class pixel
    {
        public Color c;
        public float z;

        public pixel(float _z,Color _c)
        {
            z = _z;
            c = _c;
        }
    }
}
