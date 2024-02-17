using No8.Areaz;
using No8.Areaz.Layout;
using No8.Areaz.Painting;
using No8.AreazTests.Models;
using NUnit.Framework;

namespace No8.AreazTests.Layout;

[TestFixture]
public class GridLayoutTests : BaseLayoutTests
{
    [Test]
    public void Grid_Empty()
    {
        var root = new LayoutNode(
            "Root",
            new Grid { BackgroundRune = Pixel.Block.Lower4 }
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
    public void Grid_4x3()
    {
        Grid grid;
        var root = new LayoutNode(
            "Root",
            grid = new Grid()
            {
                new GridRowTemplate("Row1", 2),
                new GridRowTemplate("Row2", 100.Percent()),
                new GridRowTemplate("Row3", 3),

                new GridColTemplate("Col1", 25.Percent()),
                new GridColTemplate("Col2", 25.Percent()),
                new GridColTemplate("Col3", 25.Percent()),
                new GridColTemplate("Col4", 25.Percent()),
                
                new GridArea( "HeaderArea", 0, 0, ColSpan: 4),
                new GridArea("NavigationArea", 1, 0, RowSpan: 2 ),
                new GridArea("ContentArea", 1, 1, RowSpan: 2, ColSpan: 3)
            }
        )
        {
            new (
                "Header", 
                new TestControl { LineSet = LineSet.Rounded }, 
                new GridGuide(areaName:"HeaderArea")),
            new (
                "Nav", 
                new TestControl { Background = Pixel.Block.Lower1, LineSet = LineSet.Rounded }, 
                new GridGuide(areaName:"NavigationArea")),
            new (
                "Content", 
                new TestControl { Background = Pixel.Shapes.CircleBorder }, 
                new GridGuide(areaName:"ContentArea")),
        };
        
        grid.BackgroundRune = Pixel.Block.Lower4;
        
        Draw(root);
        Assert.AreEqual("""
                ╭[Header]──────────────────────────────╮
                ╰──────────────────────────────────────╯
                ╭[Nav]───╮┌[Content]───────────────────┐
                │▁▁▁▁▁▁▁▁││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │▁▁▁▁▁▁▁▁││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │▁▁▁▁▁▁▁▁││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │▁▁▁▁▁▁▁▁││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │▁▁▁▁▁▁▁▁││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │▁▁▁▁▁▁▁▁││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │▁▁▁▁▁▁▁▁││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                │▁▁▁▁▁▁▁▁││○○○○○○○○○○○○○○○○○○○○○○○○○○○○│
                ╰────────╯└────────────────────────────┘
                """,
            Canvas.ToString()
        );
    }

}