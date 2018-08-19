using System.Collections.Generic;

public class ForageBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() {
            new DialogueLine("You go out to find some supplies...", null),
            new DialogueLine("You root around in the sand for a while...", null),
            new DialogueLine("You end up finding some crabs that look like they might taste all right.", null),
        };
        _dialogue = new Dialogue("Player", lines, null, () => {
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
        base.EndAction();
    }
}
