using System.Collections.Generic;

public class BuildCampBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("Player", "I put some work into expanding my camp.", null) };
        _dialogue = new Dialogue(lines, null, () => {
            EndAction();
        });
    }

    protected override void OnActionStart()
    {
        SetDialogue(_dialogue);
    }
}
