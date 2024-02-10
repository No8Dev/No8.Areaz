using System.Drawing;

namespace No8.Areaz.Layout
{
    public interface ILayoutManager
    {
        /// <summary>
        ///     Layout instructions for the child node of a container
        /// </summary>
        interface ILayoutInstructions
        {
            /// <summary>
            ///     Optional size requested for the node
            /// </summary>
            SizeNumber? SizeRequested { get; init; }

            /// <summary>
            ///     Margin around node
            /// </summary>
            SidesInt? Margin { get; init; }
        }

        void MeasureIn(TreeNode container, IReadOnlyList<TreeNode> children);
        void MeasureOut(TreeNode container, IReadOnlyList<TreeNode> children);
    }
}