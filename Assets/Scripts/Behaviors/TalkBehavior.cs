using System.Collections.Generic;

public class TalkBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("I spent some time talking with Boat.", null) };
        _dialogue = new Dialogue("Player", lines, null, () => {
            EndAction();
        });
    }

    protected override void OnActionStart()
    {
        SetDialogue(_dialogue);
    }

    protected override void EndAction()
    {
        GameStatus.Instance.LoveLevel.CurrentValue += 5;
        base.EndAction();
    }
}
