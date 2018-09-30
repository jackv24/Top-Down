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
        if(currentMap != null)
        {
            TileProperties tile = currentMap.GetTileByWorldPosition(startPosition);
            if(tile == null)
                moveVector = Vector2.Zero;
            else
            {
                if(moveVector.x < 0)
                {
                    if(!tile.CanMoveLeft)
                        moveVector.x = 0;
                }
                else
                {
                    if(!tile.CanMoveRight)
                        moveVector.x = 0;
                }

                if(moveVector.y < 0)
                {
                    if(!tile.CanMoveUp)
                        moveVector.y = 0;
                }
                else
                {
                    if(!tile.CanMoveDown)
                        moveVector.y = 0;
                }
            }
        }

        return moveVector;
    }
}
