namespace No8.Areaz.Layout;

public record SidesNumber()
{
    public static SidesNumber Zero => new(0 );
    public static SidesNumber One => new(1 );

    public static SidesNumber Create(Number value) => new(value);

    /// <summary>
    /// All sides have the same value
    /// </summary>
    public SidesNumber(Number value) : this(value, value, value, value) 
    { }

    public SidesNumber(Number horizontal, Number vertical) 
        : this (horizontal, vertical, horizontal, vertical)
    { }

    public SidesNumber(Number west, Number north, Number east, Number south) : this()
    {
        West = west;
        North = north;
        East = east;
        South = south;
    }

    public SidesNumber(SidesNumber other)
    {
        West = other.West;
        North = other.North;
        East = other.East;
        South = other.South;
    }

    public Number South { get; } = Number.Undefined;
    public Number East { get; } = Number.Undefined;
    public Number West { get; } = Number.Undefined;
    public Number North { get; } = Number.Undefined;

    /// <summary>
    ///     Return the Side value
    /// </summary>
    public Number this[Side side]
    {
        get
        {
            switch (side)
            {
                case Side.West: return West;
                case Side.North: return North;
                case Side.East: return East;
                case Side.South: return South;
                default:
                    throw new ArgumentException("Unsupported side", nameof(side));
            }
        }

    }

    public Number ComputedEdgeValue(Side side, Number? defaultValue = null)
    {
        if (this[side].HasValue())
            return this[side];

        return defaultValue ?? Number.Undefined;
    }

    public (float west, float north, float east, float south)
        Resolve(float containerWidth, float containerHeight)
    {
        return (
            West.Resolve(containerWidth),
            North.Resolve(containerHeight),
            East.Resolve(containerWidth),
            South.Resolve(containerHeight));
    }
    
    public bool IsZero =>
        West.IsZeroPoints() &&
        North.IsZeroPoints() &&
        East.IsZeroPoints() &&
        South.IsZeroPoints();

    public override string ToString()
    {
        if (West == East  && North == South && West == North)
            return $"(all:{West})";
        return $"(←:{West} ↑:{North} →:{East} ↓:{South})";
    }
    
    public static implicit operator SidesNumber(int value) => Create(value);
    public static implicit operator SidesNumber(Number value) => Create(value);
}