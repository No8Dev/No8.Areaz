using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using No8.Areaz.Layout;

namespace No8.Areaz;

public record Number(float Value, Number.UoM Unit) : IAdditionOperators<Number,Number,Number>
{
    public const float ValueUndefined = float.NaN;
    public const float Zero = 0f;
    
    public static readonly Number Undefined = new (ValueUndefined, UoM.Undefined);


    public enum UoM {
        Undefined = 0,      // Default
        Points,
        Percent,
    }

    public Number() : this(ValueUndefined, UoM.Undefined) { }

    public static implicit operator Number(int i) => new(i, UoM.Points);
    public static implicit operator Number(short s) => new(s, UoM.Points);
    public static implicit operator Number(float f) => f.HasValue() ? new(f, UoM.Points) : Undefined;
    public static implicit operator Number(double d) => new((float)d, UoM.Points);
    public static implicit operator float(Number dim) => dim.HasValue() ? dim.Value : ValueUndefined;

    public bool IsPoints => Unit == UoM.Points;
    public bool IsPercent => Unit == UoM.Percent;

    internal bool HasValue()
    {
        return Unit switch
        {
            UoM.Percent => Value.HasValue(),
            UoM.Points => Value.HasValue(),
            _ => false
        };
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasPointValue() => Unit == UoM.Points && Value.HasValue();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasPercentValue() => Unit == UoM.Percent && Value.HasValue();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasNoPointValue() => Unit != UoM.Points || Value.HasNoValue();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZeroPoints() => Unit == UoM.Points && Value == 0f;
    
    
    public bool HasPointValue(out float value)
    {
        value = Value;
        return HasPointValue();
    }

    public float Min(float other)
    {
        if (IsPoints)
        {
            if (Value.HasValue() && other.HasValue())
                return MathF.Min(Value, other);
            if (Value.HasValue())
                return Value;
        }
        return other;
    }
    public float Max(float other)
    {
        if (IsPoints)
        {
            if (Value.HasValue() && other.HasValue())
                return MathF.Max(Value, other);
            if (Value.HasValue())
                return Value;
        }

        return other;
    }

    public static float Add(float number, float other)
    {
        if (number.HasValue() && other.HasValue())
            return number + other;
        
        return number.HasValue() ? number : ValueUndefined;
    }

    public static float Subtract(float number, float other)
    {
        if (number.HasValue() && other.HasValue())
            return number - other;
        
        return number.HasValue() ? number : ValueUndefined;

    }

    public static float Multiply(float number, float other)
    {
        if (number.HasValue() && other.HasValue())
            return number * other;
        
        return number.HasValue() ? number : ValueUndefined;
    }

    public static float Divide(float number, float other)
    {
        if (number.HasValue() && other.HasValue())
            return number / other;
            
        return number.HasValue() ? number : ValueUndefined;
    }
    
    
    internal float Resolve(float parentDim)
    {
        return Unit switch
        {
            UoM.Points => Value,
            UoM.Percent => Multiply(parentDim, Value),
            _ => ValueUndefined
        };
    }

    internal int ResolveToInt(float parentDim)
    {
        return Unit switch
        {
            UoM.Points => (int)Value,
            UoM.Percent => (int)Multiply(parentDim, Value),
            _ => 0
        };
    }

    public float OrElse(float other) => HasPointValue() ? Value : other;
    

    public virtual bool Equals(Number? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return
            Unit.Equals(other.Unit) &&
            NumberMath.FloatsEqual(Value, other.Value);
    }

    public override int GetHashCode() => HashCode.Combine(Value, (int)Unit);

    public override string ToString()
    {
        switch (Unit)
        {
            case UoM.Points:
                return $"{Value}";
            case UoM.Percent:
                return $"{Value}%";
            default:
                return "-";                    
        }
    }

    public static Number operator +(Number left, Number right)
    {
        if (left.HasPointValue() && right.HasPointValue())
            return left.Value + right.Value;
        return Undefined;
    }
    
    public static Number operator -(Number left, Number right)
    {
        if (left.HasPointValue() && right.HasPointValue())
            return left.Value - right.Value;
        return Undefined;
    }

    public static float AddNumber(Number left, Number right, float parent)
    {
        var leftVal = left.Resolve(parent);
        var rightVal = right.Resolve(parent);

        if (leftVal.HasValue() && rightVal.HasValue())
            return leftVal + rightVal;
        if (leftVal.HasValue())
            return leftVal;
        return rightVal;
    }
}