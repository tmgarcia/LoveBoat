using System.Collections.Generic;

public class FishBehavior : ActionBehavior
{
    protected override void OnActionStart()
    {
        var lines = FishLogic();

        SetDialogue(new Dialogue(lines, null, () => {
            EndAction();
        }));
    }

    protected override void EndAction()
    {
        base.EndAction();
    }

    private List<DialogueLine> FishLogic()
    {
        var lines = new List<DialogueLine>();
        lines.Add(new DialogueLine("", "It's raining... might be a good time to fish."));

        var skill = GameStatus.Instance.SkillLevels["fishing"].Level;

        var fishGathered = 1;

        if (skill < 10)
        {
            lines.Add(new DialogueLine("", "I didn't catch much, but I'm getting better."));
            fishGathered = 1;
        }
        else if (skill < 20)
        {
            lines.Add(new DialogueLine("", "The fish were pretty cooperative today."));
            fishGathered = 2;
        }
        else
        {
            lines.Add(new DialogueLine("", "By now, I'm a master fisherman."));
            fishGathered = 3;
        }

        GameStatus.Instance.InventoryItems["foodRaw"].Amount += fishGathered;

        GameStatus.Instance.SkillLevels["fishing"].Level += 3;


        return lines;
    }
}
