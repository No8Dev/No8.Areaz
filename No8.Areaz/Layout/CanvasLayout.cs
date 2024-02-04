using System.Drawing;

namespace No8.Areaz.Layout;

public class CanvasLayout : ILayoutManager
{
    public static readonly CanvasLayout Default = new();
    internal static readonly Instructions DefaultInstructions = new ();

    public void Measure(TreeNode container, IReadOnlyList<TreeNode> children, SizeF availableSize)
    {
        float maxX = 0f, maxY = 0f;

        if (container.MeasuredSize is not null)
        {
            maxX = container.MeasuredSize.Value.Width;
            maxY = container.MeasuredSize.Value.Height;
        }

        foreach (var child in children)
        {
            if (child.MeasuredSize is null) continue;
            
            Measure(child, availableSize);
            
            maxX = MathF.Max(maxX, child.Bounds.Right - 1);
            maxY = MathF.Max(maxY, child.Bounds.Bottom - 1);
        }

        container.MeasuredSize = new SizeF(maxX, maxY);
        Measure(container, availableSize);
        if (container.MeasuredSize.Value.IsEmpty)
            container.MeasuredSize = container.Bounds.Size;
    }

    public void Measure(TreeNode node, SizeF availableSize)
    {
        if (node.MeasuredSize is null)
            return;
            
        var measured = node.MeasuredSize.Value;

        var instructions = node.Instructions as Instructions ?? DefaultInstructions;

        var (x, width, _) = Tree.ResolveDimension(availableSize.Width, measured.Width, instructions.XY.X, instructions.AlignHorz);
        var (y, height, _) = Tree.ResolveDimension(availableSize.Height, measured.Height, instructions.XY.Y, instructions.AlignVert);

        node.Bounds = new((int)x, (int)y, (int)width, (int)height);
    }

    /// <summary>
    ///     Instructions for laying out a node inside a Canvas Node
    /// </summary>
    public class Instructions : ILayoutManager.ILayoutInstructions
    {
        public Instructions(Align alignHorz = Align.Start, Align alignVert = Align.Start, XY? xy = null)
        {
            AlignHorz = alignHorz;
            AlignVert = alignVert;
            XY = xy ?? XY.Zero;
        }
        
        public Align AlignHorz { get; }
        public Align AlignVert { get; }
        
        // ReSharper disable once InconsistentNaming
        public XY XY { get; }

    }
}
