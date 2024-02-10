using System.Collections;
using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public class LayoutNode : IEnumerable<LayoutNode>
{
    private readonly List<LayoutNode> _children = new();
    
    public INode Node { get; }
    public ILayoutManager.ILayoutInstructions? Instructions { get; set; }
    
    /// <summary>
    ///     The size that the node thinks it should be
    /// </summary>
    public SizeF? MeasuredSize { get; set; }
    
    /// <summary>
    ///     The location and size of the node after Measure and Layout
    /// </summary>
    public Rectangle Bounds { get; set; }
    
    public bool FixedSize { get; set; }
    
    public bool IsLeaf() => _children.IsEmpty();

    public LayoutNode(INode node, ILayoutManager.ILayoutInstructions? instructions = null)
    {
        Node = node;
        Instructions = instructions;
    }

    public IEnumerator<LayoutNode> GetEnumerator() => _children.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IReadOnlyList<LayoutNode> Children => _children.AsReadOnly();

    public LayoutNode Add(LayoutNode layoutNode)
    {
        _children.Add(layoutNode);
        return this;
    }
    public LayoutNode Add(INode node, ILayoutManager.ILayoutInstructions? instructions = null)
    {
        return Add(new LayoutNode(node, instructions));
    }

    public LayoutNode Add(out LayoutNode layoutNode, INode node, ILayoutManager.ILayoutInstructions? instructions = null)
    {
        layoutNode = new(node, instructions);
        Add(layoutNode);
        return this;
    }
    
    public override string ToString() => BuildString(new StringBuilder()).ToString();

    protected virtual StringBuilder BuildString(StringBuilder? sb = null, int indent = 0)
    {
        sb ??= new();
        sb.Append(" ".PadLeft(indent+1, '→'));
        sb.Append($"{GetType().Name}");
        if (!string.IsNullOrEmpty(Node.Name)) sb.Append($" [{Node.Name}]");
        if (MeasuredSize is not null) sb.Append($" MeasuredSize{MeasuredSize.Value}");
        if (!Bounds.IsEmpty) sb.Append($" Bounds{Bounds}");
        sb.AppendLine();

        foreach (var child in Children)
            child.BuildString(sb, indent + 1);
        
        return sb;
    }
    
    internal StringBuilder BuildInstructionsString(StringBuilder? sb = null, int indent = 0)
    {
        sb ??= new();
        sb.Append(" ".PadLeft(indent+1, '→'));
        if (!string.IsNullOrEmpty(Node.Name)) sb.Append($"[{Node.Name}] ");
        if (Instructions is not null)
            sb.Append($"{Instructions}");
        sb.AppendLine();

        foreach (var child in Children)
            child.BuildInstructionsString(sb, indent + 1);
        
        return sb;
    }

}