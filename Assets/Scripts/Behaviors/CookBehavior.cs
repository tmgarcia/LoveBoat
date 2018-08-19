﻿using System.Collections.Generic;
using UnityEngine;

public class CookBehavior : ActionBehavior
{
    [SerializeField] private AudioClip _cookingSound;



    protected override void OnActionStart()
    {
        var lines = CookLogic();

        SetDialogue(new Dialogue(lines, null, () => {
            EndAction();
        }));
    }

    protected override void EndAction()
    {
        base.EndAction();
    }

    private List<DialogueLine> CookLogic()
    {
        var lines = new List<DialogueLine>();
        lines.Add(new DialogueLine("", "I spent some time preparing the food I had collected."));

        var maxFoodConversion = 1;
        switch (GameStatus.Instance.SkillLevels["cooking"].Level)
        {
            case 0:
                lines.Add(new DialogueLine("", "I'm still getting the hang of cooking without a kitchen..."));
                maxFoodConversion = 1;
                break;
            case 10:
                lines.Add(new DialogueLine("", "I can tell I'm getting better at this survival business."));
                maxFoodConversion = 2;
                break;
            case 20:
                lines.Add(new DialogueLine("", "By now, I'm a seasoned veteran of culinary survival."));
                maxFoodConversion = 3;
                break;
        }

        var foodPrepared = Mathf.Min(maxFoodConversion, GameStatus.Instance.InventoryItems["foodRaw"].Amount);
        if (foodPrepared > 1) { lines.Add(new DialogueLine("", "I made " + foodPrepared + " rations.")); }
                         else { lines.Add(new DialogueLine("", "I made 1 ration.")); };

        if (foodPrepared < maxFoodConversion)
        {
            lines.Add(new DialogueLine("", "If I had more ingredients, I probably could have made more..."));
        }

        GameStatus.Instance.InventoryItems["foodRaw"].Amount -= foodPrepared;
        GameStatus.Instance.ResourceValues["food"].CurrentValue += foodPrepared;

        GameStatus.Instance.SkillLevels["cooking"].Level += 3;


        return lines;
    }
}
