namespace No8.Areaz.Layout;

/// <summary>
///     Values for Start, Top, End, Bottom edges
/// </summary>
public record SidesInt(int Start, int Top, int End, int Bottom)
{
    public static readonly SidesInt Zero = new(0);
    public static readonly SidesInt One = new(1);

    public SidesInt(int value) 
        : this(value,value, value, value)
    {
    }

    public SidesInt(int horizontal, int vertical) 
        : this(horizontal, vertical, horizontal, vertical)
    {
    }

    /// <summary>
    ///     Return the Side value
    /// </summary>
    public int this[Side side]
    {
        get =>
            side switch
            {
                Side.Start => Start,
                Side.Top => Top,
                Side.End => End,
                Side.Bottom => Bottom,
                _ => throw new ArgumentException("Unsupported side", nameof(side))
            };
    }

    public bool IsZero => Start == 0 &&
                           Top == 0 &&
                           End == 0 &&
                           Bottom == 0;

    public bool HasValue => !IsZero;

    public void Deconstruct(out int start, out int top, out int end, out int bottom)
    {
        start = Start;
        top = Top;
        end = End;
        bottom = Bottom;
    }
}