namespace No8.Areaz.Layout;

public class GridTemplateTrack
{
    public string? Name { get; set; }
    public Number? Size { get; set; }
    public Number? MinSize { get; set; }
    public Number? MaxSize { get; set; }

    protected GridTemplateTrack(Number size, Number? minSize = null, Number? maxSize = null)
    {
        Size = size;
        MinSize = minSize;
        MaxSize = maxSize;
    }

    protected GridTemplateTrack(string name, Number size, Number? minSize = null, Number? maxSize = null)
        : this(size, minSize, maxSize)
    {
        Name = name;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"[{(Name ?? "")}");
        if (Size is not null) sb.Append($",{Size}");
        if (MinSize is not null) sb.Append($",min:{MinSize}");
        if (MaxSize is not null) sb.Append($",max:{MaxSize}");
        sb.Append("]");
        return sb.ToString();
    }
}

public class GridRowTemplate : GridTemplateTrack
{
    public GridRowTemplate(Number size, Number? minSize = null, Number? maxSize = null) 
        : base(size, minSize, maxSize) { }
    public GridRowTemplate(string name, Number size, Number? minSize = null, Number? maxSize = null) 
        : base(name, size, minSize, maxSize) { }
}

public class GridColTemplate : GridTemplateTrack
{
    public GridColTemplate(Number size, Number? minSize = null, Number? maxSize = null) 
        : base(size, minSize, maxSize) { }
    public GridColTemplate(string name, Number size, Number? minSize = null, Number? maxSize = null) 
        : base(name, size, minSize, maxSize) { }
}