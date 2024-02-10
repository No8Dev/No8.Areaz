using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public abstract class Node : INode
{
    public string Name { get; set; }

    protected Node(string? name = null, SizeNumber? sizeRequested = null)
    {
        Name = name ?? string.Empty;
        SizeRequested = sizeRequested;
    }

    public abstract ILayoutManager? LayoutManager();
    public SizeNumber? SizeRequested { get; set; }

    public virtual void PaintIn(Canvas canvas, Rectangle rect) { }
    public virtual void PaintOut(Canvas canvas, Rectangle rect) { }

    public override string ToString() => BuildString(new StringBuilder()).ToString();

    protected virtual StringBuilder BuildString(StringBuilder? sb = null)
    {
        sb ??= new();
        sb.Append($"{GetType().Name}");
        if (!string.IsNullOrEmpty(Name)) sb.Append($" [{Name}] ");
        if (SizeRequested is not null) sb.Append($" Size{SizeRequested.Value}");
        return sb;
    }
}