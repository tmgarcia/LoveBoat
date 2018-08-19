using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBehavior : ActionBehavior
{
    private DialogueOption _endOption;
    private DialogueOption _addFoodOption;
    private DialogueOption _fillRepairOption;
    private DialogueOption _fillLoveOption;

    private void Start()
    {
        _endOption = new DialogueOption("Do Nothing");
        _endOption.OnSelect.AddListener(EndAction);
        _addFoodOption = new DialogueOption("Add Food");
        _addFoodOption.OnSelect.AddListener(AddFood);
        _fillRepairOption = new DialogueOption("Fill Repair");
        _fillRepairOption.OnSelect.AddListener(FillRepair);
        _fillLoveOption = new DialogueOption("Fill Love");
        _fillLoveOption.OnSelect.AddListener(FillLove);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("HI HOW ARE YOU");
        AddDialogueOption(_endOption);
        AddDialogueOption(_addFoodOption);
        AddDialogueOption(_fillRepairOption);
        AddDialogueOption(_fillLoveOption);
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
