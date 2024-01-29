using System.Collections;
using System.Drawing;
using System.Text;
using No8.Areaz;
using No8.Areaz.Layout;
using No8.Areaz.Painting;

namespace No8.AreazTests.Models;

public class TestNode : INode
{
    private List<TestNode> _children = new ();

    public string Name { get; set; } = string.Empty;
    
    public IEnumerator<INode> GetEnumerator() => _children.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public PlannedLayout Plan { get; init; }
    public PlacementLayout Placement { get; } = new();
    public IReadOnlyList<INode> Children => _children.AsReadOnly();
    public MeasureFunc? MeasureNode { get; set; }


    public TestNode(string name, PlannedLayout? plan = null)
    {
        Name = name;
        Plan = plan ?? new();
    }

    public TestNode(PlannedLayout? plan = null)
    {
        Plan = plan ?? new();
    }
    
    public TestNode(out TestNode node, string name, PlannedLayout? plan = null) 
        : this(name, plan)
    {
        node = this;
    }

    public TestNode(out TestNode node, PlannedLayout? plan = null) 
        : this(plan)
    {
        node = this;
    }

    private void DoDraw(Canvas canvas, Rectangle bounds, LineSet lineSet)
    {
        if (bounds.Size.IsEmpty) return;
        if (bounds.Height == 0 || bounds.Width == 0)
            canvas.DrawLine(bounds, lineSet);
        else 
            canvas.DrawRectangle(bounds, lineSet);
    }
    
    public void OnDraw(Canvas canvas)
    {
        canvas.FillRectangle(
            Placement.Bounds, 
            Name.Length > 0 ? Rune.GetRuneAt(Name, 0) : Pixel.Block.LightShade);

        DoDraw(canvas, Placement.Bounds, LineSet.Single);
        DoDraw(canvas, Placement.ContentBounds, LineSet.Double);
    }
    
    public override string ToString() { return ToString(new StringBuilder(), true, false); }

    public string ToString(StringBuilder sb, bool plan, bool layout)
    {
        sb.Append($"{GetType().Name}");
        if (!string.IsNullOrEmpty(Name)) sb.Append($" Name[{Name}]");
        if (plan)
            sb.Append($" {nameof(Plan)}[{Plan}]");
        if (layout)
            sb.Append($" {nameof(Placement)}[{Placement}]");
        AppendProperties(sb);

        if (_children.Count > 0)
        {
            sb.AppendLine();
            AppendChildren(sb, plan, layout);
        }

        return sb.ToString();
    }

    protected virtual void AppendProperties(StringBuilder sb)
    {
        /*
        if (HasFocus) sb.Append($" {nameof(HasFocus)}={HasFocus}");
        if (!IsEnabled) sb.Append($" {nameof(IsEnabled)}={IsEnabled}");
        if (ForegroundBrush != null) sb.Append($" {nameof(ForegroundBrush)}={ForegroundBrush}");
        if (BackgroundBrush != null) sb.Append($" {nameof(BackgroundBrush)}={BackgroundBrush}");
        */
    }

    protected void AppendChildren(StringBuilder sb, bool plan, bool layout)
    {
        foreach (var child in _children)
        {
            child.ToString(sb, plan, layout);
            sb.AppendLine();
        }
    }
}