using System;
namespace DCSRsH
{
	public abstract class Light : IArrangeable
	{
		public Vector Position { get; set; } = Vector.Zero;
		public Vector Rotation { get; set; } = Vector.Zero;
		public Colour colour;
		public Light(Colour newColour)
		{
			colour = newColour;
		}
		public abstract Colour GetLighting(Vector position, Vector normal);
	}
}

