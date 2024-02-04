namespace No8.Areaz.Layout;

// ReSharper disable once InconsistentNaming
/// <summary>
///     XY location relative to parent node 
/// </summary>
public record XY(int X, int Y)
{
    public static readonly XY Zero = new(0, 0);
}