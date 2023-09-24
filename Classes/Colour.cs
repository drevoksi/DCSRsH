using System;
using SkiaSharp;
namespace DCSRsH
{
	public struct Colour
	{
        public static Colour White => new Colour(255);
        public static Colour Black => new Colour(0);
        public static Colour operator *(float f, Colour colour)
			=> new Colour(colour.red * f, colour.green * f, colour.blue * f);
        public static Colour operator *(Colour a, Colour b)
			=> new Colour(a.red * b.red, a.green * b.green, a.blue * b.blue);
		public static Colour operator +(Colour a, Colour b)
			=> new Colour(a.red + b.red, a.green + b.green, a.blue + b.blue);
        public float red;
		public float green;
		public float blue;
		public Colour(int newRed, int newGreen, int newBlue)
		{
            red = newRed / 255f;
            green = newGreen / 255f;
            blue = newBlue / 255f;
            Clamp();
        }
		public Colour(float newRed, float newGreen, float newBlue)
		{
			red = newRed;
			green = newGreen;
			blue = newBlue;
			Clamp();
        }
        public Colour(int grey)
        {
            red = grey / 255f;
            green = grey / 255f;
            blue = grey / 255f;
            Clamp();
        }
        public Colour(float grey)
		{
			red = grey;
			green = grey;
			blue = grey;
			Clamp();
		}
		public SKColor ToSKColor() => new SKColor((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
		public float ToGreyscale() => red * 0.3f + green * 0.58f + blue * 0.12f;
		public override string ToString() => $"[{red}, {green}, {blue}]";
        private void Clamp()
		{
            red = CalculuF.UnitClamp(red);
            green = CalculuF.UnitClamp(green);
            blue = CalculuF.UnitClamp(blue);
        }
	}
}

