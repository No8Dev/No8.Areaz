using No8.Areaz;
using No8.Areaz.Layout;
using No8.Areaz.Painting;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

[TestFixture]
public class SubGridLayoutTests : BaseLayoutTests
{
    [Test]
    public void Grid_Scaffold()
    {
        var root = new LayoutNode(
            "Root",
            new Grid { CellColGap = 1, CellRowGap = 1, AreaLines = LineSet.Single, InnerLines = LineSet.None }
                .AddRows(new[] { 1, 100.Percent() })
                .AddCols(new[] { 10, 100.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("Header", 0, 0, ColSpan: 2),
                    new GridArea("Nav", 1, 0),
                    new GridArea("Content", 1, 1),
                })
        )
        {
            new("Header", new TestControl()),
            new("Nav", new TestControl()),
            new("Content", new Grid{ CellColGap = 1, CellRowGap = 1, AreaLines = LineSet.Double, InnerLines = LineSet.None, OuterLines = LineSet.Rounded }
                .AddRows(new[] {1, 100.Percent()})
                .AddCols(new[]{10, 100.Percent()})
                .AddAreas(new[]
                {
                    new GridArea("SubHeader", 0, 0, ColSpan:2),
                    new GridArea("SubNav", 1, 0),
                    new GridArea("SubContent", 1, 1)
                })
            )
        };
        
        Draw(root);
        Assert.AreEqual("""
                ╔══════════════════════════════════════╗
                ║├[Header]────────────────────────────┤║
                ╟──────────┬───────────────────────────╢
                ║┌[Nav]───┐│╭─────────────────────────╮║
                ║│░░░░░░░░│││                         │║
                ║│░░░░░░░░││╞══════════╦══════════════╡║
                ║│░░░░░░░░│││          ║              │║
                ║│░░░░░░░░│││          ║              │║
                ║│░░░░░░░░│││          ║              │║
                ║│░░░░░░░░│││          ║              │║
                ║└────────┘│╰──────────╨──────────────╯║
                ╚══════════╧═══════════════════════════╝
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Grid_Scaffold_NoGaps()
    {
        var root = new LayoutNode(
            "Root",
            new Grid()
                .AddRows(new[] { 1, 100.Percent() })
                .AddCols(new[] { 10, 100.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("Header", 0, 0, ColSpan: 2),
                    new GridArea("Nav", 1, 0),
                    new GridArea("Content", 1, 1),
                })
        )
        {
            new("Header", new TestControl { Background = Pixel.Block.ShadeLight }),
            new("Nav", new TestControl { Background = Pixel.Block.ShadeMedium }),
            new("Content", new Grid()
                .AddRows(new[] { 1, 100.Percent() })
                .AddCols(new[] { 10, 100.Percent() })
                .AddAreas(new[]
                {
                    new GridArea("SubHeader", 0, 0, ColSpan: 2),
                    new GridArea("SubNav", 1, 0),
                    new GridArea("SubContent", 1, 1)
                })
            )
            {
                new("SubHeader", new TestControl { Background = Pixel.Block.ShadeDark }),
                new("SubNav", new TestControl { Background = Pixel.Shapes.CircleBorder }),
                new("SubContent", new TestControl { Background = Pixel.Shapes.CircleSolid })
            }
        };
        
        Draw(root);
        Assert.AreEqual("""
                ├[Header]──────────────────────────────┤
                ┌[Nav]───┐├[SubHeader]─────────────────┤
                │▒▒▒▒▒▒▒▒│┌[SubNav]┐┌[SubContent]──────┐
                │▒▒▒▒▒▒▒▒││○○○○○○○○││●●●●●●●●●●●●●●●●●●│
                │▒▒▒▒▒▒▒▒││○○○○○○○○││●●●●●●●●●●●●●●●●●●│
                │▒▒▒▒▒▒▒▒││○○○○○○○○││●●●●●●●●●●●●●●●●●●│
                │▒▒▒▒▒▒▒▒││○○○○○○○○││●●●●●●●●●●●●●●●●●●│
                │▒▒▒▒▒▒▒▒││○○○○○○○○││●●●●●●●●●●●●●●●●●●│
                │▒▒▒▒▒▒▒▒││○○○○○○○○││●●●●●●●●●●●●●●●●●●│
                │▒▒▒▒▒▒▒▒││○○○○○○○○││●●●●●●●●●●●●●●●●●●│
                │▒▒▒▒▒▒▒▒││○○○○○○○○││●●●●●●●●●●●●●●●●●●│
                └────────┘└────────┘└──────────────────┘
                """,
            Canvas.ToString()
        );
    }
}