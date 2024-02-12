using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public interface IControl
{
    /// <summary>
    ///     Node identity
    /// </summary>
    string Name { get; set; }

    /// <summary>
    ///     Layout manager to layout child elements
    ///     Can be null if not child elements
    /// </summary>
    ILayoutManager? LayoutManager();

    bool ValidGuide(ILayoutGuide? guide);

    // /// <summary>
    // ///     Optional size requested for the node
    // /// </summary>
    // SizeNumber? SizeRequested { get; set; }
    
    /// <summary>
    ///     Node can paint to the canvas before any child nodes 
    /// </summary>
    void PaintIn(Canvas canvas, Rectangle rect);
    
    /// <summary>
    ///     Node can paint to the canvas after all child nodes have painted. 
    /// </summary>
    void PaintOut(Canvas canvas, Rectangle rect);
}