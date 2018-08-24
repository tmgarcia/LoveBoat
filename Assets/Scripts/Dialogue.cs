using System.Collections.Generic;

public class Dialogue
{
    public List<DialogueOption> Options { get; private set; }
    public List<DialogueLine> Lines { get; private set; }

    // Optional event to listen to for dialogues that don't have options
    private System.Action _onAllLinesShown;

    public Dialogue(List<DialogueLine> lines, List<DialogueOption> options, System.Action onAllLinesShown = null)
    {
        Lines = lines;
        Options = options;

        _onAllLinesShown = onAllLinesShown;
    }

    public void AllDialogueLinesShown()
    {
        if (_onAllLinesShown != null)
        {
            _onAllLinesShown();
        }
    }
}

public class DialogueLine
{
    public string Speaker { get; private set; }
    public string Text { get; private set; }
    private System.Action _onDisplay;

    public DialogueLine(string speaker, string text, System.Action onDisplay = null)
    {
        Speaker = speaker;
        Text = text;
        _onDisplay = onDisplay;
    }

    public void Display()
    {
        if (_onDisplay != null)
        {
            _onDisplay();
        }
    }
}

public class DialogueOption
{
    public string Text { get; private set; }
    private System.Action _onSelect;

    public DialogueOption(string text, System.Action onSelect)
    {
        Text = text;
        _onSelect = onSelect;
    }

    public void Select()
    {
        if (_onSelect != null)
        {
            _onSelect();
        }
    }
}