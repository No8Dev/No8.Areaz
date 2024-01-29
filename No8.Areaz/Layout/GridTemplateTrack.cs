namespace No8.Areaz.Layout
{
    public class GridTemplateTrack
    {
        public string? Name { get; set; }
        public Number? Size { get; set; }
        public Number? MinSize { get; set; }
        public Number? MaxSize { get; set; }

        public GridTemplateTrack(Number size, Number? minSize = null, Number? maxSize = null)
        {
            Size = size;
            MinSize = minSize;
            MaxSize = maxSize;
        }

        public GridTemplateTrack(string name, Number size, Number? minSize = null, Number? maxSize = null)
            : this(size, minSize, maxSize)
        {
            Name = name;
        }
    }
}