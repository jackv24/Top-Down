using Godot;
using System;
using System.Linq;

public class Level : Node
{
    [Export]
    private float gridSize = 64;
    public float GridSize { get { return gridSize; } }

    private NavigableMap currentMap;

    public override void _Ready()
    {
        currentMap = this.FindChildrenOfType<NavigableMap>().FirstOrDefault();
    }

    public override void _Process(float delta)
    {

    }

    public Vector2 ProcessMovement(Vector2 startPosition, Vector2 moveVector)
    {
        if (currentMap != null)
            return currentMap.ProcessMovement(startPosition, moveVector);

        return moveVector;
    }
}
