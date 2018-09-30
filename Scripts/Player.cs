using Godot;
using System;

public class Player : Node2D
{
    public delegate void CharacterMovedEvent(int x, int y);
    public event CharacterMovedEvent CharacterMoved;

    private enum State
    {
        Idle,
        Moving
    }

    [Export]
    private float moveSpeed = 200;

    private float gridSize = 64;

    private State currentState;

    private Vector2 moveStartPosition;
    private Vector2 moveEndPosition;
    private float moveTimeElapsed;
    private float moveDuration;

    private Level level;

    public override void _Ready()
    {
        level = this.FindParentOfTypeRecursive<Level>();
        if(level != null)
            gridSize = level.GridSize;

        currentState = State.Idle;

        SnapToGrid();
    }

    public override void _Process(float delta)
    {
        GD.Print(currentState.ToString());

        switch(currentState)
        {
            case State.Idle:
                ProcessInput();
                break;

            case State.Moving:
                if(!ProcessMovement(delta))
                {
                    currentState = State.Idle;
                    
                    // Process input again in same frame to prevent frame pause when holding move
                    ProcessInput();
                }
                break;
        }
    }

    private void SnapToGrid()
    {
        Vector2 pos = Position;

        float halfGridSize = gridSize / 2;

        pos.x = Mathf.Round(pos.x / gridSize) * gridSize - halfGridSize;
        pos.y = Mathf.Round(pos.y / gridSize) * gridSize - halfGridSize;

        Position = pos;
    }

    private void ProcessInput()
    {
        Vector2 input = Vector2.Zero;

        if(Input.IsActionPressed("up"))
            input.y -= 1;
        if(Input.IsActionPressed("down"))
            input.y += 1;
        if(Input.IsActionPressed("left"))
            input.x -= 1;
        if(Input.IsActionPressed("right"))
            input.x += 1;

        int x = Mathf.Abs(input.x) > 0 ? (int)Mathf.Sign(input.x) : 0;
        int y = Mathf.Abs(input.y) > 0 ? (int)Mathf.Sign(input.y) : 0;        

        if(x != 0 || y != 0)
            BeginMoving(x, y);
    }

    private void BeginMoving(int x, int y)
    {
        Vector2 move = new Vector2(gridSize * x, gridSize * y);
        moveStartPosition = Position;
        moveEndPosition = moveStartPosition + move;

        moveTimeElapsed = 0;
        moveDuration = move.Length() / moveSpeed;

        currentState = State.Moving;

        CharacterMoved?.Invoke(x, y);
    }

    private bool ProcessMovement(float delta)
    {
        bool didMove = true;

        moveTimeElapsed += delta;
        if(moveTimeElapsed >= moveDuration)
        {
            moveTimeElapsed = moveDuration;
            didMove = false;
        }

        Position = moveStartPosition.LinearInterpolate(moveEndPosition, moveTimeElapsed / moveDuration);

        return didMove;
    }
}