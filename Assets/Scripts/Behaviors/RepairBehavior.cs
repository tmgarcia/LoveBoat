using System.Collections.Generic;

public class RepairBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("Player", "I spent some repairing Boat.", null) };
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
        GameStatus.Instance.InventoryItems["wood"].Amount -= 5;
        GameStatus.Instance.RepairLevel.CurrentValue += 5;
        GameStatus.Instance.LoveLevel.CurrentValue += 3;
        base.EndAction();
    }
}
