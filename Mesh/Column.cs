using System;
namespace DCSRsH
{
	public class Column : Mesh
	{
		public Column(float radius, float height, int quality, Colour colour)
		{
			triangles = new List<Triangle>();
			Vector vectorRadius = radius * Vector.Right;
			Vector vectorHalfHeight = height / 2 * Vector.Up;
			Vector[] edge = new Vector[] { vectorRadius - vectorHalfHeight, vectorRadius + vectorHalfHeight};
			List<Vector[]> edges = new List<Vector[]>();
			for (int n = 0; n <= quality; n++)
			{
				float angle = float.Pi * 2 / quality * n;
				edges.Add(edge.Select(x => x.Rotated(0, 0, angle)).ToArray());
			}
			for (int n = 0; n < quality; n++)
			{
				Vector a = edges[n][0], b = edges[n][1];
                Vector c = edges[n + 1][0], d = edges[n + 1][1];
				Vector adcCentre = (a + d + c) / 3f, adbCentre = (a + d + b) / 3f;
				Vector centre = (a + d) / 2f;
				triangles.Add(new Triangle(a, d, c, centre.Normalised(), adcCentre, colour));
                triangles.Add(new Triangle(a, d, b, centre.Normalised(), adbCentre, colour));
            }
		}
	}
}

