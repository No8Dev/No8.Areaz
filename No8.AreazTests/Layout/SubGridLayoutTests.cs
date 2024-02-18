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
            new("Header", new TestControl { BackgroundRune = Pixel.Block.ShadeLight }),
            new("Nav", new TestControl { BackgroundRune = Pixel.Block.ShadeMedium }),
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
                new("SubHeader", new TestControl { BackgroundRune = Pixel.Block.ShadeDark }),
                new("SubNav", new TestControl { BackgroundRune = Pixel.Shapes.CircleBorder }),
                new("SubContent", new TestControl { BackgroundRune = Pixel.Shapes.CircleSolid })
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
    
    [Test]
    public void Grid_Scaffold_NoGaps_Align()
    {
        LayoutNode subHeader, subNav;
        
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
            new("Header", new Frame { BackgroundRune = Pixel.Block.ShadeLight} ),
            new("Nav", new Frame { BackgroundRune = Pixel.Block.ShadeMedium }),
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
                (subHeader = new("SubHeader", new Frame { BackgroundRune = Pixel.Block.ShadeDark })),
                (subNav = new("SubNav", new Frame { BackgroundRune = Pixel.Shapes.CircleBorder })),
                new("SubContent", new Frame { BackgroundRune = Pixel.Shapes.CircleSolid })
            }
        };

        subHeader.Add(new LayoutNode(
            new Frame { Border = LineSet.Single }, 
            new FrameGuide{ Size = new SizeNumber(50.Percent(), 1)})
        );
        subNav.Add(new LayoutNode(
            new Frame { BackgroundRune = Pixel.Misc.StarBorder, Border = LineSet.Rounded },
            new FrameGuide { Size = new SizeNumber(100.Percent(), 50.Percent()) })
        );

        var subContent = root.Find("SubContent")!;
        subContent.Add(new LayoutNode(
            new Frame { BackgroundRune = Pixel.Misc.Diamond },
            new FrameGuide(Align.Start, Align.Start, size: new SizeNumber(10, 5))
        ));
        subContent.Add(new LayoutNode(
            new Frame { BackgroundRune = Pixel.Misc.Heart, Border = LineSet.Double },
            new FrameGuide(Align.Center, Align.Center, size: new SizeNumber(10, 5))
        ));
        subContent.Add(new LayoutNode(
            new Frame { BackgroundRune = Pixel.Misc.Clubs },
            new FrameGuide(Align.End, Align.End, size: new SizeNumber(10, 5))
        ));
        
        Draw(root);
        Assert.AreEqual("""
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ▒▒▒▒▒▒▒▒▒▒╶─────────────╴▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▒▒▒▒▒▒▒▒▒▒╭────────╮♢♢♢♢♢♢♢♢♢♢●●●●●●●●●●
                ▒▒▒▒▒▒▒▒▒▒│☆☆☆☆☆☆☆☆│♢♢♢♢♢♢♢♢♢♢●●●●●●●●●●
                ▒▒▒▒▒▒▒▒▒▒│☆☆☆☆☆☆☆☆│♢♢♢♢♢╔════════╗●●●●●
                ▒▒▒▒▒▒▒▒▒▒│☆☆☆☆☆☆☆☆│♢♢♢♢♢║♡♡♡♡♡♡♡♡║●●●●●
                ▒▒▒▒▒▒▒▒▒▒╰────────╯♢♢♢♢♢║♡♡♡♡♡♡♡♡║●●●●●
                ▒▒▒▒▒▒▒▒▒▒○○○○○○○○○○●●●●●║♡♡♡♡♣♣♣♣♣♣♣♣♣♣
                ▒▒▒▒▒▒▒▒▒▒○○○○○○○○○○●●●●●╚════♣♣♣♣♣♣♣♣♣♣
                ▒▒▒▒▒▒▒▒▒▒○○○○○○○○○○●●●●●●●●●●♣♣♣♣♣♣♣♣♣♣
                ▒▒▒▒▒▒▒▒▒▒○○○○○○○○○○●●●●●●●●●●♣♣♣♣♣♣♣♣♣♣
                ▒▒▒▒▒▒▒▒▒▒○○○○○○○○○○●●●●●●●●●●♣♣♣♣♣♣♣♣♣♣
                """,
            Canvas.ToString()
        );
    }
}