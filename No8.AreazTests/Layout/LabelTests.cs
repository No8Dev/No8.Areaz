using No8.Areaz.Figlets;
using No8.Areaz.Layout;

namespace No8.AreazTests.Layout;

[TestFixture]
public class LabelTests : BaseLayoutTests
{

    [Test]
    public void Label_Simple()
    {
        var root = new LayoutNode("Root", new Label("Allo, world."));
        
        Draw(root);
        Assert.AreEqual("""
                Allo, world.
                """,
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Label_MultiLine()
    {
        var root = new LayoutNode("Root", new Label("Allo," + Environment.NewLine + "world."));
        
        Draw(root);
        Assert.AreEqual("""
                Allo,
                world.
                """,
            Canvas.ToString()
        );
    }

    private const string LoremIpsum =
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    
    [Test]
    public void Label_LoremIpsum()
    {
        var root = new LayoutNode("Root", new Label(LoremIpsum), new CanvasGuide(Align.Stretch, Align.Stretch));
        
        Draw(root);
        
        // ReSharper disable StringLiteralTypo
        Assert.AreEqual(
            "Lorem ipsum dolor sit amet, consectetur",
            Canvas.ToString()
        );
        // ReSharper restore StringLiteralTypo
    }
    [Test]
    
    public void Label_LoremIpsum_Wrap()
    {
        var root = new LayoutNode(
            "Root", 
            new Label(LoremIpsum) { WrapLongLines = true }, 
            new CanvasGuide(Align.Stretch, Align.Stretch));
        
        Draw(root);
        // ReSharper disable StringLiteralTypo
        Assert.AreEqual("""
                Lorem ipsum dolor sit amet, consectetur
                adipiscing elit, sed do eiusmod tempor
                incididunt ut labore et dolore magna
                aliqua. Ut enim ad minim veniam, quis
                nostrud exercitation ullamco laboris
                nisi ut aliquip ex ea commodo consequat.
                Duis aute irure dolor in reprehenderit
                in voluptate velit esse cillum dolore eu
                fugiat nulla pariatur. Excepteur sint
                occaecat cupidatat non proident, sunt in
                culpa qui officia deserunt mollit anim
                id est laborum.
                """,
            // ReSharper restore StringLiteralTypo
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Label_Figlet()
    {
        var text = Figlet.Render("Yes", Figlet.Fonts.Rebel);
        
        var root = new LayoutNode(
            "Root", 
            new Label(text), 
            new CanvasGuide(Align.Stretch, Align.Stretch));
        
        Draw(root);
        // ReSharper disable StringLiteralTypo
        Assert.AreEqual("""
                 █████ █████
                ▒▒███ ▒▒███
                 ▒▒███ ███ ██████  █████
                  ▒▒█████ ███▒▒██████▒▒
                   ▒▒███ ▒███████▒▒█████
                    ▒███ ▒███▒▒▒  ▒▒▒▒███
                    █████▒▒██████ ██████
                   ▒▒▒▒▒  ▒▒▒▒▒▒ ▒▒▒▒▒▒
                """,
            // ReSharper restore StringLiteralTypo
            Canvas.ToString()
        );
    }
    
    [Test]
    public void Label_Figlet_Truncated()
    {
        var text = Figlet.Render("Allo, world.");
        
        var root = new LayoutNode(
            "Root", 
            new Label(text), 
            new CanvasGuide(Align.Stretch, Align.Stretch));
        
        Draw(root);
        // ReSharper disable StringLiteralTypo
        Assert.AreEqual("""
                    _    _ _
                   / \  | | | ___    __      _____  _ __
                  / _ \ | | |/ _ \   \ \ /\ / / _ \| '__
                 / ___ \| | | (_) |   \ V  V / (_) | |
                /_/   \_\_|_|\___( )   \_/\_/ \___/|_|
                                 |/
                """,
            // ReSharper restore StringLiteralTypo
            Canvas.ToString()
        );
    }

}