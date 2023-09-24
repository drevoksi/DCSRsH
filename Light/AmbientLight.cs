using System;
namespace DCSRsH
{
	public class AmbientLight : Light
	{
		public AmbientLight(Colour colour) : base(colour) { }
		public override Colour GetLighting(Vector position, Vector normal)
			=> colour;
    }
}

