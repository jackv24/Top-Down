using Godot;
using System;

public enum GameState
{
    Playing,
    Dialogue
}

public class Game : Node
{
    public GameState State { get; set; }

    [Export]
    private string firstLevel;

    private string currentLevel;

    public override void _Ready()
    {
        if(!string.IsNullOrEmpty(firstLevel))
            SwitchLevel(firstLevel);
    }

    public void SwitchLevel(string nextLevel)
    {
        // Unload current level
        if (!string.IsNullOrEmpty(currentLevel))
        {
            var level = GetNode(currentLevel);
            RemoveChild(level);
            level.Free();
        }

        // Load next level and set as current
        {
            var levelResource = (PackedScene)GD.Load($"res://Levels/{nextLevel}.tscn");
            var level = levelResource.Instance();
            AddChild(level);

            currentLevel = nextLevel;
        }
    }
}
