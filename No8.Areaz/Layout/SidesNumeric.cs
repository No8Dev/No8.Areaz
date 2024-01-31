using System.Security.AccessControl;

namespace No8.Areaz.Layout;

public record SidesNumeric
{
    public static SidesNumeric Zero => new(0 );
    public static SidesNumeric One => new(1 );

    public static SidesNumeric Create(Number value) => new(value);

    public static SidesNumeric Create(
        Number? start = null,
        Number? top = null,
        Number? end = null,
        Number? bottom = null,
        Number? horizontal = null,
        Number? vertical = null,
        Number? all = null) =>
        new(start, top, end, bottom, horizontal, vertical, all);

    /// <summary>
    /// All sides have the same value
    /// </summary>
    public SidesNumeric(Number value) { Start = Top = End = Bottom = value; }

    public SidesNumeric(Number? start = null,
        Number? top = null,
        Number? end = null,
        Number? bottom = null,
        Number? horizontal = null,
        Number? vertical = null,
        Number? all = null)
    {
        if (all != null) Start = End = Top = Bottom = all;
        if (horizontal != null) Start = End = horizontal;
        if (vertical != null) Top = Bottom = vertical;
        if (start != null) Start = start;
        if (end != null) End = end;
        if (top != null) Top = top;
        if (bottom != null) Bottom = bottom;
    }

    public SidesNumeric(SidesNumeric other)
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

    public override string ToString()
    {
        if (Start == End  && Top == Bottom && Start == Top)
            return $"(all:{Start})";
        return $"({Start},{Top},{End},{Bottom})";
    }

    public static implicit operator SidesNumeric(int value) => Create(value);
    public static implicit operator SidesNumeric(Number value) => Create(value);
}