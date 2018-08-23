using System.Collections.Generic;
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
        var skill = GameStatus.Instance.SkillLevels["cooking"].Level;

        if (skill < 10)
        {
            lines.Add(new DialogueLine("", "I'm still getting the hang of cooking without a kitchen..."));
            maxFoodConversion = 1;
        }
        else if (skill < 20)
        {
            lines.Add(new DialogueLine("", "I can tell I'm getting better at this survival business."));
            maxFoodConversion = 2;
        }
        else
        {
            lines.Add(new DialogueLine("", "By now, I'm a seasoned veteran of culinary survival."));
            maxFoodConversion = 3;
        }

        var foodPrepared = Mathf.Min(maxFoodConversion, GameStatus.Instance.InventoryItems["foodRaw"].Amount);
        if (foodPrepared > 1) { lines.Add(new DialogueLine("", "I made " + foodPrepared + " rations.")); }
                         else { lines.Add(new DialogueLine("", "I made 1 ration.")); };

        if (foodPrepared < maxFoodConversion)
        {
            lines.Add(new DialogueLine("", "If I had more ingredients, I probably could have made more..."));
        }

        if (maxFoodConversion == 3)
        {
            lines.Add(new DialogueLine("", "I saved some oil, too."));
            GameStatus.Instance.InventoryItems["oil"].Amount += foodPrepared;
        }

        GameStatus.Instance.InventoryItems["foodRaw"].Amount -= foodPrepared;
        GameStatus.Instance.ResourceValues["food"].CurrentValue += foodPrepared;

        GameStatus.Instance.SkillLevels["cooking"].Level += 3;


        return lines;
    }
}
