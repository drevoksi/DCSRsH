using System;
using SkiaSharp;
namespace DCSRsH
{
	public class PNGCamera : Camera
	{
        readonly string path;
        int count;
        public PNGCamera(Vector frame, int resolution, Colour backgroundColour) : base(frame, resolution, backgroundColour)
        {
            path = "PNGRenders/Render" + DateTime.Now.ToString(" yy-MM-dd HH:mm:ss");
            Directory.CreateDirectory(path);
            count = 0;
        }
        public override void Render()
        {
            DrawMaps();
            SKBitmap bitmap = new SKBitmap(resolutionX, resolutionY);
            for (int y = 0; y < resolutionY; y++)
            {
                for (int x = 0; x < resolutionX; x++)
                {
                    SKColor color = colourMap[x, y].ToSKColor();
                    bitmap.SetPixel(x, y, color);
                }
            }
            count++;
            File.WriteAllBytes(path + "/Render" + count + ".png", bitmap.Encode(SKEncodedImageFormat.Png, 100).ToArray());
        }
    }
}

