using No8.Areaz.Layout;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

public class BasicLayoutTests : BaseLayoutTests
{
    [Test]
    public void TestLayout_Create_Stretch()
    {
        var root = new LayoutNode("Root", new TestControl());
        
        Draw(root);
        Assert.AreEqual("""
                ┌[Root]────────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                """,
            Canvas.ToString()
            );
    }

    [Test]
    public void TestLayout_Margin()
    {
        var root = new LayoutNode(
            "Root", 
            new TestControl(), 
            new CanvasGuide(margin: SidesInt.One));
        
        Draw(root);
        Assert.AreEqual("""

                 ┌[Root]──────────────────────────────┐
                 │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                 │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                 │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                 │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                 │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                 │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                 │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                 │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                 └────────────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }

    [Test]
    public void TestLayout_CreateWithSize_AlignStart()
    {
        var root = new LayoutNode(
            "Root",
            new TestControl(), 
            new CanvasGuide(size:new(10,10)));
        
        Draw(root);
        Assert.AreEqual("""
                ┌[Root]──┐
                │░░░░░░░░│
                │░░░░░░░░│
                │░░░░░░░░│
                │░░░░░░░░│
                │░░░░░░░░│
                │░░░░░░░░│
                │░░░░░░░░│
                │░░░░░░░░│
                └────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestLayout_CreateWithInstructions_AlignEnd()
    {
        var root = new LayoutNode(
            "Root",
            new TestControl(), 
            new CanvasGuide(alignHorz:Align.End, alignVert:Align.End, size:new(10,10)));
        
        Draw(root);
        Assert.AreEqual("""


                                              ┌[Root]──┐
                                              │░░░░░░░░│
                                              │░░░░░░░░│
                                              │░░░░░░░░│
                                              │░░░░░░░░│
                                              │░░░░░░░░│
                                              │░░░░░░░░│
                                              │░░░░░░░░│
                                              │░░░░░░░░│
                                              └────────┘
                """,
            Canvas.ToString()
        );
    }    
    
    [Test]
    public void TestLayout_Child_HorzCenter()
    {
        var root = new LayoutNode("Root", new TestControl())
        {
            new LayoutNode("Child",   new TestControl(), 
                new CanvasGuide(alignHorz: Align.Center, size:new(10,5)))
        };
        
        Draw(root);
        Assert.AreEqual("""
                ┌[Root]────────┌[Child]─┐──────────────┐
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░└────────┘░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestLayout_Child_Center()
    {
        var root = new LayoutNode("Root", new TestControl())
        {
            new LayoutNode(
                "Child",
                new TestControl(), 
                new CanvasGuide(Align.Center, Align.Center, size:new(10,6)))
        };
        
        Draw(root);
        Assert.AreEqual("""
                ┌[Root]────────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░┌[Child]─┐░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░└────────┘░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestLayout_Child_End()
    {
        var root = new LayoutNode("Root", new TestControl())
        {
            new LayoutNode(
                "Child",
                new TestControl(), 
                new CanvasGuide(Align.End, Align.End, size:new(10,6))
                )
        };
        
        Draw(root);
        Assert.AreEqual("""
                ┌[Root]────────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░┌[Child]─┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│░░░░░░░░│
                └─────────────────────────────└────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestLayout_Child_HorzStretch()
    {
        var root = new LayoutNode("Root", new TestControl())
        {
            new LayoutNode(
                "Child",
                new TestControl(), 
                new CanvasGuide(Align.Stretch, Align.Center, size:new(10,6))
                )
        };
        
        Draw(root);
        Assert.AreEqual("""
                ┌[Root]────────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                ┌[Child]───────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestLayout_Child_VertStretch()
    {
        var root = new LayoutNode("Root", new TestControl())
        {
            new LayoutNode(
                "Child",
                new TestControl(), 
                new CanvasGuide(Align.Center, Align.Stretch, size:new(10,6)))
        };
        
        Draw(root);
        Assert.AreEqual("""
                ┌[Root]────────┌[Child]─┐──────────────┐
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░│░░░░░░░░│░░░░░░░░░░░░░░│
                └──────────────└────────┘──────────────┘
                """,
            Canvas.ToString()
        );
    }
}