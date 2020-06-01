using System;
using System.Collections.Generic;
using System.Text;

namespace dirt
{
    class Projector
    {
		public static vector projectVector(vector v)
		{
			vector res = new vector(v.x,v.y,v.z + settings.offZ);

			res = rotateVectorX(res, Program.rx);
			res = rotateVectorY(res, Program.ry);
			res = rotateVectorZ(res, Program.rz);

			res = res * projectionMatrix();

			res = depth(res);

			res = res * settings.scale;
			res = res + new vector(settings.offX, settings.offY, settings.offZ);

			return res;
		}

		public static vector depth(vector v)
		{
			if (v.z + 15 > 0)
			{
				v.x = v.x / ((v.z + 10) * 0.15f);

				v.y = v.y / ((v.z + 10) * 0.15f);
			}
			else
			{
				v.z = -1000;
			}
			return v;
		}

		public static matrix4x4 projectionMatrix()
		{
			matrix4x4 m = new matrix4x4();

			m.m[0, 0] = settings.ratio * settings.fov;
			m.m[1, 1] = settings.fov;
			m.m[2, 2] = settings.zFar / (settings.zFar - settings.zNear);
			m.m[3, 2] = (-settings.zFar * settings.zNear) / (settings.zFar - settings.zNear);
			m.m[2, 3] = 1.0f;
			m.m[3, 3] = 0.0f;

			return m;
		}

		public static vector rotateVectorX(vector v, float t)
		{
			vector r = new vector(0, 0, 0);

			r.x = v.x;
			r.y = v.y * MathF.Cos(t) + v.z * MathF.Sin(t);
			r.z = v.y * -MathF.Sin(t) + v.z * MathF.Cos(t);

			return r;
		}

		public static vector rotateVectorY(vector v, float t)
		{
			vector r = new vector(0, 0, 0);

			r.x = v.x * MathF.Cos(t) + v.z * -MathF.Sin(t);
			r.y = v.y;
			r.z = v.x * MathF.Sin(t) + v.z * MathF.Cos(t);

			return r;
		}

		public static vector rotateVectorZ(vector v, float t)
		{
			vector r = new vector(0, 0, 0);

			r.x = v.x * MathF.Cos(t) + v.y * MathF.Sin(t);
			r.y = v.x * -MathF.Sin(t) + v.y * MathF.Cos(t);
			r.z = v.z;

			return r;
		}

		public static matrix4x4 translationMatrix(vector v)
		{
			matrix4x4 matrix = new matrix4x4();
			matrix.m[0, 0] = 1.0f;
			matrix.m[1, 1] = 1.0f;
			matrix.m[2, 2] = 1.0f;
			matrix.m[3, 3] = 1.0f;
			matrix.m[3, 0] = v.x;
			matrix.m[3, 1] = v.y;
			matrix.m[3, 2] = v.z;
			return matrix;
		}

		public static matrix4x4 RotationX(float fAngleRad)
		{
			matrix4x4 matrix = new matrix4x4();
			matrix.m[0, 0] = 1.0f;
			matrix.m[1, 1] = MathF.Cos(fAngleRad);
			matrix.m[1, 2] = MathF.Sin(fAngleRad);
			matrix.m[2, 1] = -MathF.Sin(fAngleRad);
			matrix.m[2, 2] = MathF.Cos(fAngleRad);
			matrix.m[3, 3] = 1.0f;
			return matrix;
		}

		public static matrix4x4 RotationY(float fAngleRad)
		{
			matrix4x4 matrix = new matrix4x4();
			matrix.m[0, 0] = MathF.Cos(fAngleRad);
			matrix.m[0, 2] = MathF.Sin(fAngleRad);
			matrix.m[2, 0] = -MathF.Sin(fAngleRad);
			matrix.m[1, 1] = 1.0f;
			matrix.m[2, 2] = MathF.Cos(fAngleRad);
			matrix.m[3, 3] = 1.0f;
			return matrix;
		}

		public static matrix4x4 RotationZ(float fAngleRad)
		{
			matrix4x4 matrix = new matrix4x4();
			matrix.m[0, 0] = MathF.Cos(fAngleRad);
			matrix.m[0, 1] = MathF.Sin(fAngleRad);
			matrix.m[1, 0] = -MathF.Sin(fAngleRad);
			matrix.m[1, 1] = MathF.Cos(fAngleRad);
			matrix.m[2, 2] = 1.0f;
			matrix.m[3, 3] = 1.0f;
			return matrix;
		}

		public static matrix4x4 identityMatrix()
		{
			matrix4x4 matrix = new matrix4x4();
			matrix.m[0, 0] = 1.0f;
			matrix.m[1, 1] = 1.0f;
			matrix.m[2, 2] = 1.0f;
			matrix.m[3, 3] = 1.0f;
			return matrix;
		}

		public static matrix4x4 pointAtMatrix(vector pos, vector target, vector up)
		{
			// Calculate new forward direction
			vector newForward = target - pos;
			newForward = newForward.normalize();

			// Calculate new Up direction
			vector a = newForward * up.dot(newForward);
			vector newUp = up - a;
			newUp = newUp.normalize();

			// New Right direction is easy, its just cross product
			vector newRight = vector.cross(newUp, newForward);

			// Construct Dimensioning and Translation Matrix	
			matrix4x4 matrix = new matrix4x4();
			matrix.m[0, 0] = newRight.x; matrix.m[0, 1] = newRight.y; matrix.m[0, 2] = newRight.z; matrix.m[0, 3] = 0.0f;
			matrix.m[1, 0] = newUp.x; matrix.m[1, 1] = newUp.y; matrix.m[1, 2] = newUp.z; matrix.m[1, 3] = 0.0f;
			matrix.m[2, 0] = newForward.x; matrix.m[2, 1] = newForward.y; matrix.m[2, 2] = newForward.z; matrix.m[2, 3] = 0.0f;
			matrix.m[3, 0] = pos.x; matrix.m[3, 1] = pos.y; matrix.m[3, 2] = pos.z; matrix.m[3, 3] = 1.0f;
			return matrix;

		}

		public static matrix4x4 quickInverseMatrix(matrix4x4 m)
		{
			matrix4x4 matrix = new matrix4x4();
			matrix.m[0, 0] = m.m[0, 0]; matrix.m[0, 1] = m.m[1, 0]; matrix.m[0, 2] = m.m[2, 0]; matrix.m[0, 3] = 0.0f;
			matrix.m[1, 0] = m.m[0, 1]; matrix.m[1, 1] = m.m[1, 1]; matrix.m[1, 2] = m.m[2, 1]; matrix.m[1, 3] = 0.0f;
			matrix.m[2, 0] = m.m[0, 2]; matrix.m[2, 1] = m.m[1, 2]; matrix.m[2, 2] = m.m[2, 2]; matrix.m[2, 3] = 0.0f;
			matrix.m[3, 0] = -(m.m[3, 0] * matrix.m[0, 0] + m.m[3, 1] * matrix.m[1, 0] + m.m[3, 2] * matrix.m[2, 0]);
			matrix.m[3, 1] = -(m.m[3, 0] * matrix.m[0, 1] + m.m[3, 1] * matrix.m[1, 1] + m.m[3, 2] * matrix.m[2, 1]);
			matrix.m[3, 2] = -(m.m[3, 0] * matrix.m[0, 2] + m.m[3, 1] * matrix.m[1, 2] + m.m[3, 2] * matrix.m[2, 2]);
			matrix.m[3, 3] = 1.0f;
			return matrix;
		}
	}
}
