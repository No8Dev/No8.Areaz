using No8.Areaz;
using No8.Areaz.Layout;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

public class StackLayoutTests : BaseLayoutTests
{
    [Test]
    public void Test_Vertical_Stack()
    {
        var root = new TreeNode(new Stack { Name = "Root", StackDirection = Direction.Vertical })
        {
            new TestNode { Name = "Child1", Size = new(8, 3) },
            new TestNode { Name = "Child2", Size = new(8, 3) },
            new TestNode { Name = "Child3", Size = new(8, 3) }
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
    public void Test_Vertical_Stack_Small()
    {
        var root = new TreeNode(new Stack { Name = "Root", StackDirection = Direction.Vertical })
        {
            new TestNode { Name = "Child1", Size = new(8, 1) },
            new TestNode { Name = "Child2", Size = new(8, 2) },
            new TestNode { Name = "Child3", Size = new(8, 3) },
            new TestNode { Name = "Child4", Size = new(8, 4) }
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
    public void Test_Horizontal_Stack()
    {
        var root = new TreeNode(new Stack { Name = "Root", StackDirection = Direction.Horizontal })
        {
            new TestNode { Name = "Child1", Size = new(8, 3) },
            new TestNode { Name = "Child2", Size = new(8, 3) },
            new TestNode { Name = "Child3", Size = new(8, 3) }
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
}