using System;
namespace DCSRsH
{
	public abstract class Mesh : IArrangeable
	{
        public Vector Position { get; set; } = Vector.Zero;
        public Vector Rotation { get; set; } = Vector.Zero;
		public Triangle[] GetTriangles() => triangles.Select(x => x.Rotated(Rotation)).ToArray();
        public List<Triangle> triangles;
        public Mesh()
        {
            triangles = new List<Triangle>();
        }
        public Mesh Displace(Vector displacement)
        {
            Position += displacement;
            return this;
        }
        public Mesh Rotate(Vector rotation)
        {
            Rotation += rotation;
            return this;
        }
    }
}

