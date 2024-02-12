using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public class CanvasControl : Control
{
    public CanvasControl(string? name = null, SizeNumber? size = null) : base(name, size) 
    { }

    public Rune BackgroundRune { get; set; } = Pixel.Block.Solid;

    public override ILayoutManager? LayoutManager() => CanvasLayout.Default;
    public override bool ValidGuide(ILayoutGuide? guide)
    {
        return guide is null || 
               guide.GetType().IsAssignableTo(typeof(CanvasGuide));
    }

    public override void PaintIn(Canvas canvas, Rectangle rect)
    {
        canvas.FillRectangle(rect, BackgroundRune);
    }
}

public class CanvasLayout : ILayoutManager
{
    public static readonly CanvasLayout Default = new();
    internal static readonly CanvasGuide DefaultGuide = new ();

    public void MeasureIn(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
        if (container.MeasuredSize is null) throw new Exception($"Canvas has no measured size {container.Control}");        
        
        foreach (var child in children)
            MeasureChild(container, child);
    }

    public void MeasureOut(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
    }
    
    public void MeasureChild(LayoutNode container, LayoutNode child)
    {
        var guide = child.Guide as CanvasGuide ?? DefaultGuide;
        var sizeRequested = guide.Size;
        var availableSize = container.MeasuredSize!.Value;
        var remainingSize = LayoutTree.Reduce(availableSize, guide.Margin ?? SidesInt.Zero);
        
        SizeF measured;
        
        if (sizeRequested is not null)
        {
            var measuredWidth = sizeRequested.Value.Width.Resolve(remainingSize.Width);
            var measuredHeight = sizeRequested.Value.Height.Resolve(remainingSize.Height);
            measured = new(measuredWidth, measuredHeight);
        }
        else if (child.MeasuredSize is not null)
            measured = child.MeasuredSize.Value;
        else
            measured = remainingSize;

        var (x, width, _) = LayoutTree.ResolveDimension(remainingSize.Width, measured.Width, guide.XY.X, guide.AlignHorz);
        var (y, height, _) = LayoutTree.ResolveDimension(remainingSize.Height, measured.Height, guide.XY.Y, guide.AlignVert);

        child.MeasuredSize = new(width, height);
        child.Bounds = new((int)x, (int)y, (int)width, (int)height);
    }
}

/// <summary>
///     Guide for laying out a node inside a Canvas Node
/// </summary>
public class CanvasGuide : ILayoutGuide
{
    public CanvasGuide(
        Align alignHorz = Align.Start, 
        Align alignVert = Align.Start,
        SizeNumber? size = null,
        SidesInt? margin = null,
        XY? xy = null)
    {
        AlignHorz = alignHorz;
        AlignVert = alignVert;
        Size = size;
        Margin = margin;
        XY = xy ?? XY.Zero;
    }
        
    public Align AlignHorz { get; set; }
    public Align AlignVert { get; set; }
        
    // ReSharper disable once InconsistentNaming
    public XY XY { get; set; }

    public SizeNumber? Size { get; init; }
        
    public SidesInt? Margin { get; init; }
    public override string ToString() => BuildString(new StringBuilder()).ToString();

    protected virtual StringBuilder BuildString(StringBuilder? sb = null)
    {
        sb ??= new();
        sb.Append($"{GetType().FullName} (↔:{AlignHorz} ↕:{AlignVert}) XY:{XY}");
        if (Size is not null) sb.Append($" Size{Size.Value}");
        if (Margin is not null) sb.Append($" Margin{Margin}");
        return sb;
    }

}
