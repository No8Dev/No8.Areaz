using System.Collections;
using System.Drawing;

namespace No8.Areaz.Layout;

public class LayoutNode : IEnumerable<LayoutNode>
{
    private readonly List<LayoutNode> _children = new();
    
    public string Name { get; set; }
    public IControl Control { get; }
    public ILayoutGuide? Guide { get; set; }
    
    /// <summary>
    ///     The size that the node thinks it should be
    /// </summary>
    public Size? MeasuredSize { get; set; }
    
    /// <summary>
    ///     The location and size of the node after Measure and Layout
    /// </summary>
    public Rectangle Bounds { get; set; }
    
    public bool FixedSize { get; set; }
    
    public bool IsLeaf() => _children.IsEmpty();

    public LayoutNode(IControl control, ILayoutGuide? guide = null) 
        : this(string.Empty, control, guide)
    { }

    public LayoutNode(string name, IControl control, ILayoutGuide? guide = null)
    {
        Name = name;
        Control = control;
        Guide = guide;

        if (string.IsNullOrEmpty(Control.Name))
            Control.Name = Name;
    }

    public IEnumerator<LayoutNode> GetEnumerator() => _children.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IReadOnlyList<LayoutNode> Children => _children.AsReadOnly();

    public LayoutNode Add(LayoutNode layoutNode)
    {
        if (!Control.ValidGuide(layoutNode.Guide))
            throw new Exception($"{layoutNode.Control.GetType()} does not support a Guide of type {layoutNode.Guide?.GetType()}.");
        
        _children.Add(layoutNode);
        return this;
    }
    public LayoutNode Add(IControl control, ILayoutGuide? guide = null)
    {
        return Add(new LayoutNode(control, guide));
    }

    public LayoutNode Add(out LayoutNode layoutNode, IControl control, ILayoutGuide? guide = null)
    {
        layoutNode = new(control, guide);
        Add(layoutNode);
        return this;
    }
    
    public override string ToString() => BuildString(new StringBuilder()).ToString();

    protected virtual StringBuilder BuildString(StringBuilder? sb = null, int indent = 0)
    {
        sb ??= new();
        sb.Append(" ".PadLeft(indent+1, '→'));
        sb.Append($"{GetType().Name}");
        if (!string.IsNullOrEmpty(Control.Name)) sb.Append($" [{Control.Name}]");
        if (MeasuredSize is not null) sb.Append($" MeasuredSize{MeasuredSize.Value}");
        if (!Bounds.IsEmpty) sb.Append($" Bounds{Bounds}");
        sb.AppendLine();

        foreach (var child in Children)
            child.BuildString(sb, indent + 1);
        
        return sb;
    }
    
    internal StringBuilder BuildGuideString(StringBuilder? sb = null, int indent = 0)
    {
        sb ??= new();
        sb.Append(" ".PadLeft(indent+1, '→'));
        if (!string.IsNullOrEmpty(Control.Name)) sb.Append($"[{Control.Name}] ");
        if (Guide is not null)
            sb.Append($"{Guide}");
        sb.AppendLine();

        foreach (var child in Children)
            child.BuildGuideString(sb, indent + 1);
        
        return sb;
    }

}