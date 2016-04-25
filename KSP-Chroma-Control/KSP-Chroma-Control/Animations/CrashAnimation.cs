using System;
using KSP_Chroma_Control.ColorSchemes;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using KSP_Chroma_Control.Animations;

namespace KSP_Chroma_Control
{
    /// <summary>
    /// Displays a warning on the keyboard, indicating that the vessel is currently out of power and cannot
    /// be controlled. Consists of two frames alternating at 1fps.
    /// </summary>
    internal class CrashAnimation : KeyboardAnimation
    {
        /// <summary>
        /// Static constructor adds lightning bolts in different colors to both frames
        /// </summary>
        static CrashAnimation()
        {
            List<ColorScheme> newFrames = new List<ColorScheme>();

            ColorScheme[] uninterpolated = generateAnimationFrames();
            
            for(int i = 0; i < uninterpolated.Length - 1; i++)
            {
                newFrames.AddRange(AnimationUtils.InterpolateFrames(uninterpolated[i], uninterpolated[i + 1], 2));
            }
            newFrames.AddRange(AnimationUtils.InterpolateFrames(uninterpolated[uninterpolated.Length - 1], new ColorScheme(Color.black), 10));

            frames = newFrames.ToArray();
        }

        private static ColorScheme[] generateAnimationFrames()
        {
            ColorScheme red = new ColorScheme(Color.red);
            ColorScheme yellow = new ColorScheme(Color.yellow);

            ColorScheme[] myReturn = new ColorScheme[20];
            
            for(int i = 0; i < 10; i++)
                myReturn[i] = (i % 2 == 0) ? red : yellow;

            for(int i = 10; i < myReturn.Length; i++)
            //for (int i = 0; i < myReturn.Length; i++)
            {
                    myReturn[i] = AnimationUtils.CircularSineWave(Color.red, Color.yellow, i);
            }

           return myReturn;
        }

        public CrashAnimation() : base(30)
        {
        }
    }
}