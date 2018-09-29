using Godot;
using System;

public class CharacterSprite : Sprite
{
    [Export]
    private bool isRightRefault = true;

    public void SetDirection(int x, int y)
    {
        Vector2 scale = Scale;

        if (x != 0)
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(x);

        Scale = scale;
    }
}
