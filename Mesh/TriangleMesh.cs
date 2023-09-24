using System;
namespace DCSRsH
{
	public class TriangleMesh : Mesh
	{
		public TriangleMesh(Vector a, Vector b, Vector c, Colour colour) : base()
		{
			triangles.Add(new Triangle(a, b, c, Vector.GetNormalToTriangle(a, b, c), (a + b + c) / 3, colour));
		}
	}
}

