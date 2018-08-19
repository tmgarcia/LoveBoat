public class FixSailBehavior : ActionBehavior
{
    private DialogueOption _continueOption;

    private void Start()
    {
        _continueOption = new DialogueOption("Continue");
        _continueOption.OnSelect.AddListener(OnContinue);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("I fixed up <Boat>'s sail.  We're leaving tomorrow morning!");
        AddDialogueOption(_continueOption);
    }

    void OnContinue()
    {
        EndAction();
    }

    protected override void EndAction()
    {
        base.EndAction();
    }
}
