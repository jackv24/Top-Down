using Godot;
using System;

public class Character : Node2D
{
    private enum State
    {
        Idle,
        Moving
    }

    [Export]
    private float moveSpeed = 200;

    private float gridSize = 64;

    private State currentState;
    private State? previousState = null;

    private Vector2 moveStartPosition;
    private Vector2 moveEndPosition;
    private float moveTimeElapsed;
    private float moveDuration;

    private AnimationPlayer anim;
    private Sprite sprite;

    protected Vector2 previousDirection;

    [Export]
    private bool defaultRight = true;

    protected Level level;
    protected Game game;

    public override void _Ready()
    {
        level = this.FindParentOfType<Level>(true);
        if (level != null)
        {
            gridSize = level.GridSize;

            SnapToGrid();

            // Child character will become ready before parent level
            level.Ready += () =>
            {
                level.AddTileOccupied(this, Position);
                SetupLevel(level);
            };
        }

        game = this.FindParentOfType<Game>(true);

        anim = this.FindChildOfType<AnimationPlayer>(true);
        sprite = this.FindChildOfType<Sprite>(true);

        currentState = State.Idle;
    }

    protected virtual void SetupLevel(Level level)
    {

    }

    public override void _Process(float delta)
    {
        State previousStateCache = currentState;

        switch (currentState)
        {
            case State.Idle:
                if (previousState != State.Idle)
                {
                    if (anim != null)
                        anim.CurrentAnimation = "idle";
                }
                ProcessInput();
                break;

            case State.Moving:
                if (previousState != State.Moving)
                {
                    if (anim != null)
                        anim.CurrentAnimation = "run";
                }
                if (!ProcessMovement(delta))
                {
                    currentState = State.Idle;
                    
                    // Process input again in same frame to prevent frame pause when holding move
                    ProcessInput();

                    // Notify tile
                    level?.SetEnteredTile(GlobalPosition, this);
                }
                break;
        }

        previousState = previousStateCache;
    }

    private void SnapToGrid()
    {
        Vector2 pos = Position;
        float halfGridSize = gridSize / 2;

        pos.x = Mathf.Round(pos.x / gridSize) * gridSize - halfGridSize;
        pos.y = Mathf.Round(pos.y / gridSize) * gridSize - halfGridSize;

        Position = pos;
    }

    protected virtual Vector2 GetInput()
    {
        return Vector2.Zero;
    }

    private void ProcessInput()
    {
        Vector2 input = GetInput();

        int x = Mathf.Abs(input.x) > 0 ? (int)Mathf.Sign(input.x) : 0;
        int y = Mathf.Abs(input.y) > 0 ? (int)Mathf.Sign(input.y) : 0;        

        if(x != 0 || y != 0)
            BeginMoving(x, y);
    }

    private void BeginMoving(int x, int y)
    {
        if(sprite != null)
        {
            if (x != 0)
            {
                var scale = sprite.Scale;
                scale.x = Mathf.Abs(scale.x) * Mathf.Sign(x) * (defaultRight ? 1 : -1);
                sprite.Scale = scale;
            }
        }

        previousDirection = new Vector2(x != 0 ? Mathf.Sign(x) : 0, y != 0 ? Mathf.Sign(y) : 0);

        Vector2 move = new Vector2(gridSize * x, gridSize * y);
        moveStartPosition = Position;

        if(level != null)
            move = level.ProcessMovement(moveStartPosition, move);

        if(move.Length() < Mathf.Epsilon)
            return;

        moveEndPosition = moveStartPosition + move;

        moveTimeElapsed = 0;
        moveDuration = move.Length() / moveSpeed;

        if(level != null)
        {
            level.RemoveTileOccupied(this, moveStartPosition);
            level.AddTileOccupied(this, moveEndPosition);
        }

        currentState = State.Moving;
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
