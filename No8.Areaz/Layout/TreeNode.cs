using System.Collections;
using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public class Tree
{
    public static void Layout(TreeNode root, Size availableSize)
    {
        LayoutInternal(root, availableSize);

        //treeNode.MeasuredSize = treeNode.Node.Measure(availableSize);
        //treeNode.Instructions ??= new CanvasLayout.Instructions(XY.Zero, Align.Stretch, Align.Stretch);
        //CanvasLayout.Default.Measure(treeNode, treeNode.Children, availableSize);
    }
    
    internal static void LayoutInternal(TreeNode treeNode, Size availableSize)
    {
        foreach (var child in treeNode)
            LayoutInternal(child, availableSize);

        treeNode.MeasuredSize = treeNode.Node.Measure(availableSize);
        treeNode.Node.LayoutManager.Measure(treeNode, treeNode.Children, availableSize);
    }

    public static void Paint(Canvas canvas, TreeNode treeNode)
    {
        treeNode.Node.Paint(canvas, treeNode.Bounds);
        foreach (var child in treeNode.Children)
            Paint(canvas, child);
    }

    // ReSharper disable once UnusedTupleComponentInReturnValue
    /// <summary>
    ///     Calculate where this node is to be painted on the canvas        
    /// </summary>
    public static (float start, float size, bool overflow) 
        ResolveDimension(float available, float measured, float offset, Align align)
    {
        available -= offset;
        bool overflow = measured > available;

        if (available <= 0)
            return (offset, measured, overflow: true);
        
        switch (align)
        {
            case Align.Start:
                return (offset, measured, overflow);
            case Align.End:
                if (overflow)
                    return (offset, available, overflow);
                return (available - measured, measured, overflow);
            case Align.Center:
                if (measured > available)
                    return (offset, available, overflow);
                var remaining = available - measured;
                return (offset + MathF.Floor(remaining / 2f), measured, overflow);
            case Align.Stretch:
                return (offset, available, overflow);
                
            default:
                throw new ArgumentOutOfRangeException(nameof(align), "Invalid alignment");
        }
    }
}


public class TreeNode : IEnumerable<TreeNode>
{
    private readonly List<TreeNode> _children = new();
    
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
    
    public bool IsLeaf() => _children.IsEmpty();

    public TreeNode(INode node, ILayoutManager.ILayoutInstructions? instructions = null)
    {
        Node = node;
        Instructions = instructions;
    }

    public IEnumerator<TreeNode> GetEnumerator() => _children.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IReadOnlyList<TreeNode> Children => _children.AsReadOnly();

    public TreeNode Add(TreeNode treeNode)
    {
        _children.Add(treeNode);
        return this;
    }
    public TreeNode Add(INode node) => Add(new TreeNode(node));

    public TreeNode Add((INode node, ILayoutManager.ILayoutInstructions instrictions) pair) 
        => Add(new TreeNode(pair.node, pair.instrictions));

    public TreeNode Add(out TreeNode treeNode, INode node, ILayoutManager.ILayoutInstructions? instructions = null)
    {
        treeNode = new(node, instructions);
        Add(treeNode);
        return this;
    }
}