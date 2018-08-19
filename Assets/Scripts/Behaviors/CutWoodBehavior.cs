using System.Collections.Generic;

public class CutWoodBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("Player", "I decided to spend some time cutting down trees to use for lumber.", null) };
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
        GameStatus.Instance.InventoryItems["wood"].Amount += 5;
        base.EndAction();
    }
}
