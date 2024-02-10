using System.Drawing;
using System.Text;
using No8.Areaz.Painting;

namespace No8.AreazTests.Painting;


public class DrawThingsTests
{
    private Canvas _canvas = null!;

    [SetUp]
    public void Setup()
    {
        _canvas = new (40, 20);
    }


    [Test]
    public void DrawTriangle()
    {
        _canvas.DrawTriangle(1, 18, 10, 2, 39, 19, Pixel.Shapes.SquareSolid);

        var str = _canvas.ToString();
        var box = @"

          ■
         ■ ■■
         ■   ■■
        ■      ■
        ■       ■■
       ■          ■■
       ■            ■■
      ■               ■
      ■                ■■
     ■                   ■■
    ■                      ■
    ■                       ■■
   ■                          ■■
   ■                            ■■
  ■                               ■
  ■                                ■■
 ■■■■■■■■■■■■■■■■■■■                 ■■
                    ■■■■■■■■■■■■■■■■■■■■";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawFillTriangle()
    {
        _canvas.FillTriangle(1, 18, 10, 2, 39, 19, Pixel.Block.Solid);

        var str = _canvas.ToString();
        var box = @"

          █
         ████
         ██████
        ████████
        ██████████
       █████████████
       ███████████████
      █████████████████
     ████████████████████
     ██████████████████████
    ████████████████████████
    ██████████████████████████
   █████████████████████████████
   ███████████████████████████████
  █████████████████████████████████
  ███████████████████████████████████
 ██████████████████████████████████████
                    ████████████████████";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawCircle()
    {
        _canvas.CircleCompensateRatio = 1.0f;
        _canvas.DrawCircle(19, 9, 7, Pixel.Block.ShadeDark);

        var str = _canvas.ToString();
        var box = @"

                 ▓▓▓▓▓
               ▓▓     ▓▓
              ▓         ▓
             ▓           ▓
             ▓           ▓
            ▓             ▓
            ▓             ▓
            ▓             ▓
            ▓             ▓
            ▓             ▓
             ▓           ▓
             ▓           ▓
              ▓         ▓
               ▓▓     ▓▓
                 ▓▓▓▓▓";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawFillCircle()
    {
        _canvas.CircleCompensateRatio = 1.0f;
        _canvas.FillCircle(19, 9, 7, Pixel.Block.ShadeLight);
        _canvas.FillCircle(19, 9, 6, Pixel.Block.ShadeMedium);
        _canvas.FillCircle(19, 9, 4, Pixel.Block.ShadeDark);

        var str = _canvas.ToString();
        var box = @"

                 ░░░░░
               ░░▒▒▒▒▒░░
              ░░▒▒▒▒▒▒▒░░
             ░░▒▒▒▓▓▓▒▒▒░░
             ░▒▒▓▓▓▓▓▓▓▒▒░
            ░▒▒▒▓▓▓▓▓▓▓▒▒▒░
            ░▒▒▓▓▓▓▓▓▓▓▓▒▒░
            ░▒▒▓▓▓▓▓▓▓▓▓▒▒░
            ░▒▒▓▓▓▓▓▓▓▓▓▒▒░
            ░▒▒▒▓▓▓▓▓▓▓▒▒▒░
             ░▒▒▓▓▓▓▓▓▓▒▒░
             ░░▒▒▒▓▓▓▒▒▒░░
              ░░▒▒▒▒▒▒▒░░
               ░░▒▒▒▒▒░░
                 ░░░░░";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void DrawSprite()
    {
        _canvas.DrawSprite(0, 0, GetSprite());

        var str = _canvas.ToString();
        var box = @"╭────────╮
│╒══════╕│
││ ▓  ▓ ││
│└──────┘│
│▲▲▲▲▲▲▲ │
│ ▲▲▲▲▲  │
│  ▲▲▲   │
│   ▲    │
│        │
╰────────╯";
        Assert.AreEqual(box, str);
    }

    [Test]
    public void GenerateSprite()
    {
        _canvas.Fill(0, 0, 10, 10, (Rune)' ', Color.White, Color.Black);
        _canvas.DrawRectangle(0, 0, 9, 9, LineSet.Rounded, Color.GreenYellow);
        _canvas.FillTriangle(1, 4, 7, 4, 4, 7, Pixel.Shapes.TriangleSolidUp, Color.OrangeRed);
        _canvas.DrawRectangle(1, 1, 8, 3, LineSet.DoubleOver, Color.Purple);
        _canvas.SetGlyph(3, 2, new Glyph(Pixel.Block.ShadeDark, Color.DeepSkyBlue));
        _canvas.SetGlyph(6, 2, new Glyph(Pixel.Block.ShadeDark, Color.DeepSkyBlue));

        var sprite = _canvas.ExportSprite(0, 0, 10, 10);

        var str = _canvas.ToString();
        var box = @"╭────────╮
│╒══════╕│
││ ▓  ▓ ││
│└──────┘│
│▲▲▲▲▲▲▲ │
│ ▲▲▲▲▲  │
│  ▲▲▲   │
│   ▲    │
│        │
╰────────╯";
        Assert.AreEqual(box, str);

        str = sprite!.ToString();
        Assert.AreEqual(SpriteStr, str);
    }

    public static readonly string SpriteStr = @"10
10
╭────────╮
│╒══════╕│
││ ▓  ▓ ││
│└──────┘│
│▲▲▲▲▲▲▲ │
│ ▲▲▲▲▲  │
│  ▲▲▲   │
│   ▲    │
│        │
╰────────╯
FFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2F
FFADFF2FFF800080FF800080FF800080FF800080FF800080FF800080FF800080FF800080FFADFF2F
FFADFF2FFF800080FF800080FF00BFFFFF800080FF800080FF00BFFFFF800080FF800080FFADFF2F
FFADFF2FFF800080FF800080FF800080FF800080FF800080FF800080FF800080FF800080FFADFF2F
FFADFF2FFFFF4500FFFF4500FFFF4500FFFF4500FFFF4500FFFF4500FFFF4500FFADFF2FFFADFF2F
FFADFF2FFFADFF2FFFFF4500FFFF4500FFFF4500FFFF4500FFFF4500FFADFF2FFFADFF2FFFADFF2F
FFADFF2FFFADFF2FFFADFF2FFFFF4500FFFF4500FFFF4500FFADFF2FFFADFF2FFFADFF2FFFADFF2F
FFADFF2FFFADFF2FFFADFF2FFFADFF2FFFFF4500FFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2F
FFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2F
FFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2FFFADFF2F
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
";
    public static Sprite GetSprite()
    {
        return new Sprite(SpriteStr);
    }
}