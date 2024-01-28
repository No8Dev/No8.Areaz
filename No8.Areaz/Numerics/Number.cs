using System.Numerics;

namespace No8.Areaz;

public record Number(float Value, Number.UoM Unit) : IAdditionOperators<Number,Number,Number>
{
    public const float ValueUndefined = float.NaN;
    public const float Zero = 0f;
    
    public static readonly Number Undefined = new (ValueUndefined, UoM.Undefined);
    public static readonly Number Auto = new(0f, UoM.Auto);


    public enum UoM {
        Undefined = 0,      // Default
        Auto,
        Points,
        Percent,
    }

    public Number() : this(ValueUndefined, UoM.Undefined) { }

    //public static Dimension operator -(Dimension left) => left with { Value = -left.Value };
    public static implicit operator Number(int i) => new(i, UoM.Points);
    public static implicit operator Number(short s) => new(s, UoM.Points);
    public static implicit operator Number(float f) => f.HasValue() ? new(f, UoM.Points) : Undefined;
    public static implicit operator Number(double d) => new((float)d, UoM.Points);
    public static implicit operator float(Number dim) => dim.IsDefined() ? dim.Value : ValueUndefined;
    
    public static Number Percent(float f) => new(f, UoM.Percent);
    public static Number Point(float f) => new(f, UoM.Points);
    
    
    
    
    internal bool IsDefined()
    {
        return Unit switch
        {
            UoM.Percent => true,
            UoM.Points => true,
            _ => false
        };
    }
    
    public bool HasValue() => Unit == UoM.Points && (!float.IsNaN(Value) && !float.IsInfinity(Value));
    public bool HasNoValue() => !HasValue();
    
    public bool HasValue(out float value)
    {
        value = Value;
        return value.HasValue();
    }

    public float MaybeMin(float other)
    {
        if (other.HasNoValue()) return Value;
        if (Value.HasValue())
            return MathF.Min(Value, other);
        return Value;
    }
    public float MaybeMax(float other)
    {
        if (other.HasNoValue()) return Value;
        if (Value.HasValue())
            return MathF.Max(Value, other);
        return Value;
    }

    public float MaxNumber(float other)
    {
        if (HasValue() && other.HasValue())
            return MathF.Max(Value, other);
        if (Value.HasValue())
            return Value;
        return other;
    }
    
    public static float Add(float number, float other)
    {
        if (number.HasValue() && other.HasValue())
            return number + other;
        
        return number.HasValue() ? number : Number.ValueUndefined;
    }

    public static float Subtract(float number, float other)
    {
        if (number.HasValue() && other.HasValue())
            return number - other;
        
        return number.HasValue() ? number : Number.ValueUndefined;

    }

    public static float Multiply(float number, float other)
    {
        if (number.HasValue() && other.HasValue())
            return number * other;
        
        return number.HasValue() ? number : Number.ValueUndefined;
    }

    public static float Divide(float number, float other)
    {
        if (number.HasValue() && other.HasValue())
            return number / other;
            
        return number.HasValue() ? number : Number.ValueUndefined;
    }
    
    
    internal float Resolve(float parentDim)
    {
        return Unit switch
        {
            Number.UoM.Points => Value,
            Number.UoM.Percent => Number.Multiply(parentDim, Value),
            _ => Number.ValueUndefined
        };
    }

    public float OrElse(float other) => HasValue() ? Value : other;
    

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
            case UoM.Auto:
                return "auto";
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
        if (left.IsDefined() && right.IsDefined() && left.Unit == right.Unit)
            return new Number(left.Value + right.Value, left.Unit);
        return Undefined;
    }
    
    public static Number operator -(Number left, Number right)
    {
        if (left.IsDefined() && right.IsDefined() && left.Unit == right.Unit)
            return left with { Value = left.Value - right.Value };
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
        return ValueUndefined;
    }
}