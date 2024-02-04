using System.Drawing;
using No8.Areaz.Layout;
using No8.Areaz.Painting;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout
{
    public class BaseLayoutTests
    {
        protected Canvas Canvas { get; private set; } = null!;

        protected void Draw(TreeNode treeNode, Size? size = null)
        {
            var sz = size ?? treeNode.Bounds.Size;
            if (sz.IsEmpty)
                sz = new(40, 12);
            else if (sz.Width <= 0)
                sz = sz with { Width = 40 };
            else if (sz.Height <= 0)
                sz = sz with { Height = 12 };

            Canvas = new (sz.Width, sz.Height);
            
            Tree.Layout(treeNode, sz);

            Tree.Paint(Canvas, treeNode);
            
            TestContext.WriteLine(Canvas.ToString());
        }
    }
}