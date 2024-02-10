using System.Drawing;

namespace No8.Areaz.Layout;

public class GridLayout : ILayoutManager
{
    public readonly List<GridTemplateTrack> TemplateRows = new();
    public readonly List<GridTemplateTrack> TemplateCols = new();
    
    private int _cellRowGap;
    private int _cellColGap;

    public int CellRowGap
    {
        get => _cellRowGap;
        set => _cellRowGap = value == 0 ? 0 : 1;
    }

    public int CellColGap
    {
        get => _cellColGap;
        set => _cellColGap = value == 0 ? 0 : 1;
    }

    public void MeasureIn(TreeNode container, IReadOnlyList<TreeNode> children)
    {
        throw new NotImplementedException();
    }

    public void MeasureOut(TreeNode container, IReadOnlyList<TreeNode> children)
    {
        throw new NotImplementedException();
    }
}

public class GridArea
{
    public string? Name { get; init; }
    public int RowIndex { get; set; }
    public int ColIndex { get; set; }
    public int RowSpan { get; set; } = 1;
    public int ColSpan { get; set; } = 1;

    public GridArea() { }
}

public class GridCell
{
    public string? Name { get; init; }
    public int RowIndex { get; set; }
    public int ColIndex { get; set; }
}