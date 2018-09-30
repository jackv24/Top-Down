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

    public Vector2 ProcessMovement(Vector2 startPosition, Vector2 moveVector)
    {
        TileProperties currentTile = GetTileByWorldPosition(startPosition);
        TileProperties nextTile = GetTileByWorldPosition(startPosition + moveVector);

        // TODO: Actually limit movement

        GD.Print($"Current Tile: {currentTile?.TileIndex.ToString() ?? "Null"}");
        GD.Print($"Next Tile: {nextTile?.TileIndex.ToString() ?? "Null"}");

        return moveVector;
    }
}
