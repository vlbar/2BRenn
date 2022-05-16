using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwoBRenn.Engine.Render.Textures;

namespace TwoBRenn.Common
{
    class TextureTransformationManager
    {
        public enum Filter
        {
            Sharpening
        }

        private Dictionary<Texture, Texture> textureAndHisFiltered = new Dictionary<Texture, Texture>();

        public Texture RevertFilter(Texture texture)
        {
            if (textureAndHisFiltered.ContainsValue(texture))
            {
                return textureAndHisFiltered.ToList().Find(x => x.Value == texture).Key;
            }
            return texture;
        }

        public Texture ApplyFilter(Filter filter, Texture forTexture)
        {
            if (textureAndHisFiltered.ContainsKey(forTexture))
            {
                return textureAndHisFiltered[forTexture];
            }

            Texture texture = CreateFilteredTexture(filter, forTexture);
            textureAndHisFiltered.Add(forTexture, texture);
            return texture;
        }

        private Texture CreateFilteredTexture(Filter filter, Texture texture)
        {
            Bitmap image = new Bitmap(texture.Image);
            switch (filter)
            {
                case Filter.Sharpening: Sharpen(image);
                    break;
            }

            Texture filteredTexture = new Texture(image);
            return filteredTexture;
        }

        public void Sharpen(Bitmap image)
        {
            // собираем матрицу
            float[] mat = new float[9];
            mat[0] = -0.1f;
            mat[1] = -0.1f;
            mat[2] = -0.1f;
            mat[3] = -0.1f;
            mat[4] = 1.8f;
            mat[5] = -0.1f;
            mat[6] = -0.1f;
            mat[7] = -0.1f;
            mat[8] = -0.1f;

            PixelTransformation(image, mat, 0, 1);
        }

        public void PixelTransformation(Bitmap image, float[] mat, int corr, float coeff)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    // результирующий цвет пикселя
                    float[] resaultRgb = new float[3];

                    for (int c = 0; c < 3; c++)
                    {
                        // обнуление счетчика обработок
                        var count = 0;

                        int bx;
                        for (bx = -1; bx <= 1; bx++)
                        {
                            int ax;
                            for (ax = -1; ax <= 1; ax++)
                            {
                                // если мы не попали в рамки, просто используем центральный пиксель, и продолжаем цикл
                                if (x + ax < 0 || x + ax > image.Width - 1 || y + bx < 0 || y + bx > image.Height - 1)
                                {
                                    // считаем составляющую в одной из точек, используем коэфицент в матрице
                                    // (под номером текущей итерации), коэфицент усиления (coeff) и прибовляем коррекцию (corr)
                                    if(c == 0) resaultRgb[c] += image.GetPixel(x, y).R * mat[count] * coeff + corr;
                                    if(c == 1) resaultRgb[c] += image.GetPixel(x, y).G * mat[count] * coeff + corr;
                                    if(c == 2) resaultRgb[c] += image.GetPixel(x, y).B * mat[count] * coeff + corr;

                                    // счетчик обработок = ячейке матрицы с необходимым коэфицентом
                                    count++;
                                    continue;
                                }

                                // иначе, если мы укладываемся в изображение (не пересекаем границы), используем соседние пиксели, корректируем ячейку массива параметрами ax, bx
                                if (c == 0) resaultRgb[c] += image.GetPixel(x + ax, y + bx).R * mat[count] * coeff + corr;
                                if (c == 1) resaultRgb[c] += image.GetPixel(x + ax, y + bx).G * mat[count] * coeff + corr;
                                if (c == 2) resaultRgb[c] += image.GetPixel(x + ax, y + bx).B * mat[count] * coeff + corr;

                                count++;
                            }
                        }
                    }

                    // теперь для всех составляющих корректируем цвет
                    for (int c = 0; c < 3; c++)
                    {
                        if (resaultRgb[c] < 0)
                        {
                            resaultRgb[c] = 0;
                        }
                        
                        if (resaultRgb[c] > 255)
                        {
                            resaultRgb[c] = 255;
                        }
                    }
                    // записываем в массив цветов слоя новое значение
                    image.SetPixel(x, y, Color.FromArgb((int)resaultRgb[0], (int)resaultRgb[1], (int)resaultRgb[2]));
                }
            }

        }
    }
}
