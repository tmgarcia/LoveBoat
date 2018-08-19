public class ExploreBehavior : ActionBehavior
{
    private DialogueOption _continueOption;

    private void Start()
    {
        _continueOption = new DialogueOption("Continue");
        _continueOption.OnSelect.AddListener(OnContinue);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("I decided to spend some time exploring the island...");
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
