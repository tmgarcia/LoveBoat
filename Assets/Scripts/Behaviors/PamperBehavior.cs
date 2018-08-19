public class PamperBehavior : ActionBehavior
{
    private DialogueOption _continueOption;

    private void Start()
    {
        _continueOption = new DialogueOption("Continue");
        _continueOption.OnSelect.AddListener(OnContinue);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("I used some of the oil I gathered to polish <Boat>'s hull.");
        AddDialogueOption(_continueOption);
    }

    void OnContinue()
    {
        EndAction();
    }

    protected override void EndAction()
    {
        GameStatus.Instance.InventoryItems["oil"].Amount -= 3;
        GameStatus.Instance.LoveLevel.CurrentValue += 8;
        base.EndAction();
    }
}
