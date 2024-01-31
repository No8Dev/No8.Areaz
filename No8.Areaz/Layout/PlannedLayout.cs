using System.Collections;
using System.Runtime.InteropServices.JavaScript;

namespace No8.Areaz.Layout;

public class PlannedLayout
{
    //-- 
    public Align HorzAlign { get; set; } = Align.Start;
    public Align VertAlign { get; set; } = Align.Start;

    //-- Size
    public Number Width { get; set; } = Number.Undefined;
    public Number Height { get; set; } = Number.Undefined;
    public Number MinWidth { get; set; } = Number.Undefined;
    public Number MaxWidth { get; set; } = Number.Undefined;
    public Number MinHeight { get; set; } = Number.Undefined;
    public Number MaxHeight { get; set; } = Number.Undefined;

    public float? AspectRatio { get; set; }

    public SidesNumeric? Position { get; set; }
    public SidesAtomic? Border { get; set; }
    public SidesNumeric? Padding { get; set; }


    //********************************************************************

    public PlannedLayout SetAlign(
        Align horzAlign = Align.Start,
        Align vertAlign = Align.Start)
    {
        HorzAlign = horzAlign;
        VertAlign = vertAlign;
        return this;
    }

    public PlannedLayout SetSize(
        Number? width = null,
        Number? height = null,
        Number? minWidth = null,
        Number? maxWidth = null,
        Number? minHeight = null,
        Number? maxHeight = null)
    {
        if (width is not null) Width = width;
        if (height is not null) Height = height;
        if (minWidth is not null) MinWidth = minWidth;
        if (maxWidth is not null) MaxWidth = maxWidth;
        if (minHeight is not null) MinHeight = minHeight;
        if (maxHeight is not null) MaxHeight = maxHeight;
        return this;
    }

    public PlannedLayout SetPadding(SidesNumeric padding)
    {
        Padding = padding;
        return this;
    }

    public PlannedLayout SetBorder(SidesAtomic border)
    {
        Border = border;
        return this;
    }

    public PlannedLayout SetBorder(int value)
    {
        Border = new(value == 0 ? 0 : 1);
        return this;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        AppendProperty(sb, nameof(HorzAlign), HorzAlign, Align.Start);
        AppendProperty(sb, nameof(VertAlign), VertAlign, Align.Start);
        AppendProperty(sb, nameof(Width), Width, Number.Undefined);
        AppendProperty(sb, nameof(MinWidth), MinWidth, Number.Undefined);
        AppendProperty(sb, nameof(MaxWidth), MaxWidth, Number.Undefined);
        AppendProperty(sb, nameof(Height), Height, Number.Undefined);
        AppendProperty(sb, nameof(MinHeight), MinHeight, Number.Undefined);
        AppendProperty(sb, nameof(MaxHeight), MaxHeight, Number.Undefined);

        if (AspectRatio is not null)
            AppendProperty(sb, nameof(AspectRatio), AspectRatio.Value, 0f);

        AppendProperty(sb, nameof(Position), Position, default);
        AppendProperty(sb, nameof(Border), Border, default);
        AppendProperty(sb, nameof(Padding), Padding, default);

        return sb.ToString();
    }

    protected virtual void AppendProperty<T>(StringBuilder sb, string? name, T? value, T defaultValue)
        where T : class?
    {
        if (value != null && !value.Equals(defaultValue))
            sb.Append($" {name}={value}");
    }

    protected virtual void AppendProperty<T>(StringBuilder sb, string? name, T? value, T defaultValue)
        where T : struct
    {
        if (value != null && !value.Value.Equals(defaultValue))
            sb.Append($" {name}={value}");
    }

    protected virtual void AppendProperty(StringBuilder sb, string? name, float? value, float defaultValue)
    {
        if (value != null && value.HasValue() && !value.Value.Is(defaultValue))
            sb.Append($" {name}={value}");
    }

    protected virtual void AppendProperty(StringBuilder sb, string? name, Number? value, Number defaultValue)
    {
        if (value != null && value.HasValue() && value.Value != defaultValue)
            sb.Append($" {name}={value}");
    }
}