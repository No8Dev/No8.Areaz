using System.Drawing;
using No8.Areaz.Painting;

namespace No8.AreazTests.Canvas;


public class DrawRectTests
{
    private Areaz.Painting.Canvas _canvas = null!;

    [SetUp]
    public void Setup()
    {
        _canvas = new Areaz.Painting.Canvas(10, 5);
    }

    [Test]
    public void DrawRect_Single()
    {
        _canvas.DrawRectangle(new ( 0,1,10,4 ), LineSet.Single);

        var str = _canvas.ToString();
        var box = @"
┌────────┐
│        │
│        │
└────────┘";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_Double()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 4), LineSet.Double);

        var str = _canvas.ToString();
        var box = @"
╔════════╗
║        ║
║        ║
╚════════╝";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_Rounded()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 4), LineSet.Rounded);

        var str = _canvas.ToString();
        var box = @"
╭────────╮
│        │
│        │
╰────────╯";
        Assert.AreEqual(box, str);
    }



    [Test]
    public void DrawRect_DoubleUnder()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 4), LineSet.DoubleUnder);

        var str = _canvas.ToString();
        var box = @"
┌────────┐
│        │
│        │
╘════════╛";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_DoubleOver()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 4), LineSet.DoubleOver);

        var str = _canvas.ToString();
        var box = @"
╒════════╕
│        │
│        │
└────────┘";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_DoubleRaised()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 4), LineSet.DoubleRaised);

        var str = _canvas.ToString();
        var box = @"
┌────────╖
│        ║
│        ║
╘════════╝";
        Assert.AreEqual(box, str);
    }
    [Test]
    public void DrawRect_DoublePressed()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 4), LineSet.DoublePressed);

        var str = _canvas.ToString();
        var box = @"
╔════════╕
║        │
║        │
╙────────┘";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_BothHorizontal()
    {
        _canvas.DrawRectangle(new(0, 1, 5, 4), LineSet.Single);
        _canvas.DrawRectangle(new(5, 1, 5, 4), LineSet.Double);

        var str = _canvas.ToString();
        var box = @"
┌───┐╔═══╗
│   │║   ║
│   │║   ║
└───┘╚═══╝";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_BothVertical()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 2), LineSet.Single);
        _canvas.DrawRectangle(new(0, 3, 10, 2), LineSet.Double);

        var str = _canvas.ToString();
        var box = @"
┌────────┐
└────────┘
╔════════╗
╚════════╝";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_SingleCross()
    {
        _canvas.DrawRectangle(new(0, 1, 7, 3), LineSet.Single);
        _canvas.DrawRectangle(new(3, 2, 7, 3), LineSet.Rounded);

        var str = _canvas.ToString();
        var box = @"
┌─────┐
│  ╭──┼──╮
└──┼──┘  │
   ╰─────╯";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_DoubleCross()
    {
        _canvas.DrawRectangle(new(0, 1, 7, 3), LineSet.Double);
        _canvas.DrawRectangle(new(3, 2, 7, 3), LineSet.Double);

        var str = _canvas.ToString();
        var box = @"
╔═════╗
║  ╔══╬══╗
╚══╬══╝  ║
   ╚═════╝";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_BothCrossDoubleTop()
    {
        _canvas.DrawRectangle(new(0, 1, 7, 3), LineSet.Single);
        _canvas.DrawRectangle(new(3, 2, 7, 3), LineSet.Double);

        var str = _canvas.ToString();
        var box = @"
┌─────┐
│  ╔══╪══╗
└──╫──┘  ║
   ╚═════╝";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_BothCrossSingleTop()
    {
        _canvas.DrawRectangle(new(0, 1, 7, 3), LineSet.Double);
        _canvas.DrawRectangle(new(3, 2, 7, 3), LineSet.Single);

        var str = _canvas.ToString();
        var box = @"
╔═════╗
║  ┌──╫──┐
╚══╪══╝  │
   └─────┘";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_SingleSideBySide()
    {
        _canvas.DrawRectangle(new(0, 1, 5, 3), LineSet.Single);
        _canvas.DrawRectangle(new(4, 1, 6, 4), LineSet.Single);

        var str = _canvas.ToString();
        var box = @"
┌───┬────┐
│   │    │
└───┤    │
    └────┘";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_RoundedSideBySide()
    {
        _canvas.DrawRectangle(new(0, 1, 5, 3), LineSet.Rounded);
        _canvas.DrawRectangle(new(4, 1, 6, 4), LineSet.Rounded);

        var str = _canvas.ToString();
        var box = @"
╭───┬────╮
│   │    │
╰───┤    │
    ╰────╯";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_DoubleSideBySide()
    {
        _canvas.DrawRectangle(new(0, 1, 5, 3), LineSet.Double);
        _canvas.DrawRectangle(new(4, 1, 6, 4), LineSet.Double);

        var str = _canvas.ToString();
        var box = @"
╔═══╦════╗
║   ║    ║
╚═══╣    ║
    ╚════╝";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_BothSideBySideDoubleTop()
    {
        _canvas.DrawRectangle(new(0, 1, 5, 3), LineSet.Single);
        _canvas.DrawRectangle(new(4, 1, 6, 4), LineSet.Double);

        var str = _canvas.ToString();
        var box = @"
┌───╔════╗
│   ║    ║
└───╢    ║
    ╚════╝";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_BothSideBySideSingleTop()
    {
        _canvas.DrawRectangle(new(0, 1, 5, 3), LineSet.Double);
        _canvas.DrawRectangle(new(4, 1, 6, 4), LineSet.Single);

        var str = _canvas.ToString();
        var box = @"
╔═══┌────┐
║   │    │
╚═══╡    │
    └────┘";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_BothSingleInside()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 3), LineSet.Double);
        _canvas.DrawRectangle(new(2, 1, 5, 4), LineSet.Single);

        var str = _canvas.ToString();
        var box = @"
╔═┌───┐══╗
║ │   │  ║
╚═╪═══╪══╝
  └───┘";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_BothDoubleInside()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 3), LineSet.Single);
        _canvas.DrawRectangle(new(2, 1, 5, 4), LineSet.Double);

        var str = _canvas.ToString();
        var box = @"
┌─╔═══╗──┐
│ ║   ║  │
└─╫───╫──┘
  ╚═══╝";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawRect_BothRoundedDoubleInside()
    {
        _canvas.DrawRectangle(new(0, 1, 10, 3), LineSet.Rounded);
        _canvas.DrawRectangle(new(2, 1, 5,  4), LineSet.Double);

        var str = _canvas.ToString();
        var box = @"
╭─╔═══╗──╮
│ ║   ║  │
╰─╫───╫──╯
  ╚═══╝";
        Assert.AreEqual(box, str);
    }

}