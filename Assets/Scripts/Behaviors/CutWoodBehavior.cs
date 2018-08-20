using System.Collections.Generic;

public class CutWoodBehavior : ActionBehavior
{
    protected override void OnActionStart()
    {
        var lines = CutWoodLogic();

        SetDialogue(new Dialogue(lines, null, () => {
            EndAction();
        }));
    }

    protected override void EndAction()
    {
        base.EndAction();
    }

    private List<DialogueLine> CutWoodLogic()
    {
        var lines = new List<DialogueLine>();
        lines.Add(new DialogueLine("", "I spent some time chopping wood."));

        var skill = GameStatus.Instance.SkillLevels["crafting"].Level;

        var woodGathered = 1;

        if (skill < 10)
        {
            lines.Add(new DialogueLine("", "It's slow going, since I don't know what I'm doing, but I'm slowly improving."));
            woodGathered = 2;
        }
        else if (skill < 20)
        {
            lines.Add(new DialogueLine("", "I made decent progress and came back with a decent amount of lumber."));
            woodGathered = 4;
        }
        else
        {
            lines.Add(new DialogueLine("", "After a long day, I came back with a huge stack of lumber."));
            woodGathered = 7;
        }

        GameStatus.Instance.InventoryItems["wood"].Amount += woodGathered;

        GameStatus.Instance.SkillLevels["crafting"].Level += 3;


        return lines;
    }
}
