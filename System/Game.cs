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
}
