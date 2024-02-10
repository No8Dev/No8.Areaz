using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public class CanvasNode : Node
{
    public CanvasNode(string? name = null, SizeNumber? size = null) : base(name, size) 
    { }

    public Rune BackgroundRune { get; set; } = Pixel.Block.Solid;

    public override ILayoutManager? LayoutManager() => CanvasLayout.Default;

    public override void PaintIn(Canvas canvas, Rectangle rect)
    {
        canvas.FillRectangle(rect, BackgroundRune);
    }
}

public class CanvasLayout : ILayoutManager
{
    public static readonly CanvasLayout Default = new();
    internal static readonly Instructions DefaultInstructions = new ();

    public void MeasureIn(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
        if (container.MeasuredSize is null) throw new Exception($"Canvas has no measured size {container.Node}");        
        
        foreach (var child in children)
            MeasureChild(container, child);
    }

    public void MeasureOut(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
    }
    
    public void MeasureChild(LayoutNode container, LayoutNode child)
    {
        var instructions = child.Instructions as Instructions ?? DefaultInstructions;
        var sizeRequested = instructions.SizeRequested;
        var availableSize = container.MeasuredSize!.Value;
        var remainingSize = LayoutTree.Reduce(availableSize, instructions.Margin ?? SidesInt.Zero);
        
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

        var (x, width, _) = LayoutTree.ResolveDimension(remainingSize.Width, measured.Width, instructions.XY.X, instructions.AlignHorz);
        var (y, height, _) = LayoutTree.ResolveDimension(remainingSize.Height, measured.Height, instructions.XY.Y, instructions.AlignVert);

        child.MeasuredSize = new(width, height);
        child.Bounds = new((int)x, (int)y, (int)width, (int)height);
    }

    /// <summary>
    ///     Instructions for laying out a node inside a Canvas Node
    /// </summary>
    public class Instructions : ILayoutManager.ILayoutInstructions
    {
        public Instructions(
            Align alignHorz = Align.Start, 
            Align alignVert = Align.Start,
            SizeNumber? sizeRequested = null,
            SidesInt? margin = null,
            XY? xy = null)
        {
            AlignHorz = alignHorz;
            AlignVert = alignVert;
            SizeRequested = sizeRequested;
            Margin = margin;
            XY = xy ?? XY.Zero;
        }
        
        public Align AlignHorz { get; set; }
        public Align AlignVert { get; set; }
        
        // ReSharper disable once InconsistentNaming
        public XY XY { get; set; }

        public SizeNumber? SizeRequested { get; init; }
        
        public SidesInt? Margin { get; init; }
        public override string ToString() => BuildString(new StringBuilder()).ToString();

        protected virtual StringBuilder BuildString(StringBuilder? sb = null)
        {
            sb ??= new();
            sb.Append($"{GetType().FullName} (↔:{AlignHorz} ↕:{AlignVert}) XY:{XY}");
            if (SizeRequested is not null) sb.Append($" Size{SizeRequested.Value}");
            if (Margin is not null) sb.Append($" Margin{Margin}");
            return sb;
        }

    }
}
