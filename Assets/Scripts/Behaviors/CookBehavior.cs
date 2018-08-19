using System.Collections.Generic;

public class CookBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("I COOK", null) };
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
        GameStatus.Instance.FoodLevel.CurrentValue += 50;
        base.EndAction();
    }
}
