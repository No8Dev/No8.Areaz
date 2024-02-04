using System.Drawing;

namespace No8.Areaz.Layout
{
    public interface ILayoutManager
    {
        interface ILayoutInstructions { }

        void Measure(TreeNode container, IReadOnlyList<TreeNode> children, SizeF availableSize);
    }
}