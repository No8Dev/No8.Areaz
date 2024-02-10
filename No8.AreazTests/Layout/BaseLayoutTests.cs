using System.Drawing;
using No8.Areaz.Layout;
using No8.Areaz.Painting;

namespace No8.AreazTests.Layout;

public class BaseLayoutTests
{
    protected Canvas Canvas { get; private set; } = null!;

    protected void Draw(LayoutNode layoutNode, Size? size = null)
    {
        var sz = size ?? layoutNode.Bounds.Size;
        if (sz.IsEmpty)
            sz = new(40, 12);
        else if (sz.Width <= 0)
            sz = sz with { Width = 40 };
        else if (sz.Height <= 0)
            sz = sz with { Height = 12 };

        Canvas = new (sz.Width, sz.Height);
            
        LayoutTree.Layout(layoutNode, sz);

        LayoutTree.Paint(Canvas, layoutNode);

        TestContext.WriteLine(layoutNode.BuildInstructionsString());
        TestContext.WriteLine(Canvas.ToString());
        TestContext.WriteLine();
        TestContext.WriteLine(layoutNode.ToString());
    }
}