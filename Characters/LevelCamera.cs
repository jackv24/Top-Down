using Godot;
using System;

public class LevelCamera : Camera2D
{
    private NavigableMap levelMap;

    public override void _Ready()
    {
        levelMap = this.FindParentOfType<NavigableMap>(true);
        if (levelMap != null)
        {
            Rect2 rect = levelMap.GetUsedRect();
            Vector2 position = rect.Position * levelMap.CellSize;
            Vector2 end = rect.End * levelMap.CellSize;
            GD.Print($"Positon: {position}, End: {end}");

            LimitLeft = (int)position.x;
            LimitTop = (int)position.y;
            LimitRight = (int)end.x;
            LimitBottom = (int)end.y;

            GD.Print($"Left: {LimitLeft}, Right: {LimitRight}, Top: {LimitTop}, Bottom: {LimitBottom}");
        }
    }
}
