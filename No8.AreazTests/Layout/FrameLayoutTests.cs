using No8.Areaz;
using No8.Areaz.Layout;
using No8.Areaz.Painting;

namespace No8.AreazTests.Layout;

[TestFixture]
public class FrameLayoutTests : BaseLayoutTests
{
    [Test]
    public void Frame_Background()
    {
        var root = new LayoutNode(
            "Root",
            new Frame()
        );

        Draw(root);
        Assert.AreEqual("""
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
                """,
            Canvas.ToString()
        );
    }

    [Test]
    public void Frame_Border()
    {
        var root = new LayoutNode(
            "Root",
            new Frame { Border = LineSet.Rounded }
        );

        Draw(root);
        Assert.AreEqual("""
                ╭──────────────────────────────────────╮
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                ╰──────────────────────────────────────╯
                """,
            Canvas.ToString()
        );
    }

    [Test]
    public void Frame_OneChild()
    {
        var root = new LayoutNode(
            "Root",
            new Frame { Border = LineSet.Rounded }
        )
        {
            new(
                new Frame { BackgroundRune = Pixel.Misc.Cloud },
                new FrameGuide(size: new SizeNumber(50.Percent(), 50.Percent())))
        };

        Draw(root);
        Assert.AreEqual("""
                ☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁───────────────────╮
                ☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁░░░░░░░░░░░░░░░░░░░│
                ☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁░░░░░░░░░░░░░░░░░░░│
                ☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁░░░░░░░░░░░░░░░░░░░│
                ☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁░░░░░░░░░░░░░░░░░░░│
                ☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
                ╰──────────────────────────────────────╯
                """,
            Canvas.ToString()
        );
    }

    [Test]
    public void Frame_FourChild()
    {
        var root = new LayoutNode(
            "Root",
            new Frame { Border = LineSet.Rounded }
        )
        {
            new(
                new Frame { BackgroundRune = Pixel.Misc.Cloud, Border = LineSet.Single },
                new FrameGuide(size: new SizeNumber(50.Percent(), 50.Percent()))
            ),
            new(
                new Frame { BackgroundRune = Pixel.Misc.Happy, Border = LineSet.Double },
                new FrameGuide(alignHorz: Align.End, size: new SizeNumber(50.Percent(), 50.Percent()),
                    margin: SidesInt.One)
            ),
            new(
                new Frame { BackgroundRune = Pixel.Misc.Coffee, Border = LineSet.Double },
                new FrameGuide(alignVert: Align.End, size: new SizeNumber(50.Percent(), 50.Percent()),
                    margin: SidesInt.One)
            ),
            new(
                new Frame { BackgroundRune = Pixel.Misc.Sun, Border = LineSet.Single },
                new FrameGuide(alignHorz: Align.End, alignVert: Align.End,
                    size: new SizeNumber(50.Percent(), 50.Percent()))
            )
        };

        Draw(root);
        Assert.AreEqual("""
                ┌──────────────────┬───────────────────╮
                │☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁│░╔════════════════╗│
                │☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁│░║☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺║│
                │☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁│░║☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺║│
                │☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁☁│░╚════════════════╝│
                ├──────────────────┘░░░░░░░░░░░░░░░░░░░│
                │░░░░░░░░░░░░░░░░░░░┌──────────────────┤
                │╔════════════════╗░│☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀│
                │║☕☕☕☕☕☕☕☕☕☕☕☕☕☕☕☕║░│☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀│
                │║☕☕☕☕☕☕☕☕☕☕☕☕☕☕☕☕║░│☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀│
                │╚════════════════╝░│☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀☀│
                ╰───────────────────┴──────────────────┘
                """,
            Canvas.ToString()
        );
    }
}