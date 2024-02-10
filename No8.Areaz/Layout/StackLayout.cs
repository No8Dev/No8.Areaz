using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public enum Direction
{
    Unknown,
    Vertical,
    Horizontal
}

public class Stack : Node
{
    public override ILayoutManager LayoutManager() => StackLayout.Default;
    
    public Direction StackDirection { get; set; } = Direction.Vertical;
}

public class StackLayout : ILayoutManager
{
    public static readonly StackLayout Default = new();
    private static readonly Instructions DefaultInstructions = new ();
    
    public void MeasureIn(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
        var stack = container.Node as Stack ?? throw new Exception($"Unexpected container {container}");
        if (container.MeasuredSize is null) throw new Exception($"Stack has no measured size {stack}");

        // Measure all the fixed size ones first
        var availableSize = container.MeasuredSize.Value;
        var remainingSize = availableSize;
        var totalChildPercentages = 0f;
        
        foreach (var child in children)
        {
            child.FixedSize = false;
            var childInstructions = child.Instructions as Instructions ?? DefaultInstructions;
            var sizeRequested = childInstructions.SizeRequested;
            var childMargin = child.Instructions?.Margin ?? SidesInt.Zero;
           
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
                        availableSize.Height - childMargin.Top - childMargin.Bottom,
                        height, 0f, childInstructions.CrossAlign);
                    
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
                        remainingSize.Width - childMargin.Start - childMargin.End,
                        width, 0f, childInstructions.CrossAlign);
                    
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
            var childInstructions = child.Instructions as Instructions ?? DefaultInstructions;
            var sizeRequested = childInstructions.SizeRequested;
            var childMargin = child.Instructions?.Margin ?? SidesInt.Zero;
            
            if (stack.StackDirection == Direction.Horizontal)
            {
                var (y, _, _) = LayoutTree.ResolveDimension(
                    availableSize.Height - childMargin.Top - childMargin.Bottom,
                    child.MeasuredSize!.Value.Height, 0f, childInstructions.CrossAlign);

                if (sizeRequested is not null)
                {
                    var requestedWidth = sizeRequested.Value.Width;
                    if (requestedWidth.IsPercent && totalChildPercentages != 0f && remainingSize.Width > 0)
                    {
                        var actualWidth = (remainingSize.Width - childMargin.Start - childMargin.End) * (requestedWidth.Value / totalChildPercentages);
                        remainingSize = remainingSize with { Width = remainingSize.Width - actualWidth - childMargin.Start - childMargin.End };
                        var height = sizeRequested.Value.Height.IsPoints ? sizeRequested.Value.Height.Value : 0f;
                        
                        var (_, actualHeight, _) = LayoutTree.ResolveDimension(availableSize.Height - childMargin.Top - childMargin.Bottom, height, 0f, childInstructions.CrossAlign);

                        child.MeasuredSize = new SizeF(actualWidth, actualHeight);
                    }
                }

                child.Bounds = new(
                    xy.X, (int)y, 
                    (int)child.MeasuredSize!.Value.Width, 
                    (int)child.MeasuredSize!.Value.Height);

                xy = xy with { X = xy.X + child.Bounds.Width + childMargin.Start + childMargin.End };
            }
            // Vertical
            else
            {
                var (x, _, _) = LayoutTree.ResolveDimension(
                    availableSize.Width - childMargin.Start - childMargin.End, 
                    child.MeasuredSize!.Value.Width, 0f, childInstructions.CrossAlign);

                if (sizeRequested is not null)
                {
                    var requestedHeight = sizeRequested.Value.Height;
                    if (requestedHeight.IsPercent && totalChildPercentages != 0f && remainingSize.Height > 0)
                    {
                        var actualHeight = (remainingSize.Height - childMargin.Top - childMargin.Bottom) * (requestedHeight.Value / totalChildPercentages);
                        remainingSize = remainingSize with { Height = remainingSize.Height - actualHeight - childMargin.Top - childMargin.Bottom };
                        var width = sizeRequested.Value.Width.IsPoints ? sizeRequested.Value.Width.Value : 0f;
                        
                        var (_, actualWidth, _) = LayoutTree.ResolveDimension(
                            availableSize.Width - childMargin.Start - childMargin.End, 
                            width, 0f, childInstructions.CrossAlign);

                        child.MeasuredSize = new SizeF(actualWidth, actualHeight);
                    }
                }

                child.Bounds = new(
                    (int)x, xy.Y, 
                    (int)child.MeasuredSize!.Value.Width, 
                    (int)child.MeasuredSize!.Value.Height);

                xy = xy with { Y = xy.Y + child.Bounds.Height + childMargin.Top + childMargin.Bottom };
            }
        }
        
    }

    public void MeasureOut(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
    }

    /// <summary>
    ///     Properties for Child node
    /// </summary>
    public class Instructions : ILayoutManager.ILayoutInstructions
    {
        public Instructions(
            Align crossAlign = Align.Stretch,
            SizeNumber? sizeRequested = null,
            SidesInt? margin = null
            )
        {
            CrossAlign = crossAlign;
            SizeRequested = sizeRequested;
            Margin = margin;
        }

        public Align CrossAlign { get; init; }
        
        public SizeNumber? SizeRequested { get; init; }
        
        public SidesInt? Margin { get; init; }

        public override string ToString() => BuildString(new StringBuilder()).ToString();

        protected virtual StringBuilder BuildString(StringBuilder? sb = null)
        {
            sb ??= new();
            sb.Append($"{GetType().FullName} Cross:{CrossAlign}");
            if (SizeRequested is not null) sb.Append($" Size{SizeRequested.Value}");
            if (Margin is not null) sb.Append($" Margin{Margin}");
            return sb;
        }
    }
}