using KSP_Chroma_Control.ColorSchemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSP_Chroma_Control
{
    class AnimationManager
    {
        private static AnimationManager instance = null;

        public static AnimationManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new AnimationManager();
                }
                return instance;
            }
        }

        private KeyboardAnimation activeAnimation = null;

        private AnimationManager()
        {
        }

        public void setAnimation(KeyboardAnimation animation)
        {
            this.activeAnimation = animation;
        }

        public ColorScheme getFrame()
        {
            return (animationRunning()) ? activeAnimation.getFrame() : new ColorScheme();
        }

        public Boolean animationRunning()
        {
            return activeAnimation != null && !activeAnimation.isFinished();
        }
    }
}
