using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public interface INode
{
    ILayoutManager LayoutManager { get; }

    /// <summary>
    ///     Measure width and height of node based on available size of parent node 
    /// </summary>
    SizeF Measure(SizeF availableSize);
    
    void Paint(Canvas canvas, Rectangle rect);
}

public abstract class Node : INode
{
    public string Name { get; set; }

    protected Node(string? name = null) { Name = name ?? string.Empty; }

    public abstract ILayoutManager LayoutManager { get; }
    public abstract SizeF Measure(SizeF availableSize);
    public abstract void Paint(Canvas canvas, Rectangle rect);

    public override string ToString() => ToString(new ()).ToString();

    public virtual StringBuilder ToString(StringBuilder sb, bool layout = false)
    {
        sb.Append($"{GetType().Name}");
        if (!string.IsNullOrEmpty(Name)) sb.Append($" [{Name}] ");
        return sb;
    }
}