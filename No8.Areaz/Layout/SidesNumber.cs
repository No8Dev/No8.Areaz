using System.Drawing;

namespace No8.Areaz.Layout;

public record SidesNumber(
    Number Start, 
    Number Top, 
    Number End, 
    Number Bottom)
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

    public SidesNumber(SidesNumber other)
    {
        Start = other.Start;
        Top = other.Top;
        End = other.End;
        Bottom = other.Bottom;
    }

    public Number Bottom { get; } = Number.Undefined;
    public Number End { get; } = Number.Undefined;
    public Number Start { get; } = Number.Undefined;
    public Number Top { get; } = Number.Undefined;

    /// <summary>
    ///     Return the Side value
    /// </summary>
    public Number this[Side side]
    {
        get
        {
            switch (side)
            {
                case Side.Start: return Start;
                case Side.Top: return Top;
                case Side.End: return End;
                case Side.Bottom: return Bottom;
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

    public (float start, float top, float end, float bottom)
        Resolve(float containerWidth, float containerHeight)
    {
        return (
            Start.Resolve(containerWidth),
            Top.Resolve(containerHeight),
            End.Resolve(containerWidth),
            Bottom.Resolve(containerHeight));
    }
    
    public bool IsZero =>
        Start.IsZeroPoints() &&
        Top.IsZeroPoints() &&
        End.IsZeroPoints() &&
        Bottom.IsZeroPoints();

    public override string ToString()
    {
        if (Start == End  && Top == Bottom && Start == Top)
            return $"(all:{Start})";
        return $"(←:{Start} ↑:{Top} →:{End} ↓:{Bottom})";
    }
    
    public static implicit operator SidesNumber(int value) => Create(value);
    public static implicit operator SidesNumber(Number value) => Create(value);
}