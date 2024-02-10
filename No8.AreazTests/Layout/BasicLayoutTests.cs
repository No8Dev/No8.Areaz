using System.Drawing;
using No8.Areaz.Layout;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

public class BasicLayoutTests : BaseLayoutTests
{
    [Test]
    public void TestLayout_Create_Stretch()
    {
        var root = Tree.Create(new TestNode { Name = "Root" });
        
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
        var root = Tree.Create(new TestNode { Name = "Root" }, new CanvasLayout.Instructions(margin: SidesInt.One));
        
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
        var root = Tree.Create(
            new TestNode { Name = "Root", SizeRequested = new(10,10) });
        
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
        var root = Tree.Create(
            new TestNode { Name = "Root", SizeRequested = new(10,10) }, new CanvasLayout.Instructions(alignHorz:Align.End, alignVert:Align.End));
        
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
        var root = new TreeNode(new TestNode("Root"))
        {
            (   new TestNode { Name="Child", SizeRequested = new (10, 5) }, 
                new CanvasLayout.Instructions(alignHorz: Align.Center))
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
        var root = new TreeNode(new TestNode("Root"))
        {
            (   new TestNode { Name="Child", SizeRequested = new (10, 6) }, 
                new CanvasLayout.Instructions(Align.Center, Align.Center))
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
        var root = new TreeNode(new TestNode("Root"))
        {
            (   new TestNode { Name="Child", SizeRequested = new (10, 6) }, 
                new CanvasLayout.Instructions(Align.End, Align.End))
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
        var root = new TreeNode(new TestNode("Root"))
        {
            (   new TestNode { Name="Child", SizeRequested = new (10, 6) }, 
                new CanvasLayout.Instructions(Align.Stretch, Align.Center))
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
        var root = new TreeNode(new TestNode("Root"))
        {
            (   new TestNode { Name="Child", SizeRequested = new (10, 6) }, 
                new CanvasLayout.Instructions(Align.Center, Align.Stretch))
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