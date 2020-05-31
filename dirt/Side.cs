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


        }
    }
}
