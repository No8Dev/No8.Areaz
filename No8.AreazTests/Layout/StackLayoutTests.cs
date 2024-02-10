using No8.Areaz;
using No8.Areaz.Layout;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

public class StackLayoutTests : BaseLayoutTests
{
    [Test]
    public void TestStack_Vertical()
    {
        var root = new TreeNode(new Stack { Name = "Root", StackDirection = Direction.Vertical })
        {
            new TestNode { Name = "Child1", SizeRequested = new(8, 3) },
            new TestNode { Name = "Child2", SizeRequested = new(8, 3) },
            new TestNode { Name = "Child3", SizeRequested = new(8, 3) }
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
        var root = new TreeNode(new Stack { Name = "Root", StackDirection = Direction.Vertical })
        {
            new TestNode { Name = "Child1", SizeRequested = new(8, 1) },
            new TestNode { Name = "Child2", SizeRequested = new(8, 2) },
            new TestNode { Name = "Child3", SizeRequested = new(8, 3) },
            new TestNode { Name = "Child4", SizeRequested = new(8, 4) }
        };
        
        Draw(root);
        Assert.AreEqual("""
                ╶[Child1]──────────────────────────────╴
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
        var root = new TreeNode(new Stack { Name = "Root", StackDirection = Direction.Vertical })
        {
            (new TestNode { Name = "Child1" }, new StackLayout.Instructions(sizeRequested:new(8, 1), margin:new (0,0,0, 1))),
            (new TestNode { Name = "Child2" }, new StackLayout.Instructions(sizeRequested:new(8, 2), margin:new (1,0,1, 0))),
            (new TestNode { Name = "Child3" }, new StackLayout.Instructions(sizeRequested:new(8, 4), margin:new (0,1,0, 0))),
        };
        
        Draw(root);
        Assert.AreEqual("""
                ╶[Child1]──────────────────────────────╴

                 ┌[Child2]────────────────────────────┐
                 └────────────────────────────────────┘

                ┌[Child3]──────────────────────────────┐
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                └──────────────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void TestStack_Horizontal()
    {
        var root = new TreeNode(new Stack { Name = "Root", StackDirection = Direction.Horizontal })
        {
            new TestNode { Name = "Child1", SizeRequested = new(8, 3) },
            new TestNode { Name = "Child2", SizeRequested = new(8, 3) },
            new TestNode { Name = "Child3", SizeRequested = new(8, 3) }
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
        var root = new TreeNode(new Stack { Name = "Root", StackDirection = Direction.Horizontal })
        {
            (new TestNode { Name = "Child1" }, new StackLayout.Instructions(margin:SidesInt.One, sizeRequested:new(8, 3))),
            (new TestNode { Name = "Child2" }, new StackLayout.Instructions(margin:SidesInt.One, sizeRequested:new(8, 3))),
            (new TestNode { Name = "Child3" }, new StackLayout.Instructions(margin:SidesInt.One, sizeRequested:new(8, 3)))
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
        var root = new TreeNode(new Stack { Name = "Root", StackDirection = Direction.Horizontal })
        {
            new TestNode { Name = "1", SizeRequested = new(1, 3) },
            new TestNode { Name = "2", SizeRequested = new(2, 3) },
            new TestNode { Name = "3", SizeRequested = new(4, 3) },
            new TestNode { Name = "4", SizeRequested = new(8, 3) }
        };
        
        Draw(root);
        Assert.AreEqual("""
                ╷┌[┌[3]┌[4]───┐
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
                ╵└┘└──┘└──────┘
                """,
            Canvas.ToString()
        );
    }
}