using System.Drawing;
using System.Text;
using No8.Areaz;
using No8.Areaz.Layout;
using No8.Areaz.Painting;

namespace No8.AreazTests.Models;

public class TestNode : INode
{
    public string Name { get; set; } = string.Empty;
    public SizeNumber? SizeRequested { get; set; }
    
    public TestNode() { }
    public TestNode(string name) { Name = name; }

    public string ToString(StringBuilder? sb = null)
    {
        sb ??= new ();
        sb.Append($"{GetType().Name}");
        if (!string.IsNullOrEmpty(Name)) sb.Append($" Name[{Name}]");

        return sb.ToString();
    }

    private readonly ILayoutManager _layoutManager = CanvasLayout.Default;
    public ILayoutManager? LayoutManager() => _layoutManager;

    private void PaintBorder(Canvas canvas, Rectangle bounds, LineSet lineSet)
    {
        if (bounds.Size.IsEmpty) return;
        if (bounds.Height <= 1 || bounds.Width <= 1)
            canvas.DrawLine(bounds, lineSet);
        else 
            canvas.DrawRectangle(bounds, lineSet);
    }

    public void PaintIn(Canvas canvas, Rectangle rect)
    {
        canvas.FillRectangle(rect, Pixel.Block.ShadeLight);
        
        PaintBorder(canvas, rect, LineSet.Single);
        
        if (Name.Length > 0)
            canvas.DrawString(rect.X + 1, rect.Y, $"[{Name}]");
    }

    public void PaintOut(Canvas canvas, Rectangle rect) { }
}

