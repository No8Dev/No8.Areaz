using System.Drawing;

namespace No8.AreazTests;

public class UgTests
{
    [Test]
    public void Rectangle_RightAndBottomAreOutside()
    {
        var rect = new Rectangle(1, 1, 4, 3);
        
        Assert.AreEqual(1, rect.Left);
        Assert.AreEqual(1, rect.Top);
        Assert.AreEqual(4, rect.Width);
        Assert.AreEqual(3, rect.Height);
        
        // Rectangle is not correct
        Assert.AreEqual(5, rect.Right);     // 4 is the inclusive value. 5 is outside the rectangle
        Assert.AreEqual(4, rect.Bottom);    // 3 is the inclusive value. 4 is outside the rectangle
        
        //       0  1  2  3  4  5  6
        //       |  |  |  |  |  |  |
        //     0-
        //     1-   *--*--*--*--+
        //     2-   *  *  *  *  |
        //     3-   *  *  *  *  |
        //     4-   +-----------+
        //     5-

    }
}