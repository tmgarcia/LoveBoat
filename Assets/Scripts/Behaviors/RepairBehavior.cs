public class RepairBehavior : ActionBehavior
{
    private DialogueOption _continueOption;

    private void Start()
    {
        _continueOption = new DialogueOption("Continue");
        _continueOption.OnSelect.AddListener(OnContinue);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("I spent some repairing <Boat>.");
        AddDialogueOption(_continueOption);
    }

    void OnContinue()
    {
        EndAction();
    }

    protected override void EndAction()
    {
        GameStatus.Instance.InventoryItems["wood"].Amount -= 5;
        GameStatus.Instance.RepairLevel.CurrentValue += 5;
        GameStatus.Instance.LoveLevel.CurrentValue += 3;
        base.EndAction();
    }
}
