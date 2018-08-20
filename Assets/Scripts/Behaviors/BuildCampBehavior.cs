using System.Collections.Generic;

public class BuildCampBehavior : ActionBehavior
{

    protected override void OnActionStart()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("", "I put some work into expanding my camp.") };
        lines.Add(new DialogueLine("", "Cooking should be much easier now!"));
        GameStatus.Instance.InventoryItems["wood"].Amount -= 10;
        GameStatus.Instance.SkillLevels["cooking"].Level += 4;
        GameStatus.Instance.SkillLevels["crafting"].Level += 2;

        SetDialogue(new Dialogue(lines, null, () => {
            EndAction();
        }));
    }

    protected override void EndAction()
    {
        base.EndAction();
    }
}
