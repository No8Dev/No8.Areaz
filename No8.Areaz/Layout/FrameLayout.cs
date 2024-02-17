using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public class Frame : Control
{
    public Frame(string? name = null, SizeNumber? size = null) : base(name, size) 
    { }

    public Rune BackgroundRune { get; set; } = Pixel.Block.ShadeLight;
    public LineSet Border { get; set; } = LineSet.None;

    public override ILayoutManager? LayoutManager() => FrameLayout.Default;
    public override bool ValidGuide(ILayoutGuide? guide)
    {
        return guide is null || 
               guide.GetType().IsAssignableTo(typeof(FrameGuide));
    }

    public override void PaintIn(Canvas canvas, Rectangle rect)
    {
        if (Border != LineSet.None)
        {
            var fillRect = rect;
            fillRect.Inflate(-1, -1);
            canvas.FillRectangle(fillRect, BackgroundRune);
            canvas.DrawRectangle(rect, Border);
        }
        else
            canvas.FillRectangle(rect, BackgroundRune);
    }
}

public class FrameLayout  : ILayoutManager
{
    public static readonly FrameLayout Default = new();
    internal static readonly FrameGuide DefaultGuide = new ();

    public void MeasureIn(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
        if (container.MeasuredSize is null) 
            throw new Exception($"Canvas has no measured size {container.Control}");        
        
        foreach (var child in children)
            MeasureChild(container, child);
    }

    public void MeasureOut(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
    }
    
    public void MeasureChild(LayoutNode container, LayoutNode child)
    {
        var guide = child.Guide as FrameGuide ?? DefaultGuide;
        
        var sizeRequested = guide.Size;
        var availableSize = container.MeasuredSize!.Value;
        
        SizeF measured;
        
        if (sizeRequested is not null)
        {
            var measuredWidth = sizeRequested.Value.Width.Resolve(availableSize.Width);
            var measuredHeight = sizeRequested.Value.Height.Resolve(availableSize.Height);
            measured = new(measuredWidth, measuredHeight);
        }
        else if (child.MeasuredSize is not null)
            measured = child.MeasuredSize.Value;
        else
            measured = availableSize;

        var (x, width, _) = LayoutTree.ResolveDimension(availableSize.Width, measured.Width, guide.XY.X, guide.AlignHorz);
        var (y, height, _) = LayoutTree.ResolveDimension(availableSize.Height, measured.Height, guide.XY.Y, guide.AlignVert);

        if (guide.Margin is not null)
        {
            x += guide.Margin.West;
            y += guide.Margin.North;
            width = width - guide.Margin.West - guide.Margin.East;
            height = height - guide.Margin.North - guide.Margin.South;
        }
            
        child.MeasuredSize = new ((int)width, (int)height);
        child.Bounds = new(
            new (container.Bounds.X + (int)x, container.Bounds.Y + (int)y), 
            child.MeasuredSize.Value);
    }
}

public class FrameGuide : ILayoutGuide
{
    public FrameGuide(
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
