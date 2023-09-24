using System;

namespace DCSRsH
{
	public struct Vector
	{
		public static Vector Right { get { return new Vector(1, 0, 0); } }
        public static Vector Forward { get { return new Vector(0, 1, 0); } }
        public static Vector Up { get { return new Vector(0, 0, 1); } }
        public static Vector Zero { get { return new Vector(0, 0, 0); } }
		public static float DotProduct(Vector a, Vector b)
			=> a.x * b.x + a.y * b.y + a.z * b.z;
		public static float CosineBetweenVectors(Vector a, Vector b)
			=> DotProduct(a, b) / a.Magnitude() / b.Magnitude();
		public static float AngleBetweenVectors(Vector a, Vector b)
			=> MathF.Acos(CosineBetweenVectors(a, b));
        public static Vector Interpolate(Vector a, Vector b, float t)
			=> (1 - t) * a + t * b;
		public static Vector GetNormalToTriangle(Vector a, Vector b, Vector c)
		{
			float x = (a.y * b.z - a.z * b.y);
			float y = -(a.x * b.z - a.z - b.x);
			float z = (a.x * b.y - a.y - b.x);
			return new Vector(x, y, z);
		}
		public static Vector[] GetClosedCircle(float radius, int sides)
		{
			List<Vector> vertices = new List<Vector>();
			float angle = float.Pi * 2 / sides;
			for (int n = 0; n <= sides; n++)
				vertices.Add((radius * Vector.Right).Rotated(0, 0, angle * n));
			return vertices.ToArray();
		}
		public static Vector operator *(float f, Vector vector)
		{
			return new Vector(vector).Transformed(x => x * f);
		}
        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static Vector operator /(Vector vector, float f)
        {
            return new Vector(vector).Transformed(x => x / f);
        }
        public static Vector operator /(Vector a, Vector b)
        {
            return new Vector(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public float x;
		public float y;
		public float z;
		public Vector(float newX, float newY, float newZ)
		{
			(x, y, z) = (newX, newY, newZ);
		}
		public Vector(Vector vector)
		{
			(x, y, z) = (vector.x, vector.y, vector.z);
		}
		public override string ToString() => $"[{x}, {y}, {z}]";
		/// <summary>
		/// Rotate around x, y and z axes, in radians.
		/// </summary>
		public void Rotate(float xRotation, float yRotation, float zRotation)
		{
			float a = xRotation;
			float b = yRotation;
			float c = zRotation;
			Func<float, float> sin = MathF.Sin;
            Func<float, float> cos = MathF.Cos;
			(x, y, z) = ((cos(b) * cos(c)) * x + (sin(a) * sin(b) * cos(c) - cos(a) * sin(c)) * y + (cos(a) * sin(b) * cos(c) + sin(a) * sin(c)) * z,
						 (cos(b) * sin(c)) * x + (sin(a) * sin(b) * sin(c) + cos(a) * cos(c)) * y + (cos(a) * sin(b) * sin(c) - sin(a) * cos(c)) * z,
						 (-sin(b)) * x + (sin(a) * cos(b)) * y + (cos(a) * cos(b)) * z);
			Transform(c => MathF.Round(c, 6));
        }
        /// <summary>
        /// Rotate around x, y and z axes, in radians.
        /// </summary>
        public void Rotate(Vector angles)
		{
			Rotate(angles.x, angles.y, angles.z);
		}
        /// <summary>
        /// Get vector rotated around x, y and z axes, in radians.
        /// </summary>
        public Vector Rotated(float xRotation, float yRotation, float zRotation)
		{
			Vector rotated = new Vector(this);
			rotated.Rotate(xRotation, yRotation, zRotation);
			return rotated;
		}
        /// <summary>
        /// Get vector rotated around x, y and z axes, in radians.
        /// </summary>
        public Vector Rotated(Vector angles)
        {
            Vector rotated = new Vector(this);
            rotated.Rotate(angles);
            return rotated;
        }
        public Vector Normalised()
		{
			float magnitude = Magnitude();
			if (magnitude == 0) return Vector.Zero;
			return new Vector(this).Transformed(x => x / magnitude);
		}
		public float SquaredMagnitude()
			=> x * x + y * y + z * z;
		public float Magnitude()
			=> MathF.Sqrt(SquaredMagnitude());
		public void Transform(Func<float, float> function)
		{
			x = function(x);
			y = function(y);
			z = function(z);
		}
		public Vector Transformed(Func<float, float> function)
		{
			Vector vector = new Vector(this);
			vector.Transform(function);
			return vector;
		}
	}
}

