using System.Drawing;

namespace No8.Areaz.Layout;

/// <summary>
///     
/// </summary>
public class PlacementLayout
{
    public Number? ResolvedWidth { get; set; }
    public Number? ResolvedHeight { get; set; }
    public float MeasuredWidth { get; set; }
    public float MeasuredHeight { get; set; }
    
    public SidesNumeric? Position { get; set; }
    public SidesAtomic? Border { get; set; }
    public SidesNumeric? Padding { get; set; }
    
    public bool HadOverflow { get; set; }


    public Rectangle Bounds =>
        new(
            (int)Position!.Start.Value,
            (int)Position!.Top.Value,
            (int)MeasuredWidth,
            (int)MeasuredHeight);

    public Rectangle ContentBounds =>
        new(
            (int)Position!.Start.Value + Border!.Start + (int)Padding!.Start.Value,
            (int)Position!.Top.Value + Border!.Top + (int)Padding!.Top.Value,
            (int)MeasuredWidth - Border!.End - (int)Padding!.End.Value,
            (int)MeasuredHeight - Border!.Bottom - (int)Padding!.Bottom.Value);
}