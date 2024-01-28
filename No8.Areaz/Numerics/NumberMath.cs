using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace No8.Areaz;

public static class NumberMath
{
    public static string ToString(this float value)
    {
        return value.HasNoValue() ? "Undefined" : value.ToString(CultureInfo.InvariantCulture);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasNoValue(this float value) => float.IsNaN(value) || float.IsInfinity(value);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasNoValue(this double value) => double.IsNaN(value) || double.IsInfinity(value);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasNoValue(this float? value) => !value.HasValue || float.IsNaN(value.Value) || float.IsInfinity(value.Value);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasNoValue(this double? value) => !value.HasValue || double.IsNaN(value.Value) || double.IsInfinity(value.Value);
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasValue(this float value) => !float.IsNaN(value) && !float.IsInfinity(value);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasValue(this double value) => !double.IsNaN(value) || !double.IsInfinity(value);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasValue(this float? value) => value.HasValue && !float.IsNaN(value.Value) && !float.IsInfinity(value.Value);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasValue(this double? value) => value.HasValue && !double.IsNaN(value.Value) && !double.IsInfinity(value.Value);
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasValue(this float self, out float value)
    {
        value = self;
        return value.HasValue();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this float value) => MathF.Abs(value) < 0.0001f;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this double value) => Math.Abs(value) < 0.0001;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZero(this float value) => MathF.Abs(value) > 0.0001f;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZero(this double value) => Math.Abs(value) > 0.0001;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotOne(this float value) => Math.Abs(value - 1f) > 0.0001;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Is(this float value, float other) => FloatsEqual(value, other);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNot(this float value, float other) => !FloatsEqual(value, other);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Is(this double value, double other) => DoublesEqual(value, other);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNot(this double value, double other) => !DoublesEqual(value, other);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Within(this float value, float begin, float end) => value >= begin && value < end;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Within(this double value, double begin, double end) => value >= begin && value < end;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool FloatsEqual(float a, float b)
    {
        if (a.HasValue() && b.HasValue())
            return MathF.Abs(a - b) < 0.0001f;

        return HasNoValue(a) && HasNoValue(b);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DoublesEqual(double a, double b)
    {
        if (a.HasValue() && b.HasValue())
            return Math.Abs(a - b) < 0.0001f;

        return a.HasNoValue() && b.HasNoValue();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FloatMax(float a, float b)
    {
        if (a.HasValue() && b.HasValue())
            return MathF.Max(a, b);

        return a.HasValue() ? a : b;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DoubleMax(double a, double b)
    {
        if (a.HasValue() && b.HasValue())
            return Math.Max(a, b);

        return a.HasValue() ? a : b;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FloatMod(float x, float y) => MathF.IEEERemainder(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FloatMin(float a, float b)
    {
        if (a.HasValue() && b.HasValue())
            return MathF.Min(a, b);

        return a.HasValue() ? a : b;
    }

    public static float OrElse(this float value, float other) => value.HasValue() ? value : other;
    public static float OrElse(this float value, Func<float> func) => value.HasValue() ? value : func();

    public static float MaybeMin(this float value, float rhs)
    {
        if (rhs.HasNoValue()) return value;
        if (value.HasValue())
            return MathF.Min(value, rhs);
        return value;
    }
    public static float MaybeMax(this float value, float rhs)
    {
        if (rhs.HasNoValue()) return value;
        if (value.HasValue())
            return MathF.Max(value, rhs);
        return value;
    }

    public static float MaxNumber(this float value, float rhs)
    {
        if (value.HasValue() && rhs.HasValue())
            return MathF.Max(value, rhs);
        if (value.HasValue())
            return value;
        return rhs;
    }

    public static float MaxNumber(this IEnumerable<float> source)
    {
        float value = default;

        foreach (var x in source)
        {
            if (x.HasValue() && x > value)
                value = x;
        }

        return value;
    }

    public static float SumNumber(this IEnumerable<float> source)
    {
        var sum = 0f;
        foreach (var x in source)
        {
            if (x.HasValue())
                sum += x;
        }

        return sum;
    }

    public static int Clamp(this int value, int min, int max)
    {
        if (value < min) value = min;
        if (value > max) value = max;

        return value;
    }
    public static int Clamp(this int value, int? min, int? max)
    {
        if (min.HasValue && value < min) value = min.Value;
        if (max.HasValue && value > max) value = max.Value;

        return value;
    }

    public static float Clamp(this float value, float min, float max)
    {
        if (value.HasValue())
        {
            if (max.HasValue())
            {
                if (value > max)
                    return max;
            }
            if (min.HasValue())
            {
                if (value < min)
                    return min;
            }
        }

        return value;
    }

    public static double Clamp(this double value, double min, double max)
    {
        if (!double.IsNaN(value))
        {
            if (!double.IsNaN(max))
            {
                if (value > max)
                    return max;
            }
            if (!double.IsNaN(min))
            {
                if (value < min)
                    return min;
            }
        }

        return value;
    }

    public static Rectangle Offset(this Rectangle rect, int? x, int? y)
    {
        return new(rect.X + x ?? 0, rect.Y + y ?? 0, rect.Width, rect.Height);
    }
    
    public static RectangleF Scale(this Rectangle rect, float? scaleX, float? scaleY)
    {
        if (scaleX == null && scaleY == null)
            return new (rect.X, rect.Y, rect.Width, rect.Height);
        var width = rect.Width * (scaleX ?? 1);
        var height = rect.Height * (scaleY ?? 1);

        if (MathF.Abs(rect.Width - width) < 1 &&
            MathF.Abs(rect.Height - height) < 1)
            return new (rect.X, rect.Y, rect.Width, rect.Height);

        return new (
            rect.X + (MathF.Abs(rect.Width - width) / 2f),
            rect.Y + (MathF.Abs(rect.Height - height) / 2f),
            width, height);
    }

}

