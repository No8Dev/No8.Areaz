namespace No8.Areaz.Layout;

/// <summary>
///     Values for West, North, East, South edges
/// </summary>
public record SidesInt(int West, int North, int East, int South)
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
                Side.West => West,
                Side.North => North,
                Side.East => East,
                Side.South => South,
                _ => throw new ArgumentException("Unsupported side", nameof(side))
            };
    }

    public bool IsZero => West == 0 &&
                           North == 0 &&
                           East == 0 &&
                           South == 0;

    public bool HasValue => !IsZero;

    public void Deconstruct(out int west, out int north, out int east, out int south)
    {
        west = West;
        north = North;
        east = East;
        south = South;
    }

    public override string ToString()
    {
        if (West == East  && North == South && West == North)
            return $"(:{West})";
        return $"(←:{West} ↑:{North} →:{East} ↓:{South})";
    }

}