using System;
namespace DCSRsH
{
	public class Triangle
    {
        public readonly Vector[] vertices;
        public Vector normal;
        public Vector centre;
        public Colour colour;
        public Triangle(Vector a, Vector b, Vector c, Vector newNormal, Vector newCentre, Colour newColour)
        {
            vertices = new Vector[3];
            vertices[0] = a;
            vertices[1] = b;
            vertices[2] = c;
            normal = newNormal.Normalised();
            centre = newCentre;
            colour = newColour;
        }
        public Triangle(Triangle triangle)
        {
            vertices = new Vector[3];
            for (int i = 0; i < 3; i++) vertices[i] = triangle.vertices[i];
            normal = triangle.normal;
            centre = triangle.centre;
            colour = triangle.colour;
        }
        public Vector this[int index]
        {
            get { return vertices[index]; }
        }
        public void Rotate(Vector angles)
        {
            for (int i = 0; i < 3; i++) vertices[i].Rotate(angles);
            normal.Rotate(angles);
            centre.Rotate(angles);
        }
        public Triangle Rotated(Vector angles)
        {
            Triangle triangle = new Triangle(this);
            triangle.Rotate(angles);
            return triangle;
        }
        public void Displace(Vector displacement)
        {
            for (int i = 0; i < 3; i++) vertices[i] += displacement;
            centre += displacement;
        }
        public Triangle Displaced(Vector displacement)
        {
            Triangle triangle = new Triangle(this);
            triangle.Displace(displacement);
            return triangle;
        }
    }
}

