using System.Text;
using No8.Areaz;
using No8.Areaz.Layout;
using No8.Areaz.Painting;

namespace No8.AreazTests.Layout;

public class CanvasLayoutTests : BaseLayoutTests
{
    [Test]
    public void CanvasNode_Background()
    {
        var root = new LayoutNode(
            "Root",
            new CanvasControl { BackgroundRune = Pixel.Block.QuadrantULeftLRight }
        );
        
        Draw(root);
        Assert.AreEqual("""
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void CanvasNode_Child_Background()
    {
        var root = new LayoutNode(
            "Root", 
            new CanvasControl { BackgroundRune = Pixel.Block.QuadrantULeftLRight})
        {
            new LayoutNode(
                "Child",
                new CanvasControl { BackgroundRune = Pixel.Block.ShadeLight}, 
                new CanvasGuide(
                    Align.Center, 
                    Align.Center,
                    new SizeNumber(50.Percent(), 50.Percent())))
        };
        
        Draw(root);
        Assert.AreEqual("""
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚░░░░░░░░░░░░░░░░░░░░▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚░░░░░░░░░░░░░░░░░░░░▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚░░░░░░░░░░░░░░░░░░░░▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚░░░░░░░░░░░░░░░░░░░░▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚░░░░░░░░░░░░░░░░░░░░▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚░░░░░░░░░░░░░░░░░░░░▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                ▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚▚
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void CanvasNode_Child_SizeRequested()
    {
        var root = new LayoutNode("Root", new CanvasControl { BackgroundRune = Pixel.Block.Solid})
        {
            new LayoutNode(
                "Dark",
                new CanvasControl { BackgroundRune = Pixel.Block.ShadeDark}, 
                new CanvasGuide(Align.Center, Align.Center, size:new(30,10))
            )
            {
                new LayoutNode
                (
                    "Medium",
                    new CanvasControl { BackgroundRune = Pixel.Block.ShadeMedium},
                    new CanvasGuide(Align.Center, Align.Center, new(20,8))
                )
                {
                    new LayoutNode
                    (   
                        "Light",
                        new CanvasControl { BackgroundRune = Pixel.Block.ShadeLight},
                        new CanvasGuide(Align.Center, Align.Center, new(10,6))
                    )
                    {
                        new LayoutNode(
                            "Empty",
                            new CanvasControl{ BackgroundRune = (Rune)' '},
                            new CanvasGuide(Align.Center, Align.Center, new SizeNumber(4, 4))
                        )
                    }
                }
            }
        };
        
        Draw(root);
        Assert.AreEqual("""
                ████████████████████████████████████████
                █████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█████
                █████▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓█████
                █████▓▓▓▓▓▒▒▒▒▒░░░░░░░░░░▒▒▒▒▒▓▓▓▓▓█████
                █████▓▓▓▓▓▒▒▒▒▒░░░    ░░░▒▒▒▒▒▓▓▓▓▓█████
                █████▓▓▓▓▓▒▒▒▒▒░░░    ░░░▒▒▒▒▒▓▓▓▓▓█████
                █████▓▓▓▓▓▒▒▒▒▒░░░    ░░░▒▒▒▒▒▓▓▓▓▓█████
                █████▓▓▓▓▓▒▒▒▒▒░░░    ░░░▒▒▒▒▒▓▓▓▓▓█████
                █████▓▓▓▓▓▒▒▒▒▒░░░░░░░░░░▒▒▒▒▒▓▓▓▓▓█████
                █████▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓█████
                █████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█████
                ████████████████████████████████████████
                """,
            Canvas.ToString()
        );
    }
    
        [Test]
    public void CanvasNode_Child_Margin()
    {
        var root = new LayoutNode(
            "Root",
            new CanvasControl { BackgroundRune = Pixel.Block.Solid})
        {
            new LayoutNode(
                "Dark",
                new CanvasControl { BackgroundRune = Pixel.Block.ShadeDark}, 
                new CanvasGuide(margin:SidesInt.One)
            )
            {
                new LayoutNode
                (
                    "Medium",
                    new CanvasControl { BackgroundRune = Pixel.Block.ShadeMedium},
                    new CanvasGuide(margin:SidesInt.One)
                )
                {
                    new LayoutNode
                    (   
                        "Light",
                        new CanvasControl { BackgroundRune = Pixel.Block.ShadeLight},
                        new CanvasGuide(margin:SidesInt.One)
                    )
                    {
                        new LayoutNode(
                            "Space",
                            new CanvasControl{ BackgroundRune = (Rune)' '},
                            new CanvasGuide(margin:SidesInt.One)
                        )
                    }
                }
            }
        };
        
        Draw(root);
        Assert.AreEqual("""
                ████████████████████████████████████████
                █▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█
                █▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓█
                █▓▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓█
                █▓▒░                                ░▒▓█
                █▓▒░                                ░▒▓█
                █▓▒░                                ░▒▓█
                █▓▒░                                ░▒▓█
                █▓▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓█
                █▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓█
                █▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█
                ████████████████████████████████████████
                """,
            Canvas.ToString()
        );
    }

}