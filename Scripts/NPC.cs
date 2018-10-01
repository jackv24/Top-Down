using Godot;
using System;

public class NPC : Character, IInteractible
{
    [Export]
    private string dialogueName;

    public void Interact()
    {
        DialoguePanel.StartDialogue(dialogueName);
    }

    protected override void SetupLevel(Level level)
    {
        level.AddInteractible(this, Position);
    }
}
