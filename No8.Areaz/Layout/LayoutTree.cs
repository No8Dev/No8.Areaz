using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public static class LayoutTree
{
    private static readonly CanvasGuide DefaultRootLayout = CanvasLayout.DefaultGuide;
    
    public static void Layout(LayoutNode root, Size availableSize)
    {
        var guide = root.Guide ?? DefaultRootLayout;
        var sizeRequested = guide.Size;
        var remainingSize = LayoutTree.Reduce(availableSize, guide.Margin ?? SidesInt.Zero);

        if (sizeRequested?.Width is not null &&
            sizeRequested?.Height is not null)
        {
            var measuredWidth = sizeRequested.Value.Width.Resolve(remainingSize.Width);
            var measuredHeight = sizeRequested.Value.Height.Resolve(remainingSize.Height);
            var alignHorz = Align.Start;
            var alignVert = Align.Start;
            if (guide is CanvasGuide canvasGuide)
            {
                alignHorz = canvasGuide.AlignHorz;
                alignVert = canvasGuide.AlignVert;
            }

            var (x, width, _) = ResolveDimension(remainingSize.Width, measuredWidth, 0, alignHorz);
            var (y, height, _) = ResolveDimension(remainingSize.Height, measuredHeight, 0, alignVert);
            
            root.MeasuredSize = new (width, height);
            root.Bounds = new((int)x, (int)y, (int)width, (int)height);
        }
        else
        {
            root.MeasuredSize = remainingSize;
            root.Bounds = new(Point.Empty, remainingSize);
        }

        LayoutInternal(root);
    }
    
    internal static void LayoutInternal(LayoutNode layoutNode)
    {
        // Option to measure all the direct children from a top down perspective
        layoutNode.Control.LayoutManager()?.MeasureIn(layoutNode, layoutNode.Children);

        // Apply same logic to all child nodes
        foreach (var child in layoutNode)
            LayoutInternal(child);

        // When all child nodes (and childrens children...) are measured, 
        // then option to measure all children from the inside out
        layoutNode.Control.LayoutManager()?.MeasureOut(layoutNode, layoutNode.Children);
        if (layoutNode.MeasuredSize is null)
            throw new Exception($"node was not measured by container:{layoutNode.Control}");
    }

    public static void Paint(Canvas canvas, LayoutNode container, Rectangle? containerBounds = null)
    {
        var margin = container.Guide?.Margin ?? SidesInt.Zero;
        containerBounds ??= container.Bounds;
        if (margin.HasValue)
            containerBounds = containerBounds.Value with
            {
                X = containerBounds.Value.X + margin.West,
                Y = containerBounds.Value.Y + margin.North
            };
            
        container.Control.PaintIn(canvas, containerBounds.Value);

        foreach (var child in container.Children)
        {
            var childBounds = new Rectangle(
                containerBounds.Value.X + child.Bounds.X,
                containerBounds.Value.Y + child.Bounds.Y,
                child.Bounds.Width, child.Bounds.Height);
            Paint(canvas, child, childBounds);
        }
            
        container.Control.PaintOut(canvas, containerBounds.Value);
    }

    // ReSharper disable once UnusedTupleComponentInReturnValue
    /// <summary>
    ///     Calculate where this node is to be painted on the canvas        
    /// </summary>
    public static (float start, float size, bool overflow) 
        ResolveDimension(float available, float measured, float offset, Align align)
    {
        available -= offset;
        bool overflow = measured > available;

        if (available < measured || available == 0)
            return (offset, measured, overflow: true);
        
        switch (align)
        {
            case Align.Start:
                return (offset, measured, overflow);
            case Align.End:
                if (overflow)
                    return (offset, available, overflow);
                return (available - measured, measured, overflow);
            case Align.Center:
                if (measured > available)
                    return (offset, available, overflow);
                var remaining = available - measured;
                return (offset + MathF.Floor(remaining / 2f), measured, overflow);
            case Align.Stretch:
                return (offset, available, overflow);
                
            default:
                throw new ArgumentOutOfRangeException(nameof(align), "Invalid alignment");
        }
    }
        
    public static Size Reduce(SizeF availableSize, SidesInt sides)
    {
        return new (
            (int)availableSize.Width - sides.West - sides.East,
            (int)availableSize.Height - sides.North - sides.South
        );
    }
        
    public static Rectangle Reduce(Rectangle rect, SidesInt sides)
    {
        return new Rectangle(
            rect.X + sides.West,
            rect.Y + sides.North,
            rect.Width - sides.West - sides.East,
            rect.Height - sides.North - sides.South
        );
    }
}