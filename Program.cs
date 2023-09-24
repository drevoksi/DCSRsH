using DCSRsH;
using System.Diagnostics;
class Program
{
    static void Main(string[] args)
    {
        // example code, hover over constructors!
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        PNGCamera camera = new PNGCamera(new Vector(3, 1.5f, 2), 600, new Colour(42));
        camera.lights.Add(new AmbientLight(new Colour(0.34f)));
        camera.lights.Add(new DirectionalLight(new Colour(0.66f), new Vector(-1, 1, -1)));
        camera.Position = new Vector(0, -18, 0);
        Mesh donut = new Donut(7, 3, 8, new Colour(240, 193, 24));
        foreach (Triangle triangle in donut.triangles)
        {
            if (triangle.centre.z > MathF.Sin((triangle.centre.x + triangle.centre.y) * 2f) * 0.28f + 0.8f)
                triangle.colour = (1 + MathF.Sin((triangle.centre.x + triangle.centre.y + 1) * 0.8f) * 0.15f) * new Colour(24, 52, 163);
            else if (triangle.centre.z > -0.7f && triangle.centre.z < 0.2f) triangle.colour = new Colour(255, 207, 36);
        }
        camera.meshes.Add(donut);
        Vector angles = new Vector(float.Pi * 6, float.Pi * 10, float.Pi * 4);
        int frames = 720;
        Console.WriteLine($"Setup in {stopwatch.ElapsedMilliseconds} ms");
        stopwatch.Restart();
        long lastFrameTime = 0;
        for (int n = 1; n <= frames; n++)
        {
            camera.Render();
            donut.Rotation = (float)n / frames * angles;
            Console.Clear();
            Console.WriteLine($"{n}/{frames}");
            Console.WriteLine($"Frame in {stopwatch.ElapsedMilliseconds - lastFrameTime} ms");
            Console.WriteLine($"Estimated time remaining: {stopwatch.ElapsedMilliseconds * (frames - n) / n / 1000} s");
            lastFrameTime = stopwatch.ElapsedMilliseconds;
        }
    }
}
