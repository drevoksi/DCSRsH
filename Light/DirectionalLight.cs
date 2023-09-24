using System;
namespace DCSRsH
{
	public class DirectionalLight : Light
	{
		public Vector direction;
		public DirectionalLight(Colour colour, Vector newDirection) : base(colour)
		{
			direction = newDirection;
		}
		public override Colour GetLighting(Vector position, Vector normal)
			=> CalculuF.UnitClamp(-Vector.CosineBetweenVectors(normal, direction)) * colour;
    }
}

