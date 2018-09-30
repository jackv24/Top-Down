using Godot;
using System;

public class TileProperties : Node
{
    [Export]
    private int tileIndex;
    public int TileIndex { get {return tileIndex; } }

    [Export]
    private bool canExitLeft = true;
    public bool CanExitLeft { get { return canExitLeft; } }

    [Export]
    private bool canExitRight = true;
    public bool CanExitRight {get { return canExitRight; } }

    [Export]
    private bool canExitUp = true;
    public bool CanExitUp {get { return canExitUp; } }

    [Export]
    private bool canExitDown = true;
    public bool CanExitDown {get { return canExitDown; } }

    [Export]
    private bool canEnterLeft = true;
    public bool CanEnterLeft { get { return canEnterLeft; } }

    [Export]
    private bool canEnterRight = true;
    public bool CanEnterRight {get { return canEnterRight; } }

    [Export]
    private bool canEnterUp = true;
    public bool CanEnterUp {get { return canEnterUp; } }

    [Export]
    private bool canEnterDown = true;
    public bool CanEnterDown {get { return canEnterDown; } }
}
