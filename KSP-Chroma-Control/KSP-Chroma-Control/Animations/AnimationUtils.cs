using KspChromaControl.ColorSchemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KspChromaControl.Animations
{
    /// <summary>
    /// Utility class that contains many useful functions for displaying animations on the keyboard.
    /// </summary>
    internal class AnimationUtils
    {
        /// <summary>
        /// Interpolates a number of frames for a smooth transition between the provided color schemes.
        /// </summary>
        /// <param name="from">The color scheme to transition from</param>
        /// <param name="to">The color scheme to transition to</param>
        /// <param name="steps">The number of steps to take</param>
        /// <returns></returns>
        public static ColorScheme[] InterpolateFrames(ColorScheme from, ColorScheme to, int steps)
        {
            ColorScheme[] myReturn = new ColorScheme[steps];
            myReturn[0] = from;
            myReturn[steps - 1] = to;
            KeyCode[,] keys = Config.Instance.KeyByPosition;

            for (int frame = 1; frame < steps - 1; frame++)
            {
                ColorScheme frameScheme = new ColorScheme(myReturn[0].baseColor);

                foreach (KeyCode key in keys)
                {
                    float newR = myReturn[frame - 1][key].r + (to[key].r - from[key].r) / steps;
                    float newG = myReturn[frame - 1][key].g + (to[key].g - from[key].g) / steps;
                    float newB = myReturn[frame - 1][key].b + (to[key].b - from[key].b) / steps;

                    Color keyColor = new Color(newR, newG, newB);
                    frameScheme[key] = keyColor;
                }

                myReturn[frame] = frameScheme;
            }

            return myReturn;
        }

        /// <summary>
        /// Calculates the distance of a key from the center of the keyboard.
        /// </summary>
        /// <param name="x">The x coordinate of the key</param>
        /// <param name="y">The y coordinate of the key</param>
        /// <returns></returns>
        public static double GetDistanceFromCenter(int x, int y)
        {
            int distanceX = x - (Config.Instance.KeyByPosition.GetLength(1) / 2);
            int distanceY = y - (Config.Instance.KeyByPosition.GetLength(0) / 2);
            double distance = Math.Sqrt(
                distanceX * distanceX
                + distanceY * distanceY
            );

            return distance;
        }

        /// <summary>
        /// Colors the keyboard in a circular sine wave from the center with the given offset.
        /// </summary>
        /// <param name="one">The base color</param>
        /// <param name="two">The color for the wave peaks</param>
        /// <param name="offset">The offset used to animate the scene (offset + 1 => next scene)</param>
        /// <returns></returns>
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

        /// <summary>
        /// Very simple gauss blur over the current color scheme.
        /// </summary>
        /// <param name="original">the original color scheme</param>
        /// <returns>the gauss-smoothed color scheme</returns>
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

        /// <summary>
        /// Allows you to apply any matrix filter to a given color scheme, as long as the matrix is of uneven
        /// width and height.
        /// </summary>
        /// <param name="original">the original color scheme</param>
        /// <param name="matrix">the transformation matrix</param>
        /// <returns></returns>
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

        /// <summary>
        /// Calculates the resulting pixel from an original using the supplied transformation matrix.
        /// </summary>
        /// <param name="original">the original color scheme</param>
        /// <param name="matrix">the transformation matrix</param>
        /// <param name="origX">the pixel x-coordinate</param>
        /// <param name="origY">the pixel y-coordinate</param>
        /// <returns></returns>
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
                                newColor.a += source.a;
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
                }

                return newColor;
            }
            catch (Exception)
            {
                return Color.black;
            }
        }

        /// <summary>
        /// Utility classes should not be instantiated.
        /// </summary>
        private AnimationUtils() { }

    }
}
