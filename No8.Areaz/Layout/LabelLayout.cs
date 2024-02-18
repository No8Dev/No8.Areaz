using System.Drawing;
using No8.Areaz.Helpers;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public class Label : Control
{
    public override ILayoutManager? LayoutManager() => NoChildrenLayout.Default;
    public override bool ValidGuide(ILayoutGuide? guide) => true;   // Ignored

    public string Text { get; set; }
    public bool WrapLongLines { get; set; } = false;

    public Label(string? text = null)
    {
        Text = text ?? string.Empty;
    }
    public override void PaintIn(Canvas canvas, Rectangle rect)
    {
        if (rect.IsEmpty || rect.Width <= 0 || rect.Height <= 0)
            return;

        var lines = Text.WrapText(WrapLongLines ? rect.Width : Text.Length);
        var y = 0;
        foreach (var line in lines)
        {
            var one = (!WrapLongLines && line.Length > rect.Width) 
                ? line.Substring(0, rect.Width) 
                : line;
            canvas.DrawString(rect.X, rect.Y + y, one);
            y += 1;
            if (y > rect.Height)    // max number of lines
                return;
        }
    }
}
