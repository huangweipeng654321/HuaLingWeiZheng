using UnityEngine;

public class MacroTweenEnum : MonoBehaviour
{
    public enum VSP_TweenProperty
    {
        Position,
        Scale,
        Angle,
        ValueOnly,
        Opacity
    };

    public enum VSP_TweenPropertyFirst
    {
        Position,
        Scale,
        Angle,
    };
    public enum VSP_TweenPropertySecond
    {
        ValueOnly,
        Opacity
    };

    public enum VSP_TweenLoop
    {
        No,
        Begin,
        End
    };

    public enum VSP_TweenAnimationType
    { 
        Linear,
        SmoothOut,
        SmoothIn,
        SmoothInOut,
        BounceOut,
        BounceIn,
        BounceInOut,
        ElasticIn,
        ElasticOut,
        ElasticInOut,
        BackIn,
        BackOut,
        BackInOut,
        CircIn,
        CircOut,
        CircInOut,
        QuadIn,
        QuadOut,
        QuadInOut,
        CubicIn,
        CubicOut,
        CubicInOut,
        QuartIn,
        QuartOut,
        QuartInOut,
        QuintIn,
        QuintOut,
        QuintInOut,
        ExpoIn,
        ExpoOut,
        ExpoInOut

    };
}
