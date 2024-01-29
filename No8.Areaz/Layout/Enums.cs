namespace No8.Areaz.Layout;

public enum Side
{
    Top,
    Bottom,
    Start,
    End,
    Horizontal,
    Vertical,
    All
}

public enum PositionType
{
    /// <summary>
    ///     Relative to parent
    /// </summary>
    Relative,
    
    /// <summary>
    ///     Absolute position in canvas
    /// </summary>
    Absolute
}

public enum Align
{
    Start, End, Center, Stretch
}

public enum MeasureMode
{
    Undefined = -1,
    Exactly   = 0,
    AtMost    = 1
}
