using Godot;
using System;
using Ink.Runtime;

public class DialoguePanel : Panel
{
    private static DialoguePanel instance;

    private RichTextLabel textLabel;

    public override void _Ready()
    {
        instance = this;
        textLabel = FindNode("Text") as RichTextLabel;

        Visible = false;
    }

    public static void StartDialogue(string name)
    {
        var file = new File();
        file.Open($"res://Ink/{name}.ink.json", (int)File.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();

        try
        {
            var story = new Story(json);
            instance?.RunStory(story);
        }
        catch
        {
            GD.Print($"File at path: \"{name}\" could not be parsed as Story!");
        }
    }

    private void RunStory(Story story)
    {
        Visible = true;

        if(textLabel != null)
        {
            textLabel.Text = story.Continue();
        }
    }
}
