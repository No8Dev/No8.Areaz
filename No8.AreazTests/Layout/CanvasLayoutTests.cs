using System.Text;
using No8.Areaz;
using No8.Areaz.Helpers;
using No8.Areaz.Layout;
using No8.Areaz.Painting;
using No8.AreazTests.Models;

namespace No8.AreazTests.Layout;

public class CanvasLayoutTests : BaseLayoutTests
{
    [Test]
    public void CanvasNode_Background()
    {
        var root = new TreeNode(
            new CanvasNode { Name = "Root", BackgroundRune = Pixel.Block.QuadrantULeftLRight }
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
        var root = new TreeNode(new CanvasNode("Root") { BackgroundRune = Pixel.Block.QuadrantULeftLRight})
        {
            (   new CanvasNode { Name="Child", BackgroundRune = Pixel.Block.ShadeLight}, 
                new CanvasLayout.Instructions(
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
        var root = new TreeNode(new CanvasNode("Root") { BackgroundRune = Pixel.Block.Solid})
        {
            new TreeNode(
                new CanvasNode { Name="Child", BackgroundRune = Pixel.Block.ShadeDark, SizeRequested = new SizeNumber(30, 10)}, 
                new CanvasLayout.Instructions(Align.Center, Align.Center)
            )
            {
                new TreeNode
                (
                    new CanvasNode { Name="Sub-Child", BackgroundRune = Pixel.Block.ShadeMedium, SizeRequested = new SizeNumber(20, 8)},
                    new CanvasLayout.Instructions(Align.Center, Align.Center)
                )
                {
                    new TreeNode
                    (   
                        new CanvasNode { Name="Sub-Sub-Child", BackgroundRune = Pixel.Block.ShadeLight, SizeRequested = new SizeNumber(10, 6)},
                        new CanvasLayout.Instructions(Align.Center, Align.Center)
                    )
                    {
                        new TreeNode(
                            new CanvasNode{ Name = "Center", BackgroundRune = (Rune)' '},
                            new CanvasLayout.Instructions(Align.Center, Align.Center, new SizeNumber(4, 4))
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
        var root = new TreeNode(new CanvasNode("Root") { BackgroundRune = Pixel.Block.Solid})
        {
            new TreeNode(
                new CanvasNode { Name="Child", BackgroundRune = Pixel.Block.ShadeDark}, 
                new CanvasLayout.Instructions(margin:SidesInt.One)
            )
            {
                new TreeNode
                (
                    new CanvasNode { Name="Sub-Child", BackgroundRune = Pixel.Block.ShadeMedium},
                    new CanvasLayout.Instructions(margin:SidesInt.One)
                )
                {
                    new TreeNode
                    (   
                        new CanvasNode { Name="Sub-Sub-Child", BackgroundRune = Pixel.Block.ShadeLight},
                        new CanvasLayout.Instructions(margin:SidesInt.One)
                    )
                    {
                        new TreeNode(
                            new CanvasNode{ Name = "Center", BackgroundRune = (Rune)' '},
                            new CanvasLayout.Instructions(margin:SidesInt.One)
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