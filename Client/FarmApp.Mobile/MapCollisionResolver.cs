namespace FarmApp.Mobile;

public static class MapCollisionResolver
{
    public static bool IsPointerOnMap(float x, float y)
    {
        var pointerIsOutsideOfBottomPanel = CheckForBottomPanel(x, y);
        return pointerIsOutsideOfBottomPanel;
    }
    
    private static bool CheckForBottomPanel(float x, float y)
    {
        const int boxHeight = 60;
        const int leftRectX = 20;
            
        var cssX = x / ScreenOffsetProvider.Density;
        var cssY = y / ScreenOffsetProvider.Density;
            
        var cssWidth = ScreenOffsetProvider.ScreenWidth / ScreenOffsetProvider.Density;
        var cssHeight = ScreenOffsetProvider.ScreenHeight / ScreenOffsetProvider.Density;

        var topRectY = cssHeight - boxHeight - ScreenOffsetProvider.Bottom;
        var bottomRectY = cssHeight - ScreenOffsetProvider.Bottom;

        var rightRectX = cssWidth - 20;

        return !CheckIfPointInRectangle(cssX, cssY, leftRectX, topRectY, rightRectX, bottomRectY);
    }

    private static bool CheckIfPointInRectangle(float pX, float pY, float rectULx, float rectULy, 
        float rectLRx, float rectLRy)
    {
        return pX >= rectULx && pX <= rectLRx && pY >= rectULy && pY <= rectLRy;
    }
}