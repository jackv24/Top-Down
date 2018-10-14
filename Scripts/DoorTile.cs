using Godot;

public class DoorTile : TileProperties
{
    [Export]
    private string nextLevel;

    public override void OnTileEntered(Character enteredCharacter)
    {
        GD.Print("Entered door tile!");
    }
}
