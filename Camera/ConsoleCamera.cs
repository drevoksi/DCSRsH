using System;
using System.Text;
namespace DCSRsH
{
	public class ConsoleCamera : Camera
	{
        public ConsoleCamera(Vector frame, int resolution) : base(frame, resolution, Colour.Black) { }
        public char[] table = """ .:-=+*#%@""".ToCharArray();
        public override void Render()
        {
            DrawMaps();
            StringBuilder builder = new StringBuilder();
            for (int y = 0; y < resolutionY; y += 2)
            {
                for (int x = 0; x < resolutionX; x++)
                {
                    builder.Append(table[CalculuF.RoundToInt(colourMap[x, y].ToGreyscale() * (table.Length - 1))]);
                }
                builder.Append('\n');
            }
            Console.WriteLine(builder.ToString());
        }
    }
}

