using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("VOID", "HI HOW ARE YOU", null) };
        var options = new List<DialogueOption>()
        {
            new DialogueOption("Do Nothing", () => EndAction()),
            new DialogueOption("Add Food", () => AddFood()),
            new DialogueOption("Fill Repair", () => FillRepair()),
            new DialogueOption("Fill Love", () => FillLove()),
        };
        _dialogue = new Dialogue(lines, options);
    }

    protected override void OnActionStart()
    {
        SetDialogue(_dialogue);
    }

    void AddFood()
    {
        GameStatus.Instance.FoodLevel.CurrentValue += 10;
        EndAction();
    }

    void FillRepair()
    {
        GameStatus.Instance.RepairLevel.CurrentValue = 100;
        EndAction();
    }

    void FillLove()
    {
        GameStatus.Instance.LoveLevel.CurrentValue = 100;
        EndAction();
    }

    protected override void EndAction()
    {
        base.EndAction();
    }
}
