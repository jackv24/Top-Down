using Godot;
using System;

public class CharacterSprite : Sprite
{
    [Export]
    private bool isRightRefault = true;

    public override void _Ready()
    {
        Character player = GetParent() as Character;
        if(player != null)
            player.CharacterMoved += SetDirection;
    }

    public void SetDirection(int x, int y)
    {
        Vector2 scale = Scale;

        if (x != 0)
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(x);

        Scale = scale;
    }
}
