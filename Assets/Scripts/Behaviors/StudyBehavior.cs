using System.Collections.Generic;
using UnityEngine;

public class StudyBehavior : ActionBehavior
{

    protected override void OnActionStart()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("", "I spent some time reading the survival guide.") };

        var roll = Random.Range(0, 3);
        if (roll < 1)
        {
            lines.Add(new DialogueLine("", "I learned some cooking techniques."));
            GameStatus.Instance.SkillLevels["cooking"].Level += 2;
        }
        else if (roll < 2)
        {
            lines.Add(new DialogueLine("", "I learned some woodworking techniques."));
            GameStatus.Instance.SkillLevels["crafting"].Level += 2;
        }
        else
        {
            lines.Add(new DialogueLine("", "I learned some fishing techniques."));
            GameStatus.Instance.SkillLevels["fishing"].Level += 2;
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
