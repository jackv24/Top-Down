using Godot;
using System;

public class TileProperties : Node
{
    [Export]
    private int tileIndex;
    public int TileIndex { get {return tileIndex; } }

    [Export]
    private bool canMoveLeft = true;
    public bool CanMoveLeft { get { return canMoveLeft; } }

    [Export]
    private bool canMoveRight = true;
    public bool CanMoveRight { get { return canMoveRight; } }

    [Export]
    private bool canMoveUp = true;
    public bool CanMoveUp { get { return canMoveUp; } }

    [Export]
    private bool canMoveDown = true;
    public bool CanMoveDown { get { return canMoveDown; } }
}
