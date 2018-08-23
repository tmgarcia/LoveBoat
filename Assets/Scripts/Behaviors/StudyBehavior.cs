using System.Collections.Generic;
using UnityEngine;

public class StudyBehavior : ActionBehavior
{

    protected override void OnActionStart()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("", "I spent some time reading the survival guide.") };

        var roll = Random.Range(0, 4);
        if (roll < 1)
        {
            lines.Add(new DialogueLine("", "I learned some cooking techniques."));
            GameStatus.Instance.SkillLevels["cooking"].Level += 5;
        }
        else if (roll < 2)
        {
            lines.Add(new DialogueLine("", "I learned some woodworking techniques."));
            GameStatus.Instance.SkillLevels["crafting"].Level += 5;
        }
        else if (roll < 3)
        {
            lines.Add(new DialogueLine("", "I learned some fishing techniques."));
            GameStatus.Instance.SkillLevels["fishing"].Level += 5;
        }
        else
        {
            lines.Add(new DialogueLine("", "I learned some scouting techniques."));
            GameStatus.Instance.SkillLevels["scouting"].Level += 5;
        }


        SetDialogue(new Dialogue(lines, null, () => {
            EndAction();
        }));
    }

    protected override void EndAction()
    {
        base.EndAction();
    }
}
