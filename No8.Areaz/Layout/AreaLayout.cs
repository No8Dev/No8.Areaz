using System.Diagnostics;

namespace No8.Areaz.Layout;

public class AreaLayout
{
    public static void Compute(INode node, int width = 40, int height = 12)
    {
        new AreaLayout().Compute(node, width, height);
    }
    
    public void Compute(INode node, float containerWidth, float containerHeight)
    {
        ResolveDimensions(node);
        var (width, widthMeasureMode) =
            CalcMeasureAndMode(node.Placement.ResolvedWidth, node.Plan.MaxWidth, containerWidth);
        var (height, heightMeasureMode) =
            CalcMeasureAndMode(node.Placement.ResolvedHeight, node.Plan.MaxHeight, containerHeight);

        LayoutNode(
            node,
            width, widthMeasureMode,
            height, heightMeasureMode,
            containerWidth, containerHeight);
        SetPlacementPosition(node, width, height);
    }

    private void LayoutNode(
        INode node, 
        float availableWidth, MeasureMode widthMeasureMode, 
        float availableHeight, MeasureMode heightMeasureMode,
        float containerWidth, float containerHeight)
    {
        Debug.Assert(
            availableWidth.HasValue() || widthMeasureMode == MeasureMode.Undefined,
            "availableWidth is indefinite so widthMeasureMode must be MeasureMode.Undefined");
        Debug.Assert(
            availableHeight.HasValue() || heightMeasureMode == MeasureMode.Undefined,
            "availableHeight is indefinite so heightMeasureMode must be MeasureMode.Undefined");

        SetPlacementPadding(node, containerWidth, containerHeight);
        SetPlacementBorder(node);

        if (node.MeasureNode is not null)
        {
            SetMeasuredDimensions_MeasureFunc(
                node,
                availableWidth, widthMeasureMode,
                availableHeight, heightMeasureMode,
                containerWidth, containerHeight);
            return;
        }

        if (node.Children.Count == 0)
        {
            SetMeasuredDimensions_EmptyContainer(
                node,
                availableWidth, widthMeasureMode,
                availableHeight, heightMeasureMode,
                containerWidth, containerHeight);
            return;
        }

        if (SetMeasuredDimensions_FixedSize(
                node,
                availableWidth, widthMeasureMode,
                availableHeight, heightMeasureMode,
                containerWidth, containerHeight))
        {
            return;
        }

        node.Placement.HadOverflow = false;
    }

    private void ResolveDimensions(INode node)
    {
        if (node.Plan.MaxWidth.HasValue() == true && node.Plan.MaxWidth == node.Plan.MinWidth)
            node.Placement.ResolvedWidth = node.Plan.MaxWidth;
        else
            node.Placement.ResolvedWidth = node.Plan.Width;
        
        if (node.Plan.MaxHeight.HasValue() == true && node.Plan.MaxHeight == node.Plan.MinHeight)
            node.Placement.ResolvedHeight = node.Plan.MaxHeight;
        else
            node.Placement.ResolvedHeight = node.Plan.Height;
    }

    private void SetPlacementPosition(INode node, float width, float height)
    {
        float start = 0, end = width;
        float top = 0f, bottom = height;
        if (node.Plan.Position is not null)
        {
            var pos = node.Plan.Position!;
            if (pos.Start.HasValue())
                start = pos.Start.Resolve(width);
            if (pos.End.HasValue())
                end = pos.End.Resolve(width);
            if (pos.Top.HasValue())
                top = pos.Top.Resolve(height);
            if (pos.Bottom.HasValue())
                bottom = pos.Bottom.Resolve(height);
        }
        // TODO Round to pixel

        node.Placement.Position = new(start, top, end, bottom);
    }
    
    private void SetPlacementPadding(INode node, float width, float height)
    {
        float start = 0f, end = 0f;
        float top = 0f, bottom = 0f;

        if (node.Plan.Padding is not null)
        {
            var padding = node.Plan.Padding!;
            if (padding.Start.HasValue())
                start = padding.Start.Resolve(width);
            if (padding.End.HasValue())
                end = padding.End.Resolve(width);
            if (padding.Top.HasValue())
                top = padding.Top.Resolve(height);
            if (padding.Bottom.HasValue())
                bottom = padding.Bottom.Resolve(height);
        }

        node.Placement.Padding = new SidesNumeric(start, top, end, bottom);
    }    
    
    private void SetPlacementBorder(INode node)
    {
        node.Placement.Border = node.Plan.Border ?? SidesAtomic.Zero;
    }

   
    private (float value, MeasureMode measureMode) 
        CalcMeasureAndMode(Number? resolved, Number max, float container)
    {
        if (IsNumberDefined(resolved, container))
            return (resolved!.Resolve(container), MeasureMode.Exactly);

        if (max.Resolve(container).HasValue())
            return (max.Resolve(container), MeasureMode.AtMost);

        return (container, container.HasValue() ? MeasureMode.Exactly : MeasureMode.Undefined);
    }

    private bool IsNumberDefined(Number? number, float containerSize)
    {
        if (number == null)
            return false;
        
        return !(number.Unit == Number.UoM.Auto
                 || number.Unit == Number.UoM.Undefined
                 || number.HasPointValue() && number.Value < 0.0f
                 || number.HasPercentValue() && (number.Value < 0.0f || containerSize.HasNoValue()));
    }

    private void SetMeasuredDimensions_MeasureFunc(
        INode node,
        float availableWidth, MeasureMode widthMeasureMode, 
        float availableHeight, MeasureMode heightMeasureMode,
        float containerWidth, float containerHeight)
    {
        Debug.Assert(
            node.MeasureNode != null,
            "Expected node to have custom measure function");
        Debug.Assert(
            node.Placement.Border != null,
            "Expected node to have placement border set");
        Debug.Assert(
            node.Placement.Padding != null,
            "Expected node to have placement padding set");

        float paddingAndBorderRow =
            node.Placement.Border.Start +
            node.Placement.Padding.Start +
            node.Placement.Padding.End +
            node.Placement.Border.End;
        float paddingAndBorderCol =
            node.Placement.Border.Top +
            node.Placement.Padding.Top +
            node.Placement.Padding.Bottom + 
            node.Placement.Border.Bottom;

        var innerWidth = availableWidth.HasValue()
            ? availableWidth
            : FloatMax(0f, availableWidth - paddingAndBorderRow);
        var innerHeight = availableHeight.HasValue()
            ? availableHeight
            : FloatMax(0f, availableHeight - paddingAndBorderCol);
        
        if (widthMeasureMode == MeasureMode.Exactly &&
            heightMeasureMode == MeasureMode.Exactly)
        {
            // Don't bother sizing the text if both dimensions are already defined.
            node.Placement.MeasuredWidth =
                BoundValue(availableWidth, containerWidth, node.Plan.MinWidth, node.Plan.MaxWidth);
            node.Placement.MeasuredHeight =
                BoundValue(availableHeight, containerHeight, node.Plan.MinHeight, node.Plan.MaxHeight);
        }
        else
        {
            // Measure the text under the current constraints.
            var measuredSize =
                node.MeasureNode!.Invoke(
                    node, 
                    innerWidth, widthMeasureMode, 
                    innerHeight, heightMeasureMode);
            float width = widthMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost
                ? measuredSize.Width + paddingAndBorderRow
                : availableWidth;
            float height = heightMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost
                ? measuredSize.Height + paddingAndBorderCol
                : availableHeight;
            node.Placement.MeasuredWidth =
                BoundValue(width, containerWidth, node.Plan.MinWidth, node.Plan.MaxWidth);
            node.Placement.MeasuredHeight =
                BoundValue(height, containerHeight, node.Plan.MinHeight, node.Plan.MaxHeight);
        }
    }

    /// <summary>
    ///     For nodes with no elements, use the available values if they were provided,
    ///     or the minimum size as indicated by the padding and border sizes.
    /// </summary>
    private void SetMeasuredDimensions_EmptyContainer(
        INode node,
        float availableWidth, MeasureMode widthMeasureMode,
        float availableHeight, MeasureMode heightMeasureMode,
        float containerWidth, float containerHeight)
    {
        Debug.Assert(
            node.Placement.Border != null,
            "Expected node to have placement border set");
        Debug.Assert(
            node.Placement.Padding != null,
            "Expected node to have placement padding set");
        
        float paddingAndBorderRow =
            node.Placement.Border.Start +
            node.Placement.Padding.Start +
            node.Placement.Padding.End +
            node.Placement.Border.End;
        float paddingAndBorderCol =
            node.Placement.Border.Top +
            node.Placement.Padding.Top +
            node.Placement.Padding.Bottom + 
            node.Placement.Border.Bottom;
        
        float width = widthMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost
            ? paddingAndBorderRow
            : availableWidth;
        float height = heightMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost
            ? paddingAndBorderCol
            : availableHeight;
        node.Placement.MeasuredWidth =
            BoundValue(width, containerWidth, node.Plan.MinWidth, node.Plan.MaxWidth);
        node.Placement.MeasuredHeight =
            BoundValue(height, containerHeight, node.Plan.MinHeight, node.Plan.MaxHeight);
    }

    private bool SetMeasuredDimensions_FixedSize(
        INode node,
        float availableWidth, MeasureMode widthMeasureMode,
        float availableHeight, MeasureMode heightMeasureMode,
        float containerWidth, float containerHeight)
    {
        if ((widthMeasureMode == MeasureMode.Exactly && heightMeasureMode == MeasureMode.Exactly) ||
            (availableWidth.HasValue() && widthMeasureMode == MeasureMode.AtMost && availableWidth <= 0f) ||
            (availableHeight.HasValue() && heightMeasureMode == MeasureMode.AtMost && availableHeight >= 0f))
        {
            float width = widthMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost && availableWidth < 0f
                ? 0f
                : availableWidth;
            float height = heightMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost && availableHeight < 0f
                ? 0f
                : availableHeight;
            node.Placement.MeasuredWidth =
                BoundValue(width, containerWidth, node.Plan.MinWidth, node.Plan.MaxWidth);
            node.Placement.MeasuredHeight =
                BoundValue(height, containerHeight, node.Plan.MinHeight, node.Plan.MaxHeight);

            return true;
        }

        return false;
    }

    private float BoundValue(
        float value, float container,
        Number minSize, Number maxSize)
    {
        var min = minSize.Resolve(container);
        if (min.HasValue() && value < min)
            return min;
        
        var max = maxSize.Resolve(container);
        if (max.HasValue() && value > max)
            return max;

        return value;
    }
}