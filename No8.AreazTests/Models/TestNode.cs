using System.Collections;
using System.Drawing;
using System.Text;
using No8.Areaz.Layout;
using No8.Areaz.Painting;

namespace No8.AreazTests.Models;

public class TestNode : INode
{
    public string Name { get; set; } = string.Empty;
    public Size Size { get; set; }
    
    public TestNode() { }
    public TestNode(string name) { Name = name; }

    public TestNode(out TestNode node) { node = this; }
    public TestNode(out TestNode node, string name) : this(name) { node = this; }

    public override string ToString() { return ToString(new ()); }

    public string ToString(StringBuilder sb, bool layout = false)
    {
        sb.Append($"{GetType().Name}");
        if (!string.IsNullOrEmpty(Name)) sb.Append($" Name[{Name}]");

        return sb.ToString();
    }

    public SizeF Measure(SizeF availableSize)
    {
        return Size.IsEmpty ? availableSize : Size;
    }

    private void DoDraw(Canvas canvas, Rectangle bounds, LineSet lineSet)
    {
        if (bounds.Size.IsEmpty) return;
        if (bounds.Height == 0 || bounds.Width == 0)
            canvas.DrawLine(bounds, lineSet);
        else 
            canvas.DrawRectangle(bounds, lineSet);
    }

    public void Paint(Canvas canvas, Rectangle rect)
    {
        canvas.FillRectangle(rect, Pixel.Block.LightShade);
        DoDraw(canvas, rect, LineSet.Single);
        if (Name.Length > 0)
            canvas.DrawString(rect.X + 1, rect.Y, $"[{Name}]");
    }
}