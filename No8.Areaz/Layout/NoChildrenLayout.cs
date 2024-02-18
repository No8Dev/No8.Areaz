namespace No8.Areaz.Layout
{
    public class NoChildrenLayout : ILayoutManager
    {
        public static readonly NoChildrenLayout Default = new NoChildrenLayout();
        
        public void MeasureIn(LayoutNode container, IReadOnlyList<LayoutNode> children) { }
        public void MeasureOut(LayoutNode container, IReadOnlyList<LayoutNode> children) { }
    }
}