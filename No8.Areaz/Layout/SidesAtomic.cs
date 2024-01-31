namespace No8.Areaz.Layout
{
    public record SidesAtomic
    {
        public static SidesAtomic Zero = new(0);
        public static SidesAtomic One = new(1);

        public int Start { get; }
        public int End { get; }
        public int Top { get; }
        public int Bottom { get; }

        private int ValidValue(int? value) => value is null or 0 ? 0 : 1;

        public SidesAtomic(int value)
        {
            Start = Top = End = Bottom = ValidValue(value);
        }

        public SidesAtomic(
            int? start = null,
            int? top = null,
            int? end = null,
            int? bottom = null,
            int? horizontal = null,
            int? vertical = null,
            int? all = null)
        {
            if (all is not null) Start = End = Top = Bottom = ValidValue(all);
            if (horizontal is not null) Start = End = ValidValue(horizontal);
            if (vertical is not null) Top = Bottom = ValidValue(vertical);
            if (start is not null) Start = ValidValue(start);
            if (end is not null) End = ValidValue(end);
            if (top is not null) Top = ValidValue(top);
            if (bottom is not null) Bottom = ValidValue(bottom);
        }

        /// <summary>
        ///     Return the Side value
        /// </summary>
        public int this[Side side]
        {
            get =>
                side switch
                {
                    Side.Start => Start,
                    Side.Top => Top,
                    Side.End => End,
                    Side.Bottom => Bottom,
                    _ => throw new ArgumentException("Unsupported side", nameof(side))
                };
        }

        public void Deconstruct(out int start, out int top, out int end, out int bottom)
        {
            start = Start;
            top = Top;
            end = End;
            bottom = Bottom;
        }
    }
}