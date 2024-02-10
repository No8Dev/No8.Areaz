using System.Collections;
using System.Drawing;

namespace No8.Areaz.Layout;

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
    
    public bool FixedSize { get; set; }
    
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