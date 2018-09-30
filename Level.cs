using Godot;
using System;

public class Level : Node
{
    [Export]
    private float gridSize = 64;
    public float GridSize { get { return gridSize; } }

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {

    }
}
