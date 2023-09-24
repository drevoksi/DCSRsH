using System;
namespace DCSRsH
{
	public class Donut : Mesh
	{
		public Donut(float radius, float crossRadius, int quality, Colour colour) : base()
		{
            Vector[] circle = Vector.GetClosedCircle(1, quality);
			Vector[] baseCircle = circle.Select(x => radius * x).ToArray();
			Vector[] crossSection = circle.Select(x => crossRadius * x.Rotated(float.Pi / 2, 0, 0) + radius * Vector.Right).ToArray();
			List<Vector[]> crossSections = new List<Vector[]>();
			for (int n = 0; n <= quality; n++)
			{
				float angle = float.Pi * 2 * n / quality;
				crossSections.Add(crossSection.Select(x => x.Rotated(0, 0, angle)).ToArray());
			}
			for (int n = 0; n < baseCircle.Length - 1; n++)
			{
				Vector[] from = crossSections[n], to = crossSections[n + 1];
				Vector centre = Vector.Interpolate(baseCircle[n], baseCircle[n + 1], 0.5f);
				for (int i = 0; i < from.Length - 1; i++)
				{
					Vector a = from[i], b = from[i + 1];
					Vector c = to[i], d = to[i + 1];
					Vector adcCentre = (a + d + c) / 3, adbCentre = (a + d + b) / 3;
					Vector normal = (a + d) / 2 - centre;
                    triangles.Add(new Triangle(a, d, c, normal, adcCentre, colour));
                    triangles.Add(new Triangle(a, d, b, normal, adbCentre, colour));
                }
			}
        }
	}
}

