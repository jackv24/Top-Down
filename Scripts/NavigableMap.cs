using Godot;
using System;
using System.Linq;
using TileDictionary = System.Collections.Generic.Dictionary<int, TileProperties>;
using OccupyingMap = System.Collections.Generic.Dictionary<Godot.Vector2, System.Collections.Generic.List<Character>>;
using InteractibleMap = System.Collections.Generic.Dictionary<Godot.Vector2, System.Collections.Generic.List<IInteractible>>;

public class NavigableMap : TileMap
{
    private Level level;

    private TileDictionary tileProperties;
    private OccupyingMap occupyingMap = new OccupyingMap();
    private InteractibleMap interactibleMap = new InteractibleMap();

    public override void _Ready()
    {
        level = this.FindParentOfType<Level>(true);

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
        // Process vector axis seperately so we can keep moving of one axis is still clear
        Vector2 horizontal = ProcessMovementInternal(startPosition, moveVector.Where(y: 0));
        Vector2 vertical = ProcessMovementInternal(startPosition, moveVector.Where(x: 0));

        return ProcessMovementInternal(startPosition, horizontal + vertical);
    }

    private Vector2 ProcessMovementInternal(Vector2 startPosition, Vector2 moveVector)
    {
        TileProperties currentTile = GetTileByWorldPosition(startPosition);
        Vector2 endPosition = startPosition + moveVector;
        TileProperties nextTile = GetTileByWorldPosition(endPosition);

        if (currentTile == null)
            return moveVector;
        else if (nextTile == null || IsTileOccupied(endPosition))
            return Vector2.Zero;

        if (moveVector.x > 0)
        {
            if(!currentTile.CanExitRight || !nextTile.CanEnterLeft)
                moveVector.x = 0;
        }
        else
        {
            if(!currentTile.CanExitLeft || !nextTile.CanEnterRight)
                moveVector.x = 0;
        }

        if(moveVector.y > 0)
        {
            if(!currentTile.CanExitDown || !nextTile.CanEnterUp)
                moveVector.y = 0;
        }
        else
        {
            if(!currentTile.CanExitUp || !nextTile.CanEnterDown)
                moveVector.y = 0;
        }

        return moveVector;
    }

    public void AddTileOccupied(Character character, Vector2 worldPosition)
    {
        Vector2 tileIndices = WorldToMap(worldPosition);
        if(!occupyingMap.ContainsKey(tileIndices) || occupyingMap[tileIndices] == null)
            occupyingMap[tileIndices] = new System.Collections.Generic.List<Character>();
        occupyingMap[tileIndices].Add(character);
    }

    public void RemoveTileOccupied(Character character, Vector2 worldPosition)
    {
        Vector2 tileIndices = WorldToMap(worldPosition);
        if (occupyingMap.ContainsKey(tileIndices)
        && occupyingMap[tileIndices] != null
        && occupyingMap[tileIndices].Contains(character))
        {
            occupyingMap[tileIndices].Remove(character);
        }
    }

    public bool IsTileOccupied(Vector2 worldPosition)
    {
        Vector2 tileIndices = WorldToMap(worldPosition);
        if (occupyingMap.ContainsKey(tileIndices)
        && occupyingMap[tileIndices] != null
        && occupyingMap[tileIndices].Count > 0)
        {
            return true;
        }

        return false;
    }

    public void AddInteractible(IInteractible interactible, Vector2 worldPosition)
    {
        Vector2 tileIndices = WorldToMap(worldPosition);
        if(!interactibleMap.ContainsKey(tileIndices) || interactibleMap[tileIndices] == null)
            interactibleMap[tileIndices] = new System.Collections.Generic.List<IInteractible>();
        interactibleMap[tileIndices].Add(interactible);
    }

    public void RemoveInteractible(IInteractible interactible, Vector2 worldPosition)
    {
        Vector2 tileIndices = WorldToMap(worldPosition);
        if (interactibleMap.ContainsKey(tileIndices)
        && interactibleMap[tileIndices] != null
        && interactibleMap[tileIndices].Contains(interactible))
        {
            interactibleMap[tileIndices].Remove(interactible);
        }
    }

    public void Interact(Vector2 worldPosition)
    {
        Vector2 tileIndices = WorldToMap(worldPosition);
        if (interactibleMap.ContainsKey(tileIndices)
        && interactibleMap[tileIndices] != null
        && interactibleMap[tileIndices].Count > 0)
        {
            interactibleMap[tileIndices].First().Interact();
        }
    }
}
