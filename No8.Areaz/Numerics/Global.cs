using System.Runtime.CompilerServices;

namespace No8.Areaz;

public static class Global
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Points(this float value) => new (value, Number.UoM.Points);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Points(this int value) => new (value, Number.UoM.Points );
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Percent(this float value) => new (value, Number.UoM.Percent);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Number Percent(this int value) => new (value, Number.UoM.Percent);
    
    public static bool IsOneOf<T>(this T value, IEnumerable<T?> values)
    {
        foreach (var v in values)
        {
            if (v is IEquatable<T> eq)
                return eq.Equals(value);
                
            if (Equals(value, v))
                return true;
        }

        return false;
    }
    
    public static bool IsNullable(Type type) => Nullable.GetUnderlyingType(type) != null;
    public static bool IsNullable<T>() => Nullable.GetUnderlyingType(typeof(T)) != null;
    
    /// <summary>
    ///     Indicates that specified value type is the default value.
    /// </summary>
    public static bool IsDefault<T>(in T value)
    {
        if (value is object obj)
            return obj.Equals(default);

        return Comparer<T>.Default.Compare(value, default) == 0;
    }

    public static bool IsEmpty<T>(this List<T> list) => !list.Any();
    
    public static void ForEachIndex<T>(this List<T> list, Action<T,int> action)
    {
        for (int i = 0; i < list.Count; i++)
            action(list[i], i);
    }
}

internal static class Sentinel
{
    internal static readonly object Instance = new();
}
