using No8.Areaz;
using No8.Areaz.Layout;
using No8.Areaz.Painting;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

[TestFixture]
public class GridLayoutTests : BaseLayoutTests
{
    [Test]
    public void Grid_Empty()
    {
        var root = new LayoutNode(
            "Root",
            new Grid { Background = Pixel.Block.Lower4 }
        );
        
        Draw(root);
        Assert.AreEqual("""
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Grid_Scaffold()
    {
        var root = new LayoutNode(
            "Root",
            new Grid()
                .AddRows(new[] { 2, 100.Percent(), 3 })
                .AddCols(new[] { 25.Percent(), 75.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("Header", 0, 0, ColSpan: 2),
                    new GridArea("Nav", 1, 0, RowSpan: 2),
                    new GridArea("Content", 1, 1),
                    new GridArea("Footer", 2, 1)
                })
        )
        {
            new("Header", new TestControl { LineSet = LineSet.Rounded }),
            new("Nav", new TestControl { LineSet = LineSet.Rounded }),
            new("Content", new TestControl { BackgroundRune = Pixel.Shapes.CircleBorder }),
            new("Footer", new TestControl())
        };
        
        Draw(root);
        Assert.AreEqual("""
                ╭[Header]──────────────────────────────╮
                ╰──────────────────────────────────────╯
                ╭[Nav]───╮┌[Content]───────────────────┐
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░│└────────────────────────────┘
                │░░░░░░░░│┌[Footer]────────────────────┐
                │░░░░░░░░││░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                ╰────────╯└────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Grid_Scaffold_Gap()
    {
        var root = new LayoutNode(
            "Root",
            new Grid { CellColGap = 1, CellRowGap = 1 }
                .AddRows(new[] { 1, 100.Percent(), 2 })
                .AddCols(new[] { 10, 100.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("Header", 0, 0, ColSpan: 2),
                    new GridArea("Nav", 1, 0),
                    new GridArea("Content", 1, 1),
                    new GridArea("Footer", 2, 0, ColSpan: 2)
                })
        )
        {
            new("Header", new TestControl { LineSet = LineSet.Rounded }),
            new("Nav", new TestControl { LineSet = LineSet.Rounded }),
            new("Content", new TestControl { BackgroundRune = Pixel.Shapes.CircleBorder }),
            new("Footer", new TestControl())
        };
        
        Draw(root);
        Assert.AreEqual("""
                ╔══════════╤═══════════════════════════╗
                ║├[Header]────────────────────────────┤║
                ╟──────────┼───────────────────────────╢
                ║╭[Nav]───╮│┌[Content]────────────────┐║
                ║│░░░░░░░░│││○○○○○○○○○○○○○○○○○○○○○○○○○│║
                ║│░░░░░░░░│││○○○○○○○○○○○○○○○○○○○○○○○○○│║
                ║│░░░░░░░░│││○○○○○○○○○○○○○○○○○○○○○○○○○│║
                ║╰────────╯│└─────────────────────────┘║
                ╟──────────┼───────────────────────────╢
                ║┌[Footer]────────────────────────────┐║
                ║└────────────────────────────────────┘║
                ╚══════════╧═══════════════════════════╝
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Grid_Scaffold_NoContent()
    {
        var root = new LayoutNode(
            "Root",
            new Grid { CellColGap = 1, CellRowGap = 1 }
                .AddRows(new[] { 1, 100.Percent(), 2 })
                .AddCols(new[] { 10, 100.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("Header", 0, 0, ColSpan: 2),
                    new GridArea("Nav", 1, 0),
                    new GridArea("Content", 1, 1),
                    new GridArea("Footer", 2, 0, ColSpan: 2)
                })
        );
        
        Draw(root);
        Assert.AreEqual("""
                ╔══════════╤═══════════════════════════╗
                ║          │                           ║
                ╟──────────┼───────────────────────────╢
                ║          │                           ║
                ║          │                           ║
                ║          │                           ║
                ║          │                           ║
                ║          │                           ║
                ╟──────────┼───────────────────────────╢
                ║          │                           ║
                ║          │                           ║
                ╚══════════╧═══════════════════════════╝
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Grid_Scaffold_AreaLines()
    {
        var root = new LayoutNode(
            "Root",
            new Grid { CellColGap = 1, CellRowGap = 1, AreaLines = LineSet.Single, InnerLines = LineSet.None }
                .AddRows(new[] { 1, 100.Percent(), 2 })
                .AddCols(new[] { 10, 100.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("Header", 0, 0, ColSpan: 2),
                    new GridArea("Nav", 1, 0),
                    new GridArea("Content", 1, 1),
                    new GridArea("Footer", 2, 0, ColSpan: 2)
                })
        )
        {
            new("Header", new TestControl { LineSet = LineSet.Rounded }),
            new("Nav", new TestControl { LineSet = LineSet.Rounded }),
            new("Content", new TestControl { BackgroundRune = Pixel.Shapes.CircleBorder }),
            new("Footer", new TestControl())
        };
        
        Draw(root);
        Assert.AreEqual("""
                ╔══════════════════════════════════════╗
                ║├[Header]────────────────────────────┤║
                ╟──────────┬───────────────────────────╢
                ║╭[Nav]───╮│┌[Content]────────────────┐║
                ║│░░░░░░░░│││○○○○○○○○○○○○○○○○○○○○○○○○○│║
                ║│░░░░░░░░│││○○○○○○○○○○○○○○○○○○○○○○○○○│║
                ║│░░░░░░░░│││○○○○○○○○○○○○○○○○○○○○○○○○○│║
                ║╰────────╯│└─────────────────────────┘║
                ╟──────────┴───────────────────────────╢
                ║┌[Footer]────────────────────────────┐║
                ║└────────────────────────────────────┘║
                ╚══════════════════════════════════════╝
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Grid_Scaffold_Outer()
    {
        var root = new LayoutNode(
            "Root",
            new Grid { InnerLines = LineSet.None }
                .AddRows(new[] { 1, 100.Percent(), 2 })
                .AddCols(new[] { 10, 100.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("Header", 0, 0, ColSpan: 2),
                    new GridArea("Nav", 1, 0),
                    new GridArea("Content", 1, 1),
                    new GridArea("Footer", 2, 0, ColSpan: 2)
                })
        )
        {
            new("Header", new TestControl { LineSet = LineSet.Rounded }),
            new("Nav", new TestControl { LineSet = LineSet.Rounded }),
            new("Content", new TestControl { BackgroundRune = Pixel.Shapes.CircleBorder }),
            new("Footer", new TestControl())
        };
        
        Draw(root);
        Assert.AreEqual("""
                ├[Header]──────────────────────────────┤
                ╭[Nav]───╮┌[Content]───────────────────┐
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │░░░░░░░░││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                ╰────────╯└────────────────────────────┘
                ┌[Footer]──────────────────────────────┐
                └──────────────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Grid_Scaffold_NoContent_AreaLine()
    {
        var root = new LayoutNode(
            "Root",
            new Grid
                {
                    CellColGap = 1, CellRowGap = 1, 
                    AreaLines = LineSet.Rounded, 
                    InnerLines = LineSet.None,
                    OuterLines = LineSet.Rounded
                }
                .AddRows(new[] { 1, 100.Percent(), 2 })
                .AddCols(new[] { 10, 100.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("Header", 0, 0, ColSpan: 2),
                    new GridArea("Nav", 1, 0),
                    new GridArea("Content", 1, 1),
                    new GridArea("Footer", 2, 0, ColSpan: 2)
                })
        );
        
        Draw(root);
        Assert.AreEqual("""
                ╭──────────────────────────────────────╮
                │                                      │
                ├──────────┬───────────────────────────┤
                │          │                           │
                │          │                           │
                │          │                           │
                │          │                           │
                │          │                           │
                ├──────────┴───────────────────────────┤
                │                                      │
                │                                      │
                ╰──────────────────────────────────────╯
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Grid_Overlap()
    {
        var root = new LayoutNode(
            "Root",
            new Grid()
                .AddRows(new[]
                    { 7.Percent(), 7.Percent(), 7.Percent(), 7.Percent(), 7.Percent(), 7.Percent(), 7.Percent() })
                .AddCols(new[]
                    { 7.Percent(), 7.Percent(), 7.Percent(), 7.Percent(), 7.Percent(), 7.Percent(), 7.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("1st", 0, 0, ColSpan: 3, RowSpan: 3),
                    new GridArea("3rd", 2, 2, ColSpan: 3, RowSpan: 3),
                    new GridArea("2nd", 4, 4, ColSpan: 3, RowSpan: 3),
                })
        )
        {
            new("1st", new TestControl { LineSet = LineSet.Single }),
            new("2nd", new TestControl { LineSet = LineSet.Rounded }),
            new("3rd", new TestControl { LineSet = LineSet.Double }),
        };
        
        Draw(root);
        Assert.AreEqual("""
                ┌[1st]────────┐
                │░░░░░░░░░░░░░│
                └─────────╔[3rd]════════╗
                          ║░░░░░░░░░░░░░║
                          ╚═════════════╝]────────╮
                                    │░░░░░░░░░░░░░│
                                    ╰─────────────╯
                """,
            Canvas.ToString()
        );
    }
}