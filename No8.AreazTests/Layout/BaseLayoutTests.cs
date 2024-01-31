using System.Text;
using No8.Areaz.Layout;
using No8.Areaz.Painting;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout
{
    public class BaseLayoutTests
    {
        protected Canvas Canvas { get; private set; }

        protected void Draw(TestNode node, int? width = null, int? height = null)
        {
            width ??= node.Placement.Bounds.Width;
            height ??= node.Placement.Bounds.Height;
            if (width == 0) width = 40;
            if (height == 0) height = 12;

            Canvas = new Canvas(width.Value, height.Value);
            var engine = new AreaLayout();
            engine.Compute(node, width.Value, height.Value);
        
            node.OnDraw(Canvas);
        
            TestContext.WriteLine(node.ToString(new StringBuilder(), true, false));
            TestContext.WriteLine(Canvas.ToString());
            TestContext.WriteLine(node.ToString(new StringBuilder(), false, true));
        }
    }
}