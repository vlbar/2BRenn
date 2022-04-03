using System;
using System.Drawing;

namespace TwoBRenn.Engine.Common.ObjectsPlacers
{
    public class ImageMap
    {
        public static float[,] GenerateMap(string imagePath, int resolution = 1)
        {
            Bitmap img = new Bitmap(imagePath);
            float[,] map = new float[img.Width / resolution, img.Height / resolution];
            for (int i = 0; i < img.Width / resolution; i++)
            {
                for (int j = 0; j < img.Height / resolution; j++)
                {
                    map[i, j] = img.GetPixel(i * resolution, j * resolution).R / 255.0f;
                }
            }
            return map;
        }
    }
}