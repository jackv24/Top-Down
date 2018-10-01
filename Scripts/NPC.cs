using Godot;
using System;

public class NPC : Character, IInteractible
{
    [Export]
    private string dialogueName;

    private bool talked;

    public void Interact()
    {
        DialoguePanel.StartDialogue(dialogueName, talked ? "repeat" : "");
        talked = true;
    }

    protected override void SetupLevel(Level level)
    {
        level.AddInteractible(this, Position);
    }
}
