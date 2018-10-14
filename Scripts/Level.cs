using Godot;
using System;
using System.Linq;

public class Level : Node
{
    public event Action Ready;

    [Export]
    private float gridSize = 64;
    public float GridSize { get { return gridSize; } }

    private NavigableMap currentMap;

    public override void _Ready()
    {
        currentMap = this.FindChildrenOfType<NavigableMap>().FirstOrDefault();

        Ready?.Invoke();
    }

    public Vector2 ProcessMovement(Vector2 startPosition, Vector2 moveVector)
    {
        if (currentMap != null)
            return currentMap.ProcessMovement(startPosition, moveVector);

        return moveVector;
    }

    public void AddTileOccupied(Character character, Vector2 worldPosition)
    {
        currentMap?.AddTileOccupied(character, worldPosition);
    }

    public void RemoveTileOccupied(Character character, Vector2 worldPosition)
    {
        currentMap?.RemoveTileOccupied(character, worldPosition);
    }

    public void AddInteractible(IInteractible interactible, Vector2 worldPosition)
    {
        currentMap?.AddInteractible(interactible, worldPosition);
    }

    public void RemoveTileOccupied(IInteractible interactible, Vector2 worldPosition)
    {
        currentMap?.RemoveInteractible(interactible, worldPosition);
    }

    public void Interact(Vector2 worldPosition)
    {
        currentMap?.Interact(worldPosition);
    }

    public void SetEnteredTile(Vector2 worldPosition, Character character)
    {
        currentMap?.SetEnteredTile(worldPosition, character);
    }
}
