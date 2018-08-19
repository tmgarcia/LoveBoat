public class CutWoodBehavior : ActionBehavior
{
    private DialogueOption _continueOption;

    private void Start()
    {
        _continueOption = new DialogueOption("Continue");
        _continueOption.OnSelect.AddListener(OnContinue);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("I decided to spend some time cutting down trees to use for lumber.");
        AddDialogueOption(_continueOption);
    }

    void OnContinue()
    {
        EndAction();
    }

    protected override void EndAction()
    {
        GameStatus.Instance.InventoryItems["wood"].Amount += 5;
        base.EndAction();
    }
}
