using System;
using System.Drawing;
using TwoBRenn.Engine.Render.Textures;

namespace TwoBRenn.Common
{
    class FractalTextureGenerator
    {
        public enum Fractal
        {
            Koch
        }

        public Texture GetTexture(Fractal fractal)
        {
            switch (fractal)
            {
                case Fractal.Koch: return GetKochTexture();
                default: return null;
            }
        }

        public Texture GetKochTexture()
        {
            Bitmap image = new Bitmap(256, 256);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.AliceBlue);
            Pen pen = new Pen(Color.Black);
            DrawKochLine(graphics, pen, new Point(0, 0), new Point(255, 0), 0, 8);
            graphics.Dispose();
            return new Texture(image);
        }

        public void DrawKochLine(Graphics g, Pen pen, Point a, Point b, double fi, int n)
        {
            if (n <= 0)
            {
                g.DrawLine(pen, a.X, a.Y, b.X, b.Y);
            }
            else
            {
                double length = Math.Pow(Math.Pow(b.Y - a.Y, 2) + Math.Pow(b.X - a.X, 2), 0.5);
                double length1Of3 = length / 3;

                // находим т., делящую отрезок как 1:3.
                Point a1 = new Point(a.X + (int)Math.Round(length1Of3 * Math.Cos(fi)),
                    a.Y + (int)Math.Round(length1Of3 * Math.Sin(fi)));

                // находим т., делящую отрезок как 2:3.
                Point b1 = new Point(a1.X + (int)Math.Round(length1Of3 * Math.Cos(fi)),
                    a1.Y + (int)Math.Round(length1Of3 * Math.Sin(fi)));

                // находим т., которая будет вершиной равностороннего
                // треугольника.
                Point c = new Point(a1.X + (int)Math.Round(length1Of3 * Math.Cos(fi + Math.PI / 3)),
                    a1.Y + (int)Math.Round(length1Of3 * Math.Sin(fi + Math.PI / 3)));

                n--;
                DrawKochLine(g, pen, a1, c, fi + Math.PI / 3, n);
                DrawKochLine(g, pen, c, b1, fi - Math.PI / 3, n);

                n--;
                DrawKochLine(g, pen, a, a1, fi, n);
                DrawKochLine(g, pen, b1, b, fi, n);
            }
        }
    }
}
