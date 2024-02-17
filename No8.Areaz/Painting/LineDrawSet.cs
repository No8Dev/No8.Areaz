using No8.Areaz.Helpers;

namespace No8.Areaz.Painting;

internal class LineDrawSet
{
    //    ^
    //  <- ->    <West(8), ^North(4), >East(2), vSouth(1)
    //    v
    // ' '╥'╺'╔'╨'║'╚'╠'╸'╗'═'╦'╝'╣'╩'╬'
    //  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5
    //                0 lines
    public const byte IndexZero = 0b_0000;  // 0
    //                1 Line
    public const byte IndexWest = 0b_1000;  // 8
    public const byte IndexNorth = 0b_0100;   // 4
    public const byte IndexEast = 0b_0010; // 2
    public const byte IndexSouth = 0b_0001;   // 1
    //                2 lines - Horz + Vert
    public const byte IndexWestEast = 0b_1010; // 10
    public const byte IndexNorthSouth = 0b_0101; // 5
    //                2 Lines - Corner
    public const byte IndexNorthWest = 0b_1100;  // 12
    public const byte IndexNorthEast = 0b_0110; // 6
    public const byte IndexSouthEast = 0b_0011; // 3
    public const byte IndexSouthWest = 0b_1001;  // 9
    //                3 Lines - Intersection
    public const byte IndexWestNorthEast = 0b_1110; // 14
    public const byte IndexNorthEastSouth = 0b_0111;  // 7
    public const byte IndexWestSouthEast = 0b_1011; // 11
    public const byte IndexSouthWestNorth = 0b_1101;   // 13
    //                4 Lines - Cross
    public const byte IndexWestNorthEastSouth = 0b_1111; //  15

    /*
     Only need to combine lines when new top line is horz or vert.
     All other line type will overwrite.

     Under is Single, over is Double
      ┌ ┐ └ ┘ ─ │ ┼ ├ ┤ ┬ ┴
    ═ ╤ ╤ ╧ ╧ ═ ╪ ╪ ╪ ╪ ╤ ╧ 
    ║ ╟ ╢ ╟ ╢ ╫ ║ ╫ ╟ ╢ ╫ ╫ 

    Under is Double, over is Single
      ╔ ╗ ╚ ╝ ═ ║ ╬ ╠ ╣ ╦ ╩
    ─ ╥ ╥ ╨ ╨ ─ ╫ ╫ ╫ ╫ ╥ ╨       
    │ ╞ ╡ ╞ ╡ ╪ │ ╪ ╞ ╡ ╪ ╪  
    */

    private readonly List<Rune> _chars;

    public LineDrawSet(string chars)
    {
        if (chars == null || chars.Length != 16)
            throw new ArgumentException(nameof(chars));

        _chars = chars.ToRuneList();
    }

    public Rune Horz => _chars[IndexWestEast];
    public Rune HorzStart => _chars[IndexEast];
    public Rune HorzEnd => _chars[IndexWest];
    public Rune Vert => _chars[IndexNorthSouth];
    public Rune VertStart => _chars[IndexSouth];
    public Rune VertEnd => _chars[IndexNorth];
    public Rune TopLeft => _chars[IndexSouthEast];
    public Rune TopRight => _chars[IndexSouthWest];
    public Rune BotLeft => _chars[IndexNorthEast];
    public Rune BotRight => _chars[IndexNorthWest];
    public Rune LeftSide => _chars[IndexNorthEastSouth];
    public Rune Top => _chars[IndexWestSouthEast];
    public Rune RightSide => _chars[IndexSouthWestNorth];
    public Rune Bottom => _chars[IndexWestNorthEast];
    public Rune Cross => _chars[IndexWestNorthEastSouth];


    public bool Contains(Rune rune) => rune.IsValid() && _chars.Contains(rune);
    public bool IsStraightLine(Rune rune) => rune == _chars[IndexWestEast] || rune == _chars[IndexNorthSouth];
    public int IndexOf(Rune rune) => _chars.IndexOf(rune);

    public Rune Combine(Rune underChar, Rune overChar, bool isStart = false, bool isEnd = false)
    {
        var underIndex = _chars.IndexOf(underChar);
        var overIndex = _chars.IndexOf(overChar);

        if (isStart || isEnd)
        {
            if (isStart && overIndex == IndexWestEast)
                overIndex = IndexEast;
            else if (isEnd && overIndex == IndexWestEast)
                overIndex = IndexWest;
            if (isStart && overIndex == IndexNorthSouth)
                overIndex = IndexSouth;
            else if (isEnd && overIndex == IndexNorthSouth)
                overIndex = IndexNorth;
        }

        return _chars[(underIndex | overIndex) & 0b_1111];
    }

    public Rune this[int index] =>
        _chars[index & 0b_1111];
}