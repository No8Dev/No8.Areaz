using No8.Areaz.Layout;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

public class BasicLayoutTests : BaseLayoutTests
{
    [Test]
    public void TestLayout1()
    {
        var root = new TestNode();
        
        AreaLayout.Compute(root, 32, 16);
        Draw(root);
        Assert.AreEqual("""
                ╔══════════════════════════════╗
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║
                ╚══════════════════════════════╝
                """,
            Canvas.ToString()
            );
    }
    
    [Test]
    public void TestLayout2()
    {
        var root = new TestNode(plan: new PlannedLayout
        {
            Width = 10,
            Height = 10
        });
        
        AreaLayout.Compute(root, 32, 16);
        Draw(root);
        Assert.AreEqual("""
                ╔════════╗
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ╚════════╝
                """,
            Canvas.ToString()
        );
    }    
    
    [Test]
    public void TestLayout3()
    {
        var root = new TestNode(new PlannedLayout { HorzAlign = Align.Center })
        {
            new TestNode(new PlannedLayout { Width = 10, Height = 10 })
        };
        
        AreaLayout.Compute(root, 32, 16);
        Draw(root);
        Assert.AreEqual("""
                ╔════════╗
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ║░░░░░░░░║
                ╚════════╝
                """,
            Canvas.ToString()
        );
    }
}