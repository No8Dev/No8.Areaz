using System.Drawing;

namespace No8.Areaz.Layout;

public class Stack : Control
{
    public override ILayoutManager LayoutManager() => StackLayout.Default;
    public override bool ValidGuide(ILayoutGuide? guide)
    {
        return guide is null || 
               guide.GetType().IsAssignableTo(typeof(StackGuide));
    }

    public Direction StackDirection { get; set; } = Direction.Vertical;
}

public class StackLayout : ILayoutManager
{
    public static readonly StackLayout Default = new();
    private static readonly StackGuide DefaultGuide = new ();
    
    public void MeasureIn(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
        var stack = container.Control as Stack ?? throw new Exception($"Unexpected container {container}");
        if (container.MeasuredSize is null) throw new Exception($"Stack has no measured size {stack}");

        // Measure all the fixed size ones first
        var availableSize = container.MeasuredSize.Value;
        var remainingSize = availableSize;
        var totalChildPercentages = 0f;
        
        foreach (var child in children)
        {
            child.FixedSize = false;
            var childGuide = child.Guide as StackGuide ?? DefaultGuide;
            var sizeRequested = childGuide.Size;
            var childMargin = child.Guide?.Margin ?? SidesInt.Zero;
           
            if (sizeRequested is not null)
            {
                float width = 0f, height = 0f;

                if (stack.StackDirection == Direction.Horizontal)
                {
                    var requestedWidth = sizeRequested.Value.Width; 
                    if (requestedWidth.IsPercent)
                    {
                        totalChildPercentages += requestedWidth.Value;
                        continue;
                    }

                    child.FixedSize = true;
                    width = requestedWidth.Value;
                    height = sizeRequested.Value.Height.IsPoints ? sizeRequested.Value.Height.Value : 0f; 
                    
                    var (_, actualHeight, _) = LayoutTree.ResolveDimension(
                        availableSize.Height - childMargin.North - childMargin.South,
                        height, 0f, childGuide.CrossAlign);
                    
                    remainingSize = remainingSize with { Width = remainingSize.Width - width };
                    child.MeasuredSize = new(width, actualHeight);
                }
                else
                {
                    var requestedHeight = sizeRequested.Value.Height;
                    if (requestedHeight.IsPercent)
                    {
                        totalChildPercentages += requestedHeight.Value;
                        continue;
                    }

                    width = sizeRequested.Value.Width.IsPoints ? sizeRequested.Value.Width : 0f;
                    height = requestedHeight.Value;
                    
                    var (_, actualWidth, _) = LayoutTree.ResolveDimension(
                        remainingSize.Width - childMargin.West - childMargin.East,
                        width, 0f, childGuide.CrossAlign);
                    
                    remainingSize = remainingSize with { Height = remainingSize.Height - height };
                    child.MeasuredSize = new(actualWidth, height);
                }
            }
            else
            {
                child.MeasuredSize = SizeF.Empty;
            }
        }

        // All children have been measured, so now lets lay them out in the stack
        var xy = XY.Zero;
        foreach (var child in children)
        {
            var childGuide = child.Guide as StackGuide ?? DefaultGuide;
            var sizeRequested = childGuide.Size;
            var childMargin = child.Guide?.Margin ?? SidesInt.Zero;
            
            if (stack.StackDirection == Direction.Horizontal)
            {
                var (y, _, _) = LayoutTree.ResolveDimension(
                    availableSize.Height - childMargin.North - childMargin.South,
                    child.MeasuredSize!.Value.Height, 0f, childGuide.CrossAlign);

                if (sizeRequested is not null)
                {
                    var requestedWidth = sizeRequested.Value.Width;
                    if (requestedWidth.IsPercent && totalChildPercentages != 0f && remainingSize.Width > 0)
                    {
                        var actualWidth = (remainingSize.Width - childMargin.West - childMargin.East) * (requestedWidth.Value / totalChildPercentages);
                        remainingSize = remainingSize with { Width = remainingSize.Width - actualWidth - childMargin.West - childMargin.East };
                        var height = sizeRequested.Value.Height.IsPoints ? sizeRequested.Value.Height.Value : 0f;
                        
                        var (_, actualHeight, _) = LayoutTree.ResolveDimension(availableSize.Height - childMargin.North - childMargin.South, height, 0f, childGuide.CrossAlign);

                        child.MeasuredSize = new SizeF(actualWidth, actualHeight);
                    }
                }

                child.Bounds = new(
                    xy.X, (int)y, 
                    (int)child.MeasuredSize!.Value.Width, 
                    (int)child.MeasuredSize!.Value.Height);

                xy = xy with { X = xy.X + child.Bounds.Width + childMargin.West + childMargin.East };
            }
            // Vertical
            else
            {
                var (x, _, _) = LayoutTree.ResolveDimension(
                    availableSize.Width - childMargin.West - childMargin.East, 
                    child.MeasuredSize!.Value.Width, 0f, childGuide.CrossAlign);

                if (sizeRequested is not null)
                {
                    var requestedHeight = sizeRequested.Value.Height;
                    if (requestedHeight.IsPercent && totalChildPercentages != 0f && remainingSize.Height > 0)
                    {
                        var actualHeight = (remainingSize.Height - childMargin.North - childMargin.South) * (requestedHeight.Value / totalChildPercentages);
                        remainingSize = remainingSize with { Height = remainingSize.Height - actualHeight - childMargin.North - childMargin.South };
                        var width = sizeRequested.Value.Width.IsPoints ? sizeRequested.Value.Width.Value : 0f;
                        
                        var (_, actualWidth, _) = LayoutTree.ResolveDimension(
                            availableSize.Width - childMargin.West - childMargin.East, 
                            width, 0f, childGuide.CrossAlign);

                        child.MeasuredSize = new SizeF(actualWidth, actualHeight);
                    }
                }

                child.Bounds = new(
                    (int)x, xy.Y, 
                    (int)child.MeasuredSize!.Value.Width, 
                    (int)child.MeasuredSize!.Value.Height);

                xy = xy with { Y = xy.Y + child.Bounds.Height + childMargin.North + childMargin.South };
            }
        }
        
    }

    public void MeasureOut(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
    }
}

/// <summary>
///     Properties for Child node
/// </summary>
public class StackGuide : ILayoutGuide
{
    public StackGuide(
        Align crossAlign = Align.Stretch,
        SizeNumber? size = null,
        SidesInt? margin = null
    )
    {
        CrossAlign = crossAlign;
        Size = size;
        Margin = margin;
    }

    public Align CrossAlign { get; init; }
        
    /// <summary>
    ///     Requested Size
    /// </summary>
    public SizeNumber? Size { get; init; }
        
    public SidesInt? Margin { get; init; }

    public override string ToString() => BuildString(new StringBuilder()).ToString();

    protected virtual StringBuilder BuildString(StringBuilder? sb = null)
    {
        sb ??= new();
        sb.Append($"{GetType().FullName} Cross:{CrossAlign}");
        if (Size is not null) sb.Append($" Size{Size.Value}");
        if (Margin is not null) sb.Append($" Margin{Margin}");
        return sb;
    }
}