namespace No8.Areaz.Layout;

public interface ILayoutManager
{
    void MeasureIn(LayoutNode container, IReadOnlyList<LayoutNode> children);
    void MeasureOut(LayoutNode container, IReadOnlyList<LayoutNode> children);
}

/// <summary>
///     Layout guide for the child node of a container
/// </summary>
public interface ILayoutGuide
{
    /// <summary>
    ///     Optional size requested for the node
    /// </summary>
    SizeNumber? Size { get; init; }

    /// <summary>
    ///     Margin around node
    /// </summary>
    SidesInt? Margin { get; init; }
}
