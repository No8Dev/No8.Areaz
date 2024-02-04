using System.Drawing;
using No8.Areaz.Painting;

namespace No8.Areaz.Layout;

public enum Direction
{
    Unknown,
    Vertical,
    Horizontal
}

public class Stack : Node
{
    public override ILayoutManager LayoutManager { get; } = new StackLayout();
    
    public Direction StackDirection { get; set; } = Direction.Vertical;
    
    public override SizeF Measure(SizeF availableSize) => availableSize;

    public override void Paint(Canvas canvas, Rectangle rect)
    {
    }
}

public class StackLayout : ILayoutManager
{
    private static readonly Instructions DefaultInstructions = new ();
    
    public void Measure(TreeNode container, IReadOnlyList<TreeNode> children, SizeF availableSize)
    {
        float offset = 0f;
        
        var stack = container.Node as Stack ?? throw new Exception($"Unexpected container {container}");

        // Measure all the fixed size ones first
        foreach (var child in children)
        {
            if (child.MeasuredSize is null) continue;
            
            MeasureChild(stack, child, availableSize, ref offset);
        }
    }

    public void MeasureChild(Stack stack, TreeNode treeNode, SizeF remaining, ref float offset)
    {
        if (treeNode.MeasuredSize is null)
            return;
            
        var measured = treeNode.MeasuredSize.Value;
        var instructions = treeNode.Instructions as Instructions ?? DefaultInstructions;
        float x = 0f, y = 0f;
        var width = measured.Width;
        var height = measured.Height;

        if (stack.StackDirection == Direction.Horizontal)
        {
            (y, height, _) = Tree.ResolveDimension(remaining.Height, measured.Height, 0f, instructions.CrossAlign);
            x = offset;
            offset += width;
        }
        else
        {
            (x, width, _) = Tree.ResolveDimension(remaining.Width, measured.Width, 0f, instructions.CrossAlign);
            y = offset;
            offset += height;
        }

        treeNode.Bounds = new((int)x, (int)y, (int)width, (int)height);
    }
    
    /// <summary>
    ///     Properties for Child node
    /// </summary>
    public class Instructions : ILayoutManager.ILayoutInstructions
    {
        public Instructions(Align crossAlign = Align.Stretch)
        {
            CrossAlign = crossAlign;
        }

        public Align CrossAlign { get; init; }
    }
}