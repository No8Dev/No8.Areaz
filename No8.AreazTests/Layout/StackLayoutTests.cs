using No8.Areaz;
using No8.Areaz.Layout;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

public class StackLayoutTests : BaseLayoutTests
{
    [Test]
    public void TestStack_Vertical()
    {
        var root = new LayoutNode(
            "Root", 
            new Stack { StackDirection = Direction.Vertical })
        {
            new ("Child1", new TestControl(), new StackGuide(size:new(8,3))),
            new ("Child2", new TestControl(), new StackGuide(size:new(8,3))),
            new ("Child3", new TestControl(), new StackGuide(size:new(8,3)))
        };
        
        Draw(root);
        Assert.AreEqual("""
                ┌[Child1]──────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                ┌[Child2]──────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                ┌[Child3]──────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestStack_Vertical_Small()
    {
        var root = new LayoutNode(
            "Root",
            new Stack { StackDirection = Direction.Vertical })
        {
            new("Child1", new TestControl(), new StackGuide(size:new(8, 1))),
            new("Child2", new TestControl(), new StackGuide(size:new(8, 2))),
            new("Child3", new TestControl(), new StackGuide(size:new(8, 3))),
            new("Child4", new TestControl(), new StackGuide(size:new(8, 4)))
        };
        
        Draw(root);
        Assert.AreEqual("""
                ├[Child1]──────────────────────────────┤
                ┌[Child2]──────────────────────────────┐
                └──────────────────────────────────────┘
                ┌[Child3]──────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                ┌[Child4]──────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestStack_Vertical_Small_Margin()
    {
        var root = new LayoutNode(
            "Root",
            new Stack { StackDirection = Direction.Vertical })
        {
            new ("Child1", new TestControl(), new StackGuide(size:new(8, 1), margin:new (0,0,0, 1))),
            new ("Child2", new TestControl(), new StackGuide(size:new(8, 2), margin:new (1,0,1, 0))),
            new ("Child3", new TestControl(), new StackGuide(size:new(8, 4), margin:new (2,1,2, 0))),
        };
        
        Draw(root);
        Assert.AreEqual("""
                ├[Child1]──────────────────────────────┤

                 ┌[Child2]────────────────────────────┐
                 └────────────────────────────────────┘

                  ┌[Child3]──────────────────────────┐
                  │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                  │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                  └──────────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestStack_Horizontal()
    {
        var root = new LayoutNode(
            "Root", 
            new Stack { StackDirection = Direction.Horizontal })
        {
            new ("Child1", new TestControl(), new StackGuide(size:new(8, 3))),
            new ("Child2", new TestControl(), new StackGuide(size:new(8, 3))),
            new ("Child3", new TestControl(), new StackGuide(size:new(8, 3)))
        };
        
        Draw(root);
        Assert.AreEqual("""
                ┌[Child1┌[Child2┌[Child3]
                │░░░░░░││░░░░░░││░░░░░░│
                │░░░░░░││░░░░░░││░░░░░░│
                │░░░░░░││░░░░░░││░░░░░░│
                │░░░░░░││░░░░░░││░░░░░░│
                │░░░░░░││░░░░░░││░░░░░░│
                │░░░░░░││░░░░░░││░░░░░░│
                │░░░░░░││░░░░░░││░░░░░░│
                │░░░░░░││░░░░░░││░░░░░░│
                │░░░░░░││░░░░░░││░░░░░░│
                │░░░░░░││░░░░░░││░░░░░░│
                └──────┘└──────┘└──────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestStack_Horizontal_Margin()
    {
        var root = new LayoutNode(
            "Root", 
            new Stack { StackDirection = Direction.Horizontal })
        {
            new LayoutNode("Child1", new TestControl(), new StackGuide(margin:SidesInt.One, size:new(8, 3))),
            new LayoutNode("Child2", new TestControl(), new StackGuide(margin:SidesInt.One, size:new(8, 3))),
            new LayoutNode("Child3", new TestControl(), new StackGuide(margin:SidesInt.One, size:new(8, 3)))
        };
        
        Draw(root);
        Assert.AreEqual("""

                 ┌[Child1] ┌[Child2] ┌[Child3]
                 │░░░░░░│  │░░░░░░│  │░░░░░░│
                 │░░░░░░│  │░░░░░░│  │░░░░░░│
                 │░░░░░░│  │░░░░░░│  │░░░░░░│
                 │░░░░░░│  │░░░░░░│  │░░░░░░│
                 │░░░░░░│  │░░░░░░│  │░░░░░░│
                 │░░░░░░│  │░░░░░░│  │░░░░░░│
                 │░░░░░░│  │░░░░░░│  │░░░░░░│
                 │░░░░░░│  │░░░░░░│  │░░░░░░│
                 └──────┘  └──────┘  └──────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestStack_Horizontal_Small()
    {
        var root = new LayoutNode(
            "Root", 
            new Stack { StackDirection = Direction.Horizontal })
        {
            new ("1", new TestControl(), new StackGuide(size:new(1, 3))),
            new ("2", new TestControl(), new StackGuide(size:new(2, 3))),
            new ("3", new TestControl(), new StackGuide(size:new(4, 3))),
            new ("4", new TestControl(), new StackGuide(size:new(8, 3)))
        };
        
        Draw(root);
        Assert.AreEqual("""
                ┬┌[┌[3]┌[4]───┐
                ││││░░││░░░░░░│
                ││││░░││░░░░░░│
                ││││░░││░░░░░░│
                ││││░░││░░░░░░│
                ││││░░││░░░░░░│
                ││││░░││░░░░░░│
                ││││░░││░░░░░░│
                ││││░░││░░░░░░│
                ││││░░││░░░░░░│
                ││││░░││░░░░░░│
                ┴└┘└──┘└──────┘
                """,
            Canvas.ToString()
        );
    }
}