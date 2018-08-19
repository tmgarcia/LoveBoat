using System.Collections.Generic;

public class ForageBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() {
            new DialogueLine("", "I went out to find some supplies..."),
            new DialogueLine("", "I rooted around in the sand for a while..."),
            new DialogueLine("", "I ended up finding some crabs that look like they might taste all right."),
            new DialogueLine("", "I also found some driftwood that might come in handy."),
        };
        _dialogue = new Dialogue(lines, null, () => {
            EndAction();
        });
    }

    protected override void OnActionStart()
    {
        SetDialogue(_dialogue);
    }

    void OnGoodbye()
    {
        EndAction();
    }

    protected override void EndAction()
    {
        GameStatus.Instance.InventoryItems["foodRaw"].Amount += 1;
        GameStatus.Instance.InventoryItems["wood"].Amount += 1;
        base.EndAction();
    }
}
