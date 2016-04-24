using KSP_Chroma_Control.ColorSchemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control.Animations
{
    internal class AnimationUtils
    {
        public static ColorScheme[] InterpolateFrames(ColorScheme from, ColorScheme to, int steps)
        {
            ColorScheme[] myReturn = new ColorScheme[steps];
            myReturn[0] = from;
            myReturn[steps - 1] = to;

            Action<ColorScheme, ColorScheme, int, int> interpolateRecursive = null;
            interpolateRecursive = (localFrom, localTo, start, stop) =>
            {
                int center = start + ((stop - start) / 2);
                if (start != stop && center != start)
                {
                    myReturn[center] = InterpolateFrames(myReturn[start], myReturn[stop]);
                    interpolateRecursive(myReturn[0], myReturn[center], 0, center);
                    interpolateRecursive(myReturn[center], myReturn[stop], center, stop);
                }
            };

            interpolateRecursive(from, to, 0, steps - 1);
            return myReturn;
        }

        public static ColorScheme InterpolateFrames(ColorScheme from, ColorScheme to)
        {
            ColorScheme myReturn = new ColorScheme();
            KeyCode[,] keys = Config.Instance.KeyByPosition;

            foreach (KeyCode key in keys)
            {
                if (myReturn.ContainsKey(key))
                {
                    myReturn[key] = new Color(
                        (from[key].r + to[key].r) / 2f,
                        (from[key].g + to[key].g) / 2f,
                        (from[key].b + to[key].b) / 2f,
                        255
                    );
                }
            }

            return myReturn;
        }

        public static double GetDistanceFromCenter(int x, int y)
        {
            int distanceX = x - (Config.Instance.KeyByPosition.GetLength(1) / 2);
            int distanceY = x - (Config.Instance.KeyByPosition.GetLength(0) / 2);
            double distance = Math.Sqrt(
                distanceX * distanceX
                + distanceY * distanceY
            );

            Debug.Log("Distance: (" + x + ":" + y + ") => " + distance);

            return distance;
        }

        public static ColorScheme CircularSineWave(Color one, Color two, double offset)
        {
            ColorScheme myReturn = new ColorScheme(one);

            for (int y = 0; y < Config.Instance.KeyByPosition.GetLength(0); y++)
                for (int x = 0; x < Config.Instance.KeyByPosition.GetLength(1); x++)
                {
                    Color newColor = new Color();
                    double distance = GetDistanceFromCenter(x, y);

                    if (offset > distance)
                    {

                        float sinus = (float)Math.Sin(distance - (offset * Math.PI / 10.0)) + 1f;

                        newColor.r = (one.r * (2f - sinus) + two.r * sinus) / 2f;
                        newColor.g = (one.g * (2f - sinus) + two.g * sinus) / 2f;
                        newColor.b = (one.b * (2f - sinus) + two.b * sinus) / 2f;
                        newColor.a = 1f;

                        myReturn.SetKeyToColor(x, y, newColor);
                    }
                }

            return myReturn;
        }

        public static ColorScheme GaussBlur(ColorScheme original)
        {
            float[,] matrix = new float[3, 3]
            {
                { 1/16f, 2/16f, 1/16f },
                { 2/16f, 16/16f, 2/16f },
                { 1/16f, 2/16f, 1/16f }
            };

            return ApplyMatrixFilter(original, matrix);
        }

        public static ColorScheme ApplyMatrixFilter(ColorScheme original, float[,] matrix)
        {
            ColorScheme myReturn = new ColorScheme();

            KeyCode[,] keys = Config.Instance.KeyByPosition;

            for (int y = 0; y < keys.GetLength(0); y++)
                for (int x = 0; x < keys.GetLength(1); x++)
                {
                    myReturn.SetKeyToColor(x, y, FilterPixel(original, matrix, x, y));
                }

            return myReturn;
        }

        private static Color FilterPixel(ColorScheme original, float[,] matrix, int origX, int origY)
        {
            try
            {
                Color oldColor = original[Config.Instance.KeyByPosition[origY, origX]];
                Color newColor;

                if (matrix.GetLength(0) % 2 == 1 && matrix.GetLength(1) % 2 == 1)
                {
                    newColor = Color.black;
                    int halfX = (matrix.GetLength(0) - 1) / 2;
                    int halfY = (matrix.GetLength(1) - 1) / 2;

                    /// Dont start below 0
                    int startX = (origX - halfX < 0) ? 0 : (origX - halfX);
                    int startY = (origY - halfY < 0) ? 0 : (origY - halfY);

                    for (int y = 0; y < matrix.GetLength(1); y++)
                        for (int x = 0; x < matrix.GetLength(0); x++)
                        {
                            try
                            {
                                Color source = original[Config.Instance.KeyByPosition[startY + y, startX + x]];
                                float factor = matrix[x, y];
                                newColor.r += source.r * factor;
                                newColor.g += source.g * factor;
                                newColor.b += source.b * factor;
                                newColor.a += source.a; // * factor; // We don't want to interpolate alpha colors
                            }
                            catch (Exception e)
                            {
                                Debug.LogWarning("invalid index: " + e.Message);
                            }
                        }
                }
                else
                {
                    newColor = oldColor;
                    Debug.LogError("Error: Using filter matrices with an even number of colums/rows is not allowed. Dimesions were " + matrix.GetLength(0) + ":" + matrix.GetLength(1));
                }

                Debug.LogWarning("OldColor(" + origX + "," + origY + "): " + oldColor.ToString() + " => " + newColor.ToString());
                return newColor;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return Color.black;
            }
        }

        private AnimationUtils() { }

    }
}
