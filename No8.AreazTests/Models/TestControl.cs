using System.Drawing;
using System.Text;
using No8.Areaz;
using No8.Areaz.Layout;
using No8.Areaz.Painting;

namespace No8.AreazTests.Models;

public class TestControl : IControl
{
    public string Name { get; set; } = string.Empty;
    //public SizeNumber? SizeRequested { get; set; }
    
    public TestControl() { }
    public TestControl(string name) { Name = name; }

    public string ToString(StringBuilder? sb = null)
    {
        sb ??= new ();
        sb.Append($"{GetType().Name}");
        if (!string.IsNullOrEmpty(Name)) sb.Append($" Name[{Name}]");

        return sb.ToString();
    }

    private readonly ILayoutManager _layoutManager = CanvasLayout.Default;
    public ILayoutManager? LayoutManager() => _layoutManager;
    public bool ValidGuide(ILayoutGuide? guide)
    {
        return guide is null || 
               guide.GetType().IsAssignableTo(typeof(CanvasGuide));
    }

    private void PaintBorder(Canvas canvas, Rectangle bounds, LineSet lineSet)
    {
        if (bounds.Size.IsEmpty) return;
        if (bounds.Height <= 1 || bounds.Width <= 1)
        {
            canvas.DrawLine(bounds, lineSet);
            if (bounds.Height <= 1)
            {
                canvas[bounds.Y, bounds.X] = '├';
                canvas[bounds.Y, bounds.Right - 1] = '┤';
            }
            else if (bounds.Width <= 1)
            {
                canvas[bounds.Y, bounds.X] = '┬';
                canvas[bounds.Bottom - 1, bounds.X] = '┴';
            }
        }
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
