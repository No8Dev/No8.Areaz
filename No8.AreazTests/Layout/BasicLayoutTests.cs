using System.Drawing;
using No8.Areaz.Layout;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

public class BasicLayoutTests : BaseLayoutTests
{
    [Test]
    public void TestLayout_Create_Stretch()
    {
        var root = new LayoutNode(new TestNode { Name = "Root" });
        
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
        var root = new LayoutNode(new TestNode { Name = "Root" }, new CanvasLayout.Instructions(margin: SidesInt.One));
        
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
            new TestNode { Name = "Root" }, new CanvasLayout.Instructions(sizeRequested:new(10,10)));
        
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
            new TestNode { Name = "Root" }, new CanvasLayout.Instructions(alignHorz:Align.End, alignVert:Align.End, sizeRequested:new(10,10)));
        
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
        var root = new LayoutNode(new TestNode("Root"))
        {
            new LayoutNode(   new TestNode { Name="Child" }, 
                new CanvasLayout.Instructions(alignHorz: Align.Center, sizeRequested:new(10,5)))
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
        var root = new LayoutNode(new TestNode("Root"))
        {
            new LayoutNode(
                new TestNode { Name="Child" }, 
                new CanvasLayout.Instructions(Align.Center, Align.Center, sizeRequested:new(10,6)))
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
        var root = new LayoutNode(new TestNode("Root"))
        {
            new LayoutNode(
                new TestNode { Name="Child" }, 
                new CanvasLayout.Instructions(Align.End, Align.End, sizeRequested:new(10,6))
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
        var root = new LayoutNode(new TestNode("Root"))
        {
            new LayoutNode(
                new TestNode { Name="Child" }, 
                new CanvasLayout.Instructions(Align.Stretch, Align.Center, sizeRequested:new(10,6))
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
        var root = new LayoutNode(new TestNode("Root"))
        {
            new LayoutNode(
                new TestNode { Name="Child" }, 
                new CanvasLayout.Instructions(Align.Center, Align.Stretch, sizeRequested:new(10,6)))
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