using Godot;
using System;
using Ink.Runtime;

public class DialoguePanel : Panel
{
    private static DialoguePanel instance;

    private Story story;

    private RichTextLabel textLabel;
    private Game game;
    private bool cancelFrame;

    public override void _Ready()
    {
        instance = this;
        textLabel = FindNode("Text", true) as RichTextLabel;
        game = this.FindParentOfType<Game>(true);

        Visible = false;
    }

    public override void _Process(float delta)
    {
        if(game == null || game.State != GameState.Dialogue)
            return;

        if(cancelFrame)
        {
            cancelFrame = false;
            return;
        }

        if(Input.IsActionJustPressed("submit"))
            AdvanceStory();
    }

    public static void StartDialogue(string name, string goToKnot = null)
    {
        var file = new File();
        file.Open($"res://Ink/{name}.ink.json", (int)File.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();

        try
        {
            var story = new Story(json);
            if(!string.IsNullOrEmpty(goToKnot))
                story.ChoosePathString(goToKnot);
            instance?.RunStory(story);
        }
        catch
        {
            GD.Print($"File at path: \"{name}\" could not be parsed as Story!");
        }
    }

    private void RunStory(Story story)
    {
        this.story = story;
        Visible = true;
        if(game != null)
            game.State = GameState.Dialogue;

        cancelFrame = true;
        AdvanceStory();
    }

    private void AdvanceStory()
    {
        if (story.canContinue)
        {
            string text = story.Continue();
            if (textLabel != null)
                textLabel.Text = text;

            foreach(var choice in story.currentChoices)
                GD.Print($"Choice: {choice.text}");
        }
        else
        {
            story = null;
            Visible = false;
            if(game != null)
                game.State = GameState.Playing;
        }
    }
}
