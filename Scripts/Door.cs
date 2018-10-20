using Godot;
using System;

public class Door : Node2D, IInteractible
{
    [Export]
    private string targetLevel;

    [Export]
    private string targetDoor;

    [Export]
    private Vector2 exitOffset;

    private Level level;
    private Game game;

    public override void _Ready()
    {
        level = this.FindParentOfType<Level>(true);
        if (level != null)
            level.Ready += () => level.AddInteractible(this, GlobalPosition);

        game = this.FindParentOfType<Game>(true);
    }

    public void Interact()
    {
        if (game == null)
            return;

        if (string.IsNullOrEmpty(targetLevel))
        {
            GD.Print($"Target Level \"{targetLevel}\" on \"{Name}\" is not valid!");
            return;
        }

        if (string.IsNullOrEmpty(targetDoor))
        {
            GD.Print($"Target Door \"{targetDoor}\" on \"{Name}\" is not valid!");
            return;
        }

        game.SwitchLevel(targetLevel);
    }
}
