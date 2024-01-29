using System.Drawing;

namespace No8.Areaz.Layout;

public interface INode : IEnumerable<INode>
{
    PlannedLayout Plan { get; }
    PlacementLayout Placement { get; }
    
    IReadOnlyList<INode> Children { get; }

    MeasureFunc? MeasureNode { get; set; }
}

public delegate SizeF MeasureFunc(
    INode       node,
    float       width,
    MeasureMode widthMode,
    float       height,
    MeasureMode heightMode);
