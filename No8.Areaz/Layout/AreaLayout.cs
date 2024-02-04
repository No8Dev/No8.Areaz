/*
using System.Diagnostics;

namespace No8.Areaz.Layout;

public class AreaLayout
{
    public static void Compute(NodeWithLayout nl, int width = 40, int height = 12)
    {
        new AreaLayout().Compute(nl, width, height);
    }
    
    public void Compute(NodeWithLayout nl, float containerWidth, float containerHeight)
    {
        ResolveDimensions(nl, containerWidth, containerHeight);

        LayoutNode(
            nl,
            containerWidth, containerHeight,
            performLayout: true);
    }

    private void LayoutNode(
        NodeWithLayout nl, 
        float containerWidth, float containerHeight,
        bool performLayout)
    {
        Debug.Assert(
            nl.WidthAvailable.HasValue() || nl.WidthMeasureMode == MeasureMode.Undefined,
            "WidthAvailable is indefinite so WidthMeasureMode must be MeasureMode.Undefined");
        Debug.Assert(
            nl.HeightAvailable.HasValue() || nl.HeightMeasureMode == MeasureMode.Undefined,
            "HeightAvailable is indefinite so HeightMeasureMode must be MeasureMode.Undefined");

        var isMeasured = SetMeasuredDimensions(
            nl,
            containerWidth, containerHeight,
            performLayout);
        if (isMeasured)     // if node has a measured size, then no need to further calculate algorithm
            return;

        nl.Node.Placement.HadOverflow = false;
        
        // STEP 1: CALCULATE VALUES FOR REMAINDER OF ALGORITHM
        var minWidth = nl.Plan.MinWidth.Resolve(containerWidth);
        var maxWidth = nl.Plan.MaxWidth.Resolve(containerWidth);
        var minHeight = nl.Plan.MinHeight.Resolve(containerHeight);
        var maxHeight = nl.Plan.MaxHeight.Resolve(containerHeight);
        
        // STEP 2: DETERMINE AVAILABLE SIZE IN LAYOUT-DIRECTION AND CROSS-DIRECTION
        
        // STEP 3: DETERMINE LAYOUT-DIRECTION LENGTH FOR EACH ITEM
        // STEP 4: COLLECT FLEX ITEMS INTO FLEX LINES
        // STEP 5: RESOLVING FLEXIBLE LENGTHS ON LAYOUT-DIRECTION DIRECTION
        // STEP 6: LAYOUT-DIRECTION JUSTIFICATION & CROSS-DIRECTION SIZE DETERMINATION
        // STEP 7: CROSS-DIRECTION ALIGNMENT
        // STEP 8: MULTI-LINE CONTENT ALIGNMENT
        // STEP 9: COMPUTING FINAL DIMENSIONS
        // STEP 10: SIZING AND POSITIONING ABSOLUTE ELEMENTS
    }

    private void ResolveDimensions(NodeWithLayout nl, float containerWidth, float containerHeight)
    {
        if (nl.Plan.MaxWidth.HasValue() && nl.Plan.MaxWidth == nl.Plan.MinWidth)
            nl.WidthResolved = nl.Plan.MaxWidth;
        else
            nl.WidthResolved = nl.Plan.Width;

        if (nl.Plan.MaxHeight.HasValue() && nl.Plan.MaxHeight == nl.Plan.MinHeight)
            nl.HeightResolved = nl.Plan.MaxHeight;
        else
            nl.HeightResolved = nl.Plan.Height;
        
        (nl.WidthAvailable, nl.WidthMeasureMode) = CalcMeasureAndMode(nl.WidthResolved, nl.Plan.MaxWidth, containerWidth);
        (nl.HeightAvailable, nl.HeightMeasureMode) = CalcMeasureAndMode(nl.HeightResolved, nl.Plan.MaxHeight, containerHeight);

        SetPlacementBounds(nl, containerWidth, containerHeight);
        SetPlacementPadding(nl, containerWidth, containerHeight);
        SetPlacementBorder(nl);
    }

    private void SetPlacementBounds(NodeWithLayout nl, float containerWidth, float containerHeight)
    {
        int start = 0, end = (int)containerWidth;
        int top = 0, bottom = (int)containerHeight;
        
        if (nl.Plan.Position is not null)
        {
            var pos = nl.Plan.Position!;
            if (pos.Start.HasValue())
                start = (int)pos.Start.Resolve(containerWidth);
            if (pos.End.HasValue())
                end = (int)pos.End.Resolve(containerWidth);
            if (pos.Top.HasValue())
                top = (int)pos.Top.Resolve(containerHeight);
            if (pos.Bottom.HasValue())
                bottom = (int)pos.Bottom.Resolve(containerHeight);
        }
        
        nl.Node.Placement.Bounds = new(start, top, end, bottom);    
    }
    
    private void SetPlacementPadding(NodeWithLayout nl, float containerWidth, float containerHeight)
    {
        if (nl.Plan.Padding is not null)
        {
            int start = 0, end = 0;
            int top = 0, bottom = 0;
            
            var padding = nl.Plan.Padding!;
            if (padding.Start.HasValue())
                start = (int)padding.Start.Resolve(containerWidth);
            if (padding.End.HasValue())
                end = (int)padding.End.Resolve(containerWidth);
            if (padding.Top.HasValue())
                top = (int)padding.Top.Resolve(containerHeight);
            if (padding.Bottom.HasValue())
                bottom = (int)padding.Bottom.Resolve(containerHeight);
            nl.Node.Placement.Padding = new (start, top, end, bottom);
        }
        else
            nl.Node.Placement.Padding = SidesInt.Zero;
    }    
    
    private void SetPlacementBorder(NodeWithLayout nl)
    {
        nl.Node.Placement.Border = nl.Plan.Border ?? SidesInt.Zero;
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

    private bool SetMeasuredDimensions(
        NodeWithLayout nl,
        float containerWidth, float containerHeight,
        bool performLayout)
    {
        if (nl.Node.MeasureNode is not null)
        {
            SetMeasuredDimensions_MeasureFunc(nl, containerWidth, containerHeight);
            return true;
        }

        if (nl.Children.Count == 0)
        {
            SetMeasuredDimensions_EmptyContainer(nl, containerWidth, containerHeight);
            return true;
        }

        if (!performLayout &&
            SetMeasuredDimensions_FixedSize(nl, containerWidth, containerHeight))
        {
            return true;
        }

        return false;
    }
    

    private void SetMeasuredDimensions_MeasureFunc(
        NodeWithLayout nl,
        float containerWidth, float containerHeight)
    {
        Debug.Assert(
            nl.Node.MeasureNode != null,
            "Expected node to have custom measure function");

        var paddingAndBorder = nl.Node.Placement.BorderAndPadding;
        var innerWidth = nl.WidthAvailable.HasNoValue()
            ? nl.WidthAvailable
            : FloatMax(0f, nl.WidthAvailable - paddingAndBorder.Start - paddingAndBorder.End);
        var innerHeight = nl.HeightAvailable.HasNoValue()
            ? nl.HeightAvailable
            : FloatMax(0f, nl.HeightAvailable - paddingAndBorder.Top - paddingAndBorder.Bottom);
        
        if (nl.WidthMeasureMode == MeasureMode.Exactly && 
            nl.HeightMeasureMode == MeasureMode.Exactly)
        {
            // Don't bother sizing the text if both dimensions are already defined.
            nl.Node.Placement.Bounds = nl.Node.Placement.Bounds with
            {
                Width = (int)BoundValue(nl.WidthAvailable, containerWidth, nl.Plan.MinWidth, nl.Plan.MaxWidth), 
                Height = (int)BoundValue(nl.HeightAvailable, containerHeight, nl.Plan.MinHeight, nl.Plan.MaxHeight)
            };
        }
        else
        {
            // Measure the text under the current constraints.
            var measuredSize =
                nl.Node.MeasureNode!.Invoke(
                    nl.Node, 
                    innerWidth, nl.WidthMeasureMode, 
                    innerHeight, nl.HeightMeasureMode);
            float width = nl.WidthMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost
                ? measuredSize.Width + paddingAndBorder.Start + paddingAndBorder.End
                : nl.WidthAvailable;
            float height = nl.HeightMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost
                ? measuredSize.Height + paddingAndBorder.Top + paddingAndBorder.Bottom
                : nl.HeightAvailable;
            nl.Node.Placement.Bounds = nl.Node.Placement.Bounds with
            {
                Width = (int)BoundValue(width, containerWidth, nl.Plan.MinWidth, nl.Plan.MaxWidth), 
                Height = (int)BoundValue(height, containerHeight, nl.Plan.MinHeight, nl.Plan.MaxHeight)
            };
        }
    }

    /// <summary>
    ///     For nodes with no elements, use the available values if they were provided,
    ///     or the minimum size as indicated by the padding and border sizes.
    /// </summary>
    private void SetMeasuredDimensions_EmptyContainer(
        NodeWithLayout nl,
        float containerWidth, float containerHeight)
    {
        var paddingAndBorder = nl.Node.Placement.BorderAndPadding;
        
        float width = nl.WidthMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost
            ? paddingAndBorder.Start + paddingAndBorder.End
            : nl.WidthAvailable;
        float height = nl.HeightMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost
            ? paddingAndBorder.Top + paddingAndBorder.Bottom
            : nl.HeightAvailable;
        
        nl.Node.Placement.Bounds = nl.Node.Placement.Bounds with
        {
            Width = (int)BoundValue(width, containerWidth, nl.Plan.MinWidth, nl.Plan.MaxWidth), 
            Height = (int)BoundValue(height, containerHeight, nl.Plan.MinHeight, nl.Plan.MaxHeight)
        };
    }

    private bool SetMeasuredDimensions_FixedSize(
        NodeWithLayout nl,
        float containerWidth, float containerHeight)
    {
        if ((nl.WidthMeasureMode == MeasureMode.Exactly && nl.HeightMeasureMode == MeasureMode.Exactly) ||
            (nl.WidthAvailable.HasValue() && nl.WidthMeasureMode == MeasureMode.AtMost && nl.WidthAvailable <= 0f) ||
            (nl.HeightAvailable.HasValue() && nl.HeightMeasureMode == MeasureMode.AtMost && nl.HeightAvailable >= 0f))
        {
            float width = nl.WidthMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost && nl.WidthAvailable < 0f
                ? 0f
                : nl.WidthAvailable;
            float height = nl.HeightMeasureMode is MeasureMode.Undefined or MeasureMode.AtMost && nl.HeightAvailable < 0f
                ? 0f
                : nl.HeightAvailable;
            
            nl.Node.Placement.Bounds = nl.Node.Placement.Bounds with
            {
                Width = (int)BoundValue(width, containerWidth, nl.Plan.MinWidth, nl.Plan.MaxWidth), 
                Height = (int)BoundValue(height, containerHeight, nl.Plan.MinHeight, nl.Plan.MaxHeight)
            };

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
*/