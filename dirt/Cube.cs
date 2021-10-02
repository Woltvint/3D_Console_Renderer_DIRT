using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace dirt
{
    class Cube
    {
        vector p;

        Color c;

        Side[] sides = new Side[6];

        public Cube(vector _p, Color _c)
        {
            p = _p;
            c = _c;

            sides[0] = new Side(new vector[] {
                new vector(p.x, p.y, p.z),
                new vector(p.x + 1, p.y, p.z),
                new vector(p.x, p.y + 1, p.z),
                new vector(p.x + 1, p.y + 1, p.z)
            }, c, vector.front);
            sides[1] = new Side(new vector[] {
                new vector(p.x + 1, p.y, p.z),
                new vector(p.x + 1, p.y, p.z + 1),
                new vector(p.x + 1, p.y + 1, p.z),
                new vector(p.x + 1, p.y + 1, p.z +1)
            }, c, vector.right);
            sides[2] = new Side(new vector[] {
                new vector(p.x + 1, p.y, p.z + 1),
                new vector(p.x, p.y, p.z + 1),
                new vector(p.x + 1, p.y + 1, p.z + 1),
                new vector(p.x, p.y + 1, p.z + 1)
            }, c, vector.back);
            sides[3] = new Side(new vector[] {
                new vector(p.x, p.y, p.z + 1),
                new vector(p.x, p.y, p.z),
                new vector(p.x, p.y + 1, p.z + 1),
                new vector(p.x, p.y + 1, p.z)
            }, c, vector.left);
            sides[4] = new Side(new vector[] {
                new vector(p.x, p.y + 1, p.z),
                new vector(p.x + 1, p.y + 1, p.z),
                new vector(p.x, p.y + 1, p.z + 1),
                new vector(p.x + 1, p.y + 1, p.z + 1)
            }, c, vector.up);
            sides[5] = new Side(new vector[] {
                new vector(p.x, p.y, p.z),
                new vector(p.x + 1, p.y, p.z),
                new vector(p.x, p.y, p.z + 1),
                new vector(p.x + 1, p.y, p.z + 1)
            }, c, vector.down);


        }

        public void renderCube()
        {
            for (int i = 0; i < 6; i++)
            {
                sides[i].renderSide();
            }
        }

        public Cube checkSides(List<Cube> cubes)
        {
            Cube cb = this;

            for (int i = 0; i < cubes.Count; i++)
            {
                if (cubes[i] == this)
                {
                    continue;
                }


                for (int a = 0; a < 6; a++)
                {
                    for (int s = 0; s < 6; s++)
                    {
                        if (cubes[i].sides[s].same(sides[a].center()))
                        {
                            cb.sides[a].visible = false;
                        }
                    }
                }
            }

            return cb;
        }


    }
}
