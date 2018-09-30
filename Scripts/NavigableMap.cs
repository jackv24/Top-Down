using Godot;
using System;
using System.Linq;
using TileDictionary = System.Collections.Generic.Dictionary<int, TileProperties>;

public class NavigableMap : TileMap
{
    private Level level;

    private TileDictionary tileProperties;

    public override void _Ready()
    {
        level = this.FindParentOfTypeRecursive<Level>();

        var tileChildren = this.FindChildrenOfType<TileProperties>();
        tileProperties = tileChildren
        .GroupBy(tile => tile.TileIndex)
        .ToDictionary(group => group.Key, group => group.FirstOrDefault());

        foreach(var kvp in tileProperties)
            GD.Print($"Found tile properties for index: {kvp.Key}. Up: {kvp.Value.CanMoveUp}, Down: {kvp.Value.CanMoveDown}, Left: {kvp.Value.CanMoveLeft}, Right: {kvp.Value.CanMoveRight}");
    }

    public TileProperties GetTileByWorldPosition(Vector2 worldPosition)
    {
        Vector2 tilePos = WorldToMap(worldPosition);
        int tileIndex = GetCellv(tilePos);

        if(tileProperties.ContainsKey(tileIndex))
            return tileProperties[tileIndex];
        else
            return null;
    }
}
