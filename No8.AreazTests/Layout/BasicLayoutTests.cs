using System.Drawing;
using No8.Areaz.Layout;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

public class BasicLayoutTests : BaseLayoutTests
{
    private Size DefaultSize = new(32, 16);
    
    [Test]
    public void TestLayout1()
    {
        var root = new TreeNode(
            new TestNode("Root"));
        
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
    public void TestLayout2()
    {
        var root = new TreeNode(
            new TestNode { Name = "Root", Size = new(10,10) },
            new CanvasLayout.Instructions());
        
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
            (   new TestNode { Name="Child", Size = new (10, 5) }, 
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
            (   new TestNode { Name="Child", Size = new (10, 6) }, 
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
            (   new TestNode { Name="Child", Size = new (10, 6) }, 
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
            (   new TestNode { Name="Child", Size = new (10, 6) }, 
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
            (   new TestNode { Name="Child", Size = new (10, 6) }, 
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