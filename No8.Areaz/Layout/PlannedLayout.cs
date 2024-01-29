using System.Collections;
using System.Runtime.InteropServices.JavaScript;

namespace No8.Areaz.Layout;

public class PlannedLayout
{
    public string Name { get; } = null!;
    //-- 
    public Align HorzAlign { get; set; } = Align.Start;
    public Align VertAlign { get; set; } = Align.Start;

    //-- Size
    public Number Width { get; private set; } = Number.Undefined;
    public Number Height { get; private set; } = Number.Undefined;
    public Number MinWidth { get; private set; } = Number.Undefined;
    public Number MaxWidth { get; private set; } = Number.Undefined;
    public Number MinHeight { get; private set; } = Number.Undefined;
    public Number MaxHeight { get; private set; } = Number.Undefined;

    public float? AspectRatio { get; set; }

    public SidesNumeric? Position { get; set; }
    public SidesAtomic? Border { get; private set; }
    public SidesNumeric?  Padding   { get; private set; }
    

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
        Number? width     = null,
        Number? height    = null,
        Number? minWidth  = null,
        Number? maxWidth  = null,
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
}