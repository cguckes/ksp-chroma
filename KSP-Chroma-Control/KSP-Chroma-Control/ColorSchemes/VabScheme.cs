using KSP_Chroma_Control.ColorSchemes;
using UnityEngine;

namespace KSP_Chroma_Control.SceneManagers
{
    class VabScheme : ColorScheme
    {
        public VabScheme()
        {
            SetKeysToColor(new string[] {
                "w", "a", "s", "d", "q", "e",
            }, new Color(1f, 1f, 0f));
            SetKeysToColor(new string[] {
                "leftshift", "space", "f"
            }, Color.magenta);
        }
    }
}