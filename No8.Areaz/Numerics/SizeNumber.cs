using System.Diagnostics.CodeAnalysis;

namespace No8.Areaz;

public readonly struct SizeNumber : IEquatable<SizeNumber>
{
    public static readonly SizeNumber Zero = new (Number.Zero, Number.Zero);
    
    public SizeNumber(Number width, Number height)
    {
        Width = width;
        Height = height;
    }

    public Number Width { get; }
    public Number Height { get; }

    public bool Equals(SizeNumber other) => Width.Equals(other.Width) && Height.Equals(other.Height);
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is SizeNumber other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Width, Height);

    public static bool operator ==(SizeNumber left, SizeNumber right) => left.Equals(right);
    public static bool operator !=(SizeNumber left, SizeNumber right) => !left.Equals(right);

    public override string ToString() => $"({Width},{Height})";
}