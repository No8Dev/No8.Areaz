using System.Collections;
using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public class Grid : Control, IEnumerable
{
    public override ILayoutManager? LayoutManager() => GridLayout.Default;
    public Rune BackgroundRune { get; set; } = Pixel.Block.Solid;

    private readonly List<GridColTemplate> _colTemplates = new();
    private readonly List<GridRowTemplate> _rowTemplates = new();
    private readonly List<GridArea> _areas = new();
    private int _cellRowGap;
    private int _cellColGap;

    
    private readonly Dictionary<int, float> _colWidths = new();
    private readonly Dictionary<int, float> _rowHeights = new();
    private readonly Dictionary<string, GridCellSize> _areaSizes = new();

    public override bool ValidGuide(ILayoutGuide? guide)
    {
        return guide is null || 
               guide.GetType().IsAssignableTo(typeof(GridGuide));
    }
    
    public override void PaintIn(Canvas canvas, Rectangle rect)
    {
        canvas.FillRectangle(rect, BackgroundRune);
        if (_cellColGap > 0)
        {
            var x = rect.X;
            var y = rect.Y;
            foreach (var pair in _colWidths)
            {
                canvas.DrawLine(x, y, x, y + rect.Height, LineSet.Rounded);
                x += (int)pair.Value + _cellColGap;
            }
            canvas.DrawLine(x, y, rect.Width, x, LineSet.Rounded);
        }
        if (_cellRowGap > 0)
        {
            var x = rect.X;
            var y = rect.Y;
            foreach (var pair in _rowHeights)
            {
                canvas.DrawLine(x, y, rect.Width, y, LineSet.Rounded);
                y += (int)pair.Value + _cellRowGap;
            }
            canvas.DrawLine(x, y, rect.Width, y, LineSet.Rounded);
        }
    }

    private void Clear()
    {
        _areaSizes.Clear();
        _colWidths.Clear();
        _rowHeights.Clear();
    }

    public void Add(GridRowTemplate row)
    {
        _rowTemplates.Add(row);
        Clear();
    }

    public void Add(GridColTemplate col)
    {
        _colTemplates.Add(col);
        Clear();
    }

    public void Add(GridArea area)
    {
        _areas.Add(area);
        Clear();
    }

    public Grid Add(IEnumerable<GridRowTemplate> rows) { _rowTemplates.AddRange(rows); Clear(); return this; }
    public Grid Add(IEnumerable<GridColTemplate> cols) { _colTemplates.AddRange(cols); Clear(); return this; }
    public Grid Add(IEnumerable<GridArea> areas) { _areas.AddRange(areas); Clear(); return this; }

    public IEnumerator GetEnumerator() => _areas.GetEnumerator();

    public IReadOnlyList<GridRowTemplate> Rows => _rowTemplates.AsReadOnly();
    public IReadOnlyList<GridColTemplate> Cols => _colTemplates.AsReadOnly();
    public IReadOnlyList<GridArea> Areas => _areas.AsReadOnly();

    public void SetColWidth(int index, float value) => _colWidths[index] = value;
    public void SetRowHeight(int index, float value) => _rowHeights[index] = value;
    public void SetAreaSize(string areaName, GridCellSize areaSize) => _areaSizes[areaName] = areaSize;

    public float GetColWidth(int index) => _colWidths[index];
    public float GetRowHeight(int index) => _rowHeights[index];
    
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

    public GridArea? GetArea(string name) => Areas.FirstOrDefault(a => a.Name == name);
    public GridCellSize GetCellSize(GridGuide guide)
    {
        if (guide.AreaName is not null)
        {
            var cellSize = _areaSizes[guide.AreaName] ?? throw new Exception($"Unknown area name:{guide.AreaName}");
            return cellSize;
        }

        var x = guide.Row!.Value * _cellRowGap;
        var y = guide.Col!.Value * _cellColGap;
        for (int i = 0; i < guide.Col; i++)
            x += (int)_colWidths[i];
        for (int i = 0; i < guide.Row; i++)
            y += (int)_rowHeights[i];
        
        return new (string.Empty,
            new (x, y, 
                (int)_colWidths[guide.Col!.Value], (int)_rowHeights[guide.Row!.Value]));
    }
}

public class GridLayout : ILayoutManager
{
    public static readonly GridLayout Default = new();
    internal static readonly GridGuide DefaultGuide = new ();
    
    public void MeasureIn(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
        var grid = container.Control as Grid ?? throw new Exception($"Unexpected container {container}");
        if (container.MeasuredSize is null) throw new Exception($"Control has no measured size {grid}");
        
        // Measure all the fixed size ones first
        var availableSize = container.MeasuredSize.Value;
        var remainingSize = availableSize;

        var actualCols = new int[grid.Cols.Count];
        var actualRows = new int[grid.Rows.Count];

        var totalColPercentages = grid.Cols
            .Where(c => c.Size?.IsPercent ?? false)
            .Sum(c => c.Size!.Value);
        var totalRowPercentages = grid.Rows
            .Where(c => c.Size?.IsPercent ?? false)
            .Sum(c => c.Size!.Value);
        
        //   _____ ______
        // | cell | cell |   number of columns + 1
        //  ------ ------
        // | cell | cell |
        //  ------ ------    number of rows + 1
        remainingSize.Width -= grid.CellColGap * (grid.Cols.Count + 1);
        remainingSize.Height -= grid.CellRowGap * (grid.Rows.Count + 1);
        
        // Fixed first so can calculate percentages next
        for (int i = 0; i < grid.Cols.Count; i++)
        {
            var col = grid.Cols[i];
            var sizeRequested = col.Size ?? throw new Exception($"Cannot calculate Column size: {col}");
            if (!sizeRequested.IsPercent)
            {
                // Fixed size
                var value = sizeRequested.Resolve(availableSize.Width);
                remainingSize.Width -= value;
                actualCols[i] = (int)value;
            }
        }

        var variableSize = remainingSize.Width;
        for (int i = 0; i < grid.Cols.Count; i++)
        {
            var col = grid.Cols[i];
            var sizeRequested = col.Size ?? throw new Exception($"Cannot calculate Column size: {col}");
            if (sizeRequested.IsPercent)
            {
                if (totalColPercentages != 0f && remainingSize.Width > 0)
                {
                    var actualWidth = variableSize * (sizeRequested.Value / totalColPercentages);
                    actualCols[i] = (int)actualWidth;
                    remainingSize.Width -= actualWidth;
                }
            }
        }        
        
        // Fixed first so can calculate percentages next
        for (int i = 0; i < grid.Rows.Count; i++)
        {
            var row = grid.Rows[i];
            var sizeRequested = row.Size ?? throw new Exception($"Cannot calculate Row size: {row}");
            if (!sizeRequested.IsPercent)
            {
                var value = sizeRequested.Resolve(availableSize.Height);
                actualRows[i] = (int)value;
                remainingSize.Height -= value;
            }
        }

        variableSize = remainingSize.Height;
        for (int i = 0; i < grid.Rows.Count; i++)
        {
            var row = grid.Rows[i];
            var sizeRequested = row.Size ?? throw new Exception($"Cannot calculate Row size: {row}");
            if (sizeRequested.IsPercent)
            {
                if (totalRowPercentages != 0f && remainingSize.Height > 0)
                {
                    var value = variableSize * (sizeRequested.Value / totalRowPercentages);
                    actualRows[i] = (int)value;
                    remainingSize.Height -= value;
                }
            }
        }

        for (int i = 0; i < actualCols.Length; i++)
            grid.SetColWidth(i, actualCols[i]);
        for (int i = 0; i < actualRows.Length; i++)
            grid.SetRowHeight(i, actualRows[i]);

        foreach (var area in grid.Areas)
        {
            var x = 0;
            var y = 0;
            for (int i = 0; i < area.Col; i++)
                x += actualCols[i];
            for (int j = 0; j < area.Row; j++)
                y += actualRows[j];
            var width = 0;
            var height = 0;
            for (int i = area.Col; i < area.Col + area.ColSpan; i++)
                width += actualCols[i];
            for (int i = area.Row; i < area.Row + area.RowSpan; i++)
                height += actualRows[i];

            var areaSize = new GridCellSize(area.Name, new Rectangle(x, y, width, height));
            grid.SetAreaSize(area.Name, areaSize);
        }
        
        // Now that the Grid rows and cols have been measured, lets set all the children bounds
        foreach (var child in children)
        {
            var childGuide = child.Guide as GridGuide ?? throw new Exception($"Grid child does not have a GridGuide: {child.Name}");
            var cellSize = grid.GetCellSize(childGuide);
            child.MeasuredSize = cellSize.Rect.Size;
            child.Bounds = cellSize.Rect;
        }
    }

    public void MeasureOut(LayoutNode container, IReadOnlyList<LayoutNode> children)
    {
        
    }
}

/// <summary>
///     Guide for laying out a node inside a Grid
/// </summary>
public class GridGuide : ILayoutGuide
{
    public GridGuide(int? col = null, int? row = null, string? areaName = null)
    {
        Size = null;
        Margin = null;
        AreaName = areaName;
        Col = col;
        Row = row;
    }
    
    public int? Col { get; set; }
    public int? Row { get; set; }
    public string? AreaName { get; set; }
        

    public SizeNumber? Size { get; init; }
    public SidesInt? Margin { get; init; }
    public override string ToString() => BuildString(new StringBuilder()).ToString();
    protected virtual StringBuilder BuildString(StringBuilder? sb = null)
    {
        sb ??= new();
        sb.Append($"{GetType().FullName}");
        if (!string.IsNullOrEmpty(AreaName)) sb.Append($" AreaName:{AreaName}");
        if (Col is not null) sb.Append($" Col:{Col.Value}");
        if (Row is not null) sb.Append($" Row:{Row.Value}");
        return sb;
    }
}

public record GridArea(
    string Name,
    int Row,
    int Col,
    int RowSpan = 1,
    int ColSpan = 1);

public record GridCellSize(
    string Name, 
    Rectangle Rect);
