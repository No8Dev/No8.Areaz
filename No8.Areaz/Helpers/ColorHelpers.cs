using System.Drawing;

namespace No8.Areaz.Helpers;

public static class ColorHelpers
{
    /// <summary>
    /// Adjust brightness of color by factor.
    /// Factor must be between -1.0 and 1.0
    /// </summary>
    public static Color AdjustBy(this Color color, float factor)
    {
        var red = (float)color.R;
        var green = (float)color.G;
        var blue = (float)color.B;

        if (factor < 0)
        {
            factor = 1 + factor;
            red *= factor;
            green *= factor;
            blue *= factor;
        }
        else
        {
            red = (255 - red) * factor + red;
            green = (255 - green) * factor + green;
            blue = (255 - blue) * factor + blue;
        }

        return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
    }

    public static Color BlendRGB(this Color c1, Color other, double t)
    {
        return Color.FromArgb(
            (int)(c1.R + t * (other.R - c1.R)),
            (int)(c1.G + t * (other.G - c1.G)),
            (int)(c1.B + t * (other.B - c1.B))
        );
    }

    public static Color BlendLAB(this Color c1, Color c2, double t)
    {
        var (l1, a1, b1) = ((Colorful)c1).AsLAB();
        var (l2, a2, b2) = ((Colorful)c2).AsLAB();
        
        return Colorful.CreateLAB(
            l1 + t * (l2 - l1),
            a1 + t * (a2 - a1),
            b1 + t * (b2 - b1));
    }

    public static Color BlendHSV(this Color c1, Color c2, double t)
    {
        var (h1, s1, v1) = ((Colorful)c1).AsHSV();
        var (h2, s2, v2) = ((Colorful)c2).AsHSV();

        // We know that h are both in [0-360]
        return Colorful.CreateHSV(
            Colorful.InterpolateAngle(h1, h2, t), 
            s1 + t * (s2 - s1), 
            v1 + t * (v2 - v1));
    }

    public static Color BlendHCL(this Color col1, Color col2, double t)
    {
        var (h1, c1, l1) = ((Colorful)col1).AsHCL();
        var (h2, c2, l2) = ((Colorful)col2).AsHCL();

        // We know that h are both in [0..360]
        return Colorful.CreateHCL(
            Colorful.InterpolateAngle(h1, h2, t), 
            c1 + t * (c2 - c1), 
            l1 + t * (l2 - l1))
            .Clamped();
    }

    public static Color BlendLUV(this Color c1, Color c2, double t)
    {
        var (l1, u1, v1) = ((Colorful)c1).AsLUV();
        var (l2, u2, v2) = ((Colorful)c2).AsLUV();
        return Colorful.CreateLUV(
            l1 + t * (l2 - l1),
            u1 + t * (u2 - u1),
            v1 + t * (v2 - v1));
    }

}