using Godot;
using System;

public class Player : Character
{
    protected override Vector2 GetInput()
    {
        if(Input.IsActionJustPressed("submit") && previousDirection.Length() > 0)
        {
            if(level != null)
            {
                level.Interact(Position + previousDirection * level.GridSize);
            }

            return Vector2.Zero;
        }

        Vector2 input = Vector2.Zero;

        if(Input.IsActionPressed("up"))
            input.y -= 1;
        if(Input.IsActionPressed("down"))
            input.y += 1;
        if(Input.IsActionPressed("left"))
            input.x -= 1;
        if(Input.IsActionPressed("right"))
            input.x += 1;

        return input;
    }
}
