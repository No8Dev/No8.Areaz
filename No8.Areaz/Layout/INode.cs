using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public interface INode
{
    ILayoutManager LayoutManager() => CanvasLayout.Default;

    /// <summary>
    ///     Measure width and height of node based on available size of parent node 
    /// </summary>
    SizeF Measure(SizeF availableSize);
    
    void Paint(Canvas canvas, Rectangle rect);
}
