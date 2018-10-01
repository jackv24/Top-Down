using Godot;
using System;
using Ink.Runtime;

public class DialogueReader : RichTextLabel
{
    public override void _Ready()
    {
        var file = new File();
        file.Open("res://Ink/Test.ink.json", (int)File.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();

        var story = new Story(json);
        string firstLine = story.Continue();

        Text = firstLine;
    }
}
