using KSP_Chroma_Control.ColorSchemes;

namespace KSP_Chroma_Control
{
    public interface KeyboardAnimation
    {
        ColorScheme getFrame();
        bool isFinished();
    }
}