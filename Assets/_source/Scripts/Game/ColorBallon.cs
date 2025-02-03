using UnityEngine;

public class ColorBallon : Singleton<ColorBallon>
{
    public Color[] colors;

    public Color GetRandomColor()
    {
        return colors.GetRandomElement();
    }
}
