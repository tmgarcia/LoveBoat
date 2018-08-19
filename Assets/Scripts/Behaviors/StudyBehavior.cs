using System.Collections.Generic;

public class StudyBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("Player", "I spent some time reading the survival guide.", null) };
        _dialogue = new Dialogue(lines, null, () => {
            EndAction();
        });
    }

    protected override void OnActionStart()
    {
        SetDialogue(_dialogue);
    }

    protected override void EndAction()
    {
        base.EndAction();
    }
}
