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
    
    public Rectangle Bounds
    {
        get
        {
            var x = (int)(Position?.Start.Value ?? 0);
            var y = (int)(Position?.Top.Value ?? 0);
            var width = (int)MeasuredWidth;
            var height = (int)MeasuredHeight;
            return new(x, y, width, height);
        }
    }

    public Rectangle ContentBounds
    {
        get
        {
            var x = (int)(Position?.Start.Value ?? 0) + (Border?.Start ?? 0) + (int)(Padding?.Start.Value ?? 0);
            var y = (int)(Position?.Top.Value ?? 0) + (Border?.Top ?? 0) + (int)(Padding?.Top.Value ?? 0);
            var width = (int)MeasuredWidth - (Border?.End ?? 0) - (int)(Padding?.End.Value ?? 0);
            var height = (int)MeasuredHeight - (Border?.Bottom ?? 0) - (int)(Padding?.Bottom.Value ?? 0); 
            return new (x, y, width, height);
        }
    }
    
    
    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append($" {nameof(Bounds)}={Bounds}");
        sb.Append($" {nameof(ContentBounds)}={ContentBounds}");
        
        if (Position is not null) sb.Append($" {nameof(Position)}={Position}");
        if (Border is not null) sb.Append($" {nameof(Border)}={Border}");
        if (Padding is not null) sb.Append($" {nameof(Padding)}={Padding}");
        if (HadOverflow) sb.Append(" HadOverflow");

        return sb.ToString();
    }
}