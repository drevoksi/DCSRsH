using System;
namespace DCSRsH
{
	public abstract class Camera : IArrangeable
	{
        public Vector Position { get; set; } = Vector.Zero;
        public Vector Rotation { get; set; } = Vector.Zero;
        public List<Mesh> meshes;
        public List<Light> lights;
        public Vector frame;
        public int resolutionX;
        public int resolutionY;
        public Colour backgroundColour;
        public Camera(Vector newFrame, int resolution, Colour newBackgroundColour)
        {
            frame = newFrame;
            resolutionX = resolution;
            resolutionY = CalculuF.RoundToInt(resolution * frame.z / frame.x);
            backgroundColour = newBackgroundColour;
            meshes = new List<Mesh>();
            lights = new List<Light>();
            colourMap = new Colour[resolutionX, resolutionY];
            depthMap = new float[resolutionX, resolutionY];
            normalMap = new Vector[resolutionX, resolutionY];
        }
        public Colour[,] colourMap;
        public float[,] depthMap;
        public Vector[,] normalMap;
        protected void DrawMaps()
        {
            colourMap = new Colour[resolutionX, resolutionY];
            depthMap = new float[resolutionX, resolutionY];
            for (int y = 0; y < resolutionY; y++)
                for (int x = 0; x < resolutionX; x++)
                    colourMap[x, y] = backgroundColour;
            PriorityQueue<Triangle, float> trianglesToRender = new PriorityQueue<Triangle, float>();
            foreach (Mesh mesh in meshes)
            {
                foreach (Triangle triangle in mesh.GetTriangles())
                {
                    if (triangle.centre.y - Position.y <= frame.y + 1f) continue;
                    Vector distance = triangle.centre - Position;
                    if (Vector.CosineBetweenVectors(distance, triangle.normal) >= 0) continue;
                    trianglesToRender.Enqueue(triangle, -distance.SquaredMagnitude());
                }
            }
            while (trianglesToRender.Count > 0)
            {
                Triangle triangle = trianglesToRender.Dequeue();
                Colour lighting = new Colour(0);
                foreach (Light light in lights) lighting += light.GetLighting(triangle.centre, triangle.normal);
                RenderTriangle(triangle, lighting);
            }
        }
        private void RenderTriangle(Triangle triangle, Colour lighting)
        {
            Colour colour = lighting * triangle.colour;
            float reciprocalDistance = 1 / (triangle.centre - Position).Magnitude();
            (int, int)[] screenspaceVertices = triangle.vertices.Select(ToScreenspace).ToArray();
            foreach ((int, int) coordinates in TriangleCoordinates(screenspaceVertices))
            {
                if (coordinates.Item1 < 0 || coordinates.Item1 >= resolutionX || coordinates.Item2 < 0 || coordinates.Item2 >= resolutionY) continue;
                colourMap[coordinates.Item1, coordinates.Item2] = colour;
                depthMap[coordinates.Item1, coordinates.Item2] = reciprocalDistance;
                normalMap[coordinates.Item1, coordinates.Item2] = triangle.normal;
            }
        }
        private (int, int)[] TriangleCoordinates((int, int)[] screenspaceVertices)
        {
            screenspaceVertices = screenspaceVertices.OrderBy(x => x.Item2).ToArray();
            if (screenspaceVertices[0].Item2 == screenspaceVertices[1].Item2)
                return BaseTriangleCoordinates(screenspaceVertices[0], screenspaceVertices[1], screenspaceVertices[2]);
            if (screenspaceVertices[1].Item2 == screenspaceVertices[2].Item2)
                return BaseTriangleCoordinates(screenspaceVertices[1], screenspaceVertices[2], screenspaceVertices[0]);
            int ox = screenspaceVertices[0].Item1;
            int oy = screenspaceVertices[0].Item2;
            int dy = screenspaceVertices[1].Item2 - oy;
            int x = screenspaceVertices[2].Item1 - ox;
            int y = screenspaceVertices[2].Item2 - oy;
            (int, int) q = (ox + x * dy / y, oy + dy);
            List<(int, int)> coordinates = new List<(int, int)>();
            coordinates.AddRange(BaseTriangleCoordinates(q, screenspaceVertices[1], screenspaceVertices[2]));
            coordinates.AddRange(BaseTriangleCoordinates(q, screenspaceVertices[1], screenspaceVertices[0]));
            return coordinates.ToArray();
        }
        private (int, int)[] BaseTriangleCoordinates((int, int) firstBaseVertex, (int, int) secondBaseVertex, (int, int) vertex)
        {
            List<(int, int)> coordinates = new List<(int, int)>();
            int dy = firstBaseVertex.Item2 - vertex.Item2;
            if (dy == 0) return LineCoordinates(Math.Min(Math.Min(firstBaseVertex.Item1, secondBaseVertex.Item1), vertex.Item1), Math.Max(Math.Max(firstBaseVertex.Item1, secondBaseVertex.Item1), vertex.Item1), vertex.Item2);
            int dysign = dy > 0 ? 1 : -1;
            int dyabs = dy * dysign;
            int vx = vertex.Item1;
            int vy = vertex.Item2;
            int ax = firstBaseVertex.Item1;
            int bx = secondBaseVertex.Item1;
            int dax = ax - vx;
            int dbx = bx - vx;
            for (int n = 0; n <= dyabs; n++)
            {
                int fromX = vx + (int)((float)dax * n / dyabs);
                int toX = vx + (int)((float)dbx * n / dyabs);
                int y = vy + n * dysign;
                coordinates.AddRange(LineCoordinates(fromX, toX, y));
            }
            return coordinates.ToArray();
        }
        private (int, int)[] LineCoordinates(int fromX, int toX, int y)
        {
            (fromX, toX) = (Math.Min(fromX, toX), Math.Max(fromX, toX));
            List<(int, int)> coordinates = new List<(int, int)>();
            for (int x = fromX; x <= toX + 1; x++)
                coordinates.Add((x, y));
            return coordinates.ToArray();
        }
        private (int, int) ToScreenspace(Vector vector)
        {
            Func<float, int> round = x => Convert.ToInt32(MathF.Round(x));
            float deltaX = vector.x - Position.x;
            float deltaY = vector.y - Position.y;
            float deltaZ = vector.z - Position.z;
            float k = frame.y / deltaY;
            float x = deltaX * k;
            float z = deltaZ * k;
            return (resolutionX / 2 + CalculuF.RoundToInt(x / frame.x * resolutionX), resolutionY / 2 - CalculuF.RoundToInt(z / frame.z * resolutionY));
        }
        public abstract void Render();
    }
}

