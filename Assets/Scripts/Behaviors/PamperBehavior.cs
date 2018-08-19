using System.Collections.Generic;

public class PamperBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("I used some of the oil I gathered to polish Boat's hull.", null) };
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
        GameStatus.Instance.InventoryItems["oil"].Amount -= 3;
        GameStatus.Instance.LoveLevel.CurrentValue += 8;
        base.EndAction();
    }
}
